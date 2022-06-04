﻿using HealthInstitution.Model.user;

namespace HealthInstitution.Model.medicine
{
    public class MedicineReview : BaseObservableEntity
    {
        #region Attributes
        private Medicine _medicine;
        public virtual Medicine Medicine { get => _medicine; set => OnPropertyChanged(ref _medicine, value); }

        private string? _comment;
        public string? Comment { get => _comment; set => OnPropertyChanged(ref _comment, value); }

        private Doctor _doctor;
        public virtual Doctor Doctor { get => _doctor; set => OnPropertyChanged(ref _doctor, value); }
        #endregion

        #region Constructor
        public MedicineReview() { }
        #endregion
    }
}
