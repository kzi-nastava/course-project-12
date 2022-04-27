﻿using HealthInstitution.Utility;

namespace HealthInstitution.Validation
{
    public class ErrorMessageViewModel : ObservableEntity
    {
        private string _errorMessage = "";
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); OnPropertyChanged(nameof(HasError)); }
        }

        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
    }
}