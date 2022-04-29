﻿using HealthInstitution.Exceptions;
using HealthInstitution.Model;
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
    public class DoctorUpdateAppointmentCommand : CommandBase
    {
        DoctorAppointmentUpdateViewModel _viewModel;
        public DoctorUpdateAppointmentCommand(DoctorAppointmentUpdateViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.SelectedPatient))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.SelectedPatient is not null
                && base.CanExecute(parameter);
        }
        public override void Execute(object? parameter)
        {
            int year = _viewModel.Date.Year;
            int month = _viewModel.Date.Month;
            int day = _viewModel.Date.Day;
            int hour = _viewModel.Time.Hour;
            int minute = _viewModel.Time.Minute;
            DateTime startDate = new DateTime(year, month, day, hour, minute, 0);
            DateTime endDate = startDate.AddMinutes(15);

            Doctor doctor = GlobalStore.ReadObject<Doctor>("LoggedUser");
            try
            {
                _viewModel.AppointmentService.updateAppointment(_viewModel.Appointment, _viewModel.SelectedPatient, doctor, startDate, endDate);                
            }
            catch (DoctorBusyException)
            {
                MessageBox.Show("There is already an appointment at selected time!");
                return;
            }
            catch (RoomBusyException)
            {
                MessageBox.Show("All rooms are busy at selected time!");
                return;
            }
            catch (UpdateFailedException)
            {
                MessageBox.Show("You didn't update any of information!\n(Appointment remains the same)");
                return;
            }
            EventBus.FireEvent("UpdateSchedule");
            EventBus.FireEvent("DoctorSchedule");
        }
    }
}