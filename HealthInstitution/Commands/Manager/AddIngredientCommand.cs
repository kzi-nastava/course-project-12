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
    public class AddIngredientCommand : CommandBase
    {

        private readonly IngredientOverviewViewModel? _viewModel;
        public AddIngredientCommand(IngredientOverviewViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.NameBox))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_viewModel.NameBox) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            bool ingredientExists = _viewModel.IngredientService.IngredientExists(_viewModel.NameBox);
            if (ingredientExists)
            {
                MessageBox.Show("Ingredient with that name already exists!");
                return;
            }
            Ingredient i = new Ingredient{ Name = _viewModel.NameBox };
            _viewModel.IngredientService.Create(i);
            MessageBox.Show("Ingredient added successfully!");
            EventBus.FireEvent("IngredientOverview");
        }
    }
}