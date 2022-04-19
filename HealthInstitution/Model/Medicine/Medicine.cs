﻿using System.Collections.Generic;

namespace HealthInstitution.Model
{
    public class Medicine : BaseObservableEntity
    {
        #region Attributes

        private readonly IList<Ingredient> _ingredients;
        public virtual IList<Ingredient> Ingredients { get => _ingredients; }

        private string _description;
        public string Description { get => _description; set => OnPropertyChanged(ref _description, value); }

        private Status _status;
        public Status Status { get => _status; set => OnPropertyChanged(ref _status, value); }

        #endregion

        #region Constructor

        public Medicine()
        {

        }

        public Medicine(string description)
        {
            _ingredients = new List<Ingredient>();
            _description = description;
            _status = Status.Pending;
        }

        #endregion

        #region Methods

        public void AddIngredient(Ingredient ingredient)
        {
            foreach (Ingredient ingredientInMedicine in _ingredients)
            {
                if (ingredientInMedicine.Id == ingredient.Id)
                {
                    // Baci exception
                    return;
                }
            }

            _ingredients.Add(ingredient);
        }

        #endregion
    }
}
