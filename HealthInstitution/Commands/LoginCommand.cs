﻿using HealthInstitution.Model;
using HealthInstitution.Ninject;
using HealthInstitution.Utility;
using HealthInstitution.ViewModel;
using System.ComponentModel;
using System.Windows;

namespace HealthInstitution.Commands
{
    public class LoginCommand : CommandBase
    {
        private readonly LoginViewModel? _viewModel;

        public LoginCommand(LoginViewModel lwm) {
            _viewModel = lwm;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(_viewModel.Email) || e.PropertyName == nameof(_viewModel.Password))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_viewModel.Email) && !string.IsNullOrEmpty(_viewModel.Password) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            var user = _viewModel._userService.Authenticate(_viewModel.Email, _viewModel.Password);
            if (user == null)
            {
                _viewModel.ErrMsgText = "Email or password is incorrect";
                _viewModel.ErrMsgVisibility = Visibility.Visible;
            }
            else {
                ViewModelBase viewModel;
                switch (user.Role) {
                    case Role.Manager:
                        Manager mn = (Manager)user;
                        GlobalStore.AddObject("LoggedUser", mn);
                        EventBus.FireEvent("ManagerLogin");
                        //todo
                        TitleManager.Title = "Manager";
                        break;
                    case Role.Administrator:
                        //todo
                        break;
                    case Role.Patient:
                        Patient pt = (Patient)user;
                        if (!pt.IsBlocked)
                        {
                            GlobalStore.AddObject("LoggedUser", pt);
                            EventBus.FireEvent("PatientLogin");
                            TitleManager.Title = "Patient";
                        }
                        else 
                        {
                            _viewModel.ErrMsgText = "Your profile is blocked!";
                            _viewModel.ErrMsgVisibility = Visibility.Visible;
                        }
                        break;
                    case Role.Doctor:
                        Doctor doc = (Doctor)user;
                        GlobalStore.AddObject("LoggedUser", doc);
                        EventBus.FireEvent("DoctorLogin");
                        TitleManager.Title = "Doctor";
                        break;
                    case Role.Secretary:
                        EventBus.FireEvent("SecretaryLogin");
                        TitleManager.Title = "Secretary";
                        break;
                    default:
                        MessageBox.Show("ERR");
                        return;
                }
            }
            
        }
    }
}
