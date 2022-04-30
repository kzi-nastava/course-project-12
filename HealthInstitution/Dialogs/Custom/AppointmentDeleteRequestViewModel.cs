﻿using HealthInstitution.Dialogs.Service;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;
using System;
using System.Windows.Input;

namespace HealthInstitution.Dialogs.Custom
{
    public class AppointmentDeleteRequestViewModel : DialogViewModelBase<AppointmentDeleteRequestViewModel>
    {
        private Guid _appointmentRequestId;

        #region Properties

        private string _patientEmailAddress;
        public string PatientEmailAddress
        {
            get { return _patientEmailAddress; }
            set { _patientEmailAddress = value; OnPropertyChanged(nameof(PatientEmailAddress)); }
        }

        private string _patientFullName;
        public string PatientFullName
        {
            get { return _patientFullName; }
            set { _patientFullName = value; OnPropertyChanged(nameof(PatientFullName)); }
        }

        private string _doctorEmailAddress;
        public string DoctorEmailAddress
        {
            get { return _doctorEmailAddress; }
            set { _doctorEmailAddress = value; OnPropertyChanged(nameof(DoctorEmailAddress)); }
        }

        private string _doctorFullName;
        public string DoctorFullName
        {
            get { return _doctorFullName; }
            set { _doctorFullName = value; OnPropertyChanged(nameof(DoctorFullName)); }
        }

        private string _doctorSpecialization;
        public string DoctorSpecialization
        {
            get { return _doctorSpecialization; }
            set { _doctorSpecialization = value; OnPropertyChanged(nameof(DoctorSpecialization)); }
        }

        private DateTime _appointmentStartTime;
        public DateTime AppointmentStartTime
        {
            get { return _appointmentStartTime; }
            set { _appointmentStartTime = value; OnPropertyChanged(nameof(AppointmentStartTime)); }
        }

        private DateTime _appointmentEndTime;
        public DateTime AppointmentEndTime
        {
            get { return _appointmentEndTime; }
            set { _appointmentEndTime = value; OnPropertyChanged(nameof(AppointmentEndTime)); }
        }

        private string _roomName;
        public string RoomName
        {
            get { return _roomName; }
            set { _roomName = value; OnPropertyChanged(nameof(RoomName)); }
        }

        private string _roomType;
        public string RoomType
        {
            get { return _roomType; }
            set { _roomType = value; OnPropertyChanged(nameof(RoomType)); }
        }

        #endregion

        #region Services

        private IAppointmentDeleteRequestService _appointmentDeleteRequestService;

        #endregion

        #region Commands

        public ICommand Ok { get; private set; }

        #endregion

        public AppointmentDeleteRequestViewModel(IAppointmentDeleteRequestService appointmentDeleteRequestService, Guid appointmentRequestId) :
                base("Appointment request details", 800, 650)
        {
            _appointmentDeleteRequestService = appointmentDeleteRequestService;
            _appointmentRequestId = appointmentRequestId;

            FetchAppointmentDeleteRequest();

            Ok = new RelayCommand<IDialogWindow>(w => CloseDialogWithResult(w, null));

        }

        public void FetchAppointmentDeleteRequest()
        {
            var appointmentDeleteRequest = _appointmentDeleteRequestService.Read(_appointmentRequestId);

            PatientEmailAddress = appointmentDeleteRequest.Patient.EmailAddress;
            PatientFullName = appointmentDeleteRequest.Patient.FullName;
            DoctorEmailAddress = appointmentDeleteRequest.Appointment.Doctor.EmailAddress;
            DoctorFullName = appointmentDeleteRequest.Appointment.Doctor.FullName;
            DoctorSpecialization = appointmentDeleteRequest.Appointment.Doctor.Specialization.ToString();
            AppointmentStartTime = appointmentDeleteRequest.Appointment.StartDate;
            AppointmentEndTime = appointmentDeleteRequest.Appointment.EndDate;
            RoomName = appointmentDeleteRequest.Appointment.Room.Name;
            RoomType = appointmentDeleteRequest.Appointment.Room.RoomType.ToString();
        }
    }
}
