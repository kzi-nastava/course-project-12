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
    public class SendMedicineRequestCommand : CommandBase
    {

        private readonly MedicineOverviewViewModel? _viewModel;
        public SendMedicineRequestCommand(MedicineOverviewViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.NameBox) || e.PropertyName == nameof(_viewModel.DescriptionBox) || e.PropertyName == nameof(_viewModel.NewIngredients))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_viewModel.NameBox) && !string.IsNullOrEmpty(_viewModel.DescriptionBox) && 
                _viewModel.NewIngredients.Count() != 0 && _viewModel.DescriptionBox.Length <= 140 && base.CanExecute(parameter);
        }

        private void SendRevisionRequest()
        {
            Medicine rM = _viewModel.MedicineService.GetMedicineByName(_viewModel.RevisionMedicineName).ElementAt(0);
            rM.Name = _viewModel.NameBox;
            rM.Description = _viewModel.DescriptionBox;
            rM.Status = Status.Pending;
            rM.Ingredients = _viewModel.NewIngredients.ToList();
            _viewModel.MedicineService.Update(rM);
            _viewModel.MedicineReviewService.Delete(GlobalStore.ReadObject<MedicineReview>("SelectedRejectedMedicine").Id);
            MessageBox.Show("The medicine has been successfully submitted for verification.");
        }

        private void SendRequest()
        {
            string name = _viewModel.NameBox;
            bool medicineExists = _viewModel.MedicineService.MedicineExists(name);
            if (medicineExists)
            {
                MessageBox.Show("Medicine with that name already exists!");
                return;
            }
            Medicine m = new Medicine
            {
                Name = _viewModel.NameBox,
                Description = _viewModel.DescriptionBox,
                Status = Status.Pending,
                Ingredients = _viewModel.NewIngredients.ToList()
            };
            _viewModel.MedicineService.Create(m);
            MessageBox.Show("The medicine has been successfully submitted for verification.");
        }

        public override void Execute(object? parameter)
        {
            if (_viewModel.IsRevision)
            {
                SendRevisionRequest();
                EventBus.FireEvent("MedicineOverview");
                return;
            }
            SendRequest();
        }
    }
}