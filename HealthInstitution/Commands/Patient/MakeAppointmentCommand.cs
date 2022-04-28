﻿using HealthInstitution.Model;
using HealthInstitution.Utility;
using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthInstitution.Commands
{
    public class MakeAppointmentCommand : CommandBase
    {
        private readonly AppointmentCreationViewModel? _viewModel;
        public MakeAppointmentCommand(AppointmentCreationViewModel viewModel) {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.Date) || e.PropertyName == nameof(_viewModel.Hours) || e.PropertyName == nameof(_viewModel.Minutes) || e.PropertyName == nameof(_viewModel.SelectedDoctor))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !(_viewModel.Date <= DateTime.Now) && !string.IsNullOrEmpty(_viewModel.Hours) && !string.IsNullOrEmpty(_viewModel.Minutes) 
                && !(_viewModel.SelectedDoctor == null) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter) {
            DateTime startTime = _viewModel.Date.AddHours(Int32.Parse(_viewModel.Hours)).AddMinutes(Int32.Parse(_viewModel.Minutes));
            DateTime endTime = startTime.AddMinutes(15);

            //doctors availabilty check
            var doctorAvailability = _viewModel._appointmentService.IsDoctorAvailable(_viewModel.SelectedDoctor, startTime, endTime);
            if (!doctorAvailability) {
                MessageBox.Show("Selected doctor is busy at selected time!");
                return;
            }

            doctorAvailability = _viewModel._appointmentUpdateRequestService.IsDoctorAvailable(_viewModel.SelectedDoctor, startTime, endTime);
            if (!doctorAvailability)
            {
                MessageBox.Show("Selected doctor may be busy at selected time!");
                return;
            }

            //rooms availabilty check
            var examinationRooms = _viewModel._roomService.ReadRoomsWithType(RoomType.ExaminationRoom);
            Room emptyRoom = null;
            foreach (var room in examinationRooms)
            {
                var roomAvailability = _viewModel._appointmentService.IsRoomAvailable(room, startTime, endTime);
                if (roomAvailability) 
                {
                    emptyRoom = room;
                    break;
                }
            }

            if (emptyRoom == null) {
                MessageBox.Show("All rooms are busy at selected time!");
                return;
            }

            Patient pt = GlobalStore.ReadObject<Patient>("LoggedUser");
            Appointment app = new Appointment(_viewModel.SelectedDoctor, pt, startTime, endTime, emptyRoom, "Type anamnesis here", false);
            _viewModel._appointmentService.Create(app);
            Activity act = new Activity(pt, DateTime.Now, ActivityType.Create);
            _viewModel._activityService.Create(act);
            MessageBox.Show("Appointment created successfully!");
            var activityCount = _viewModel._activityService.ReadPatientMakeActivity(pt, 30).ToList<Activity>().Count;
            if (activityCount > 8)
            {
                pt.IsBlocked = true;
                _viewModel._patientService.Update(pt);
                MessageBox.Show("Your profile has been blocked!\n(Too many appointments made)");
                EventBus.FireEvent("BackToLogin");
            }
            else 
            {
                EventBus.FireEvent("PatientAppointments");
            }
        }
    }
}