﻿using System;
using System.Linq;
using HealthInstitution.Core.Features.SurveyManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Services.Interfaces;

namespace HealthInstitution.Core.Services.Implementation
{
    public class HealthInstitutionSurveyService : CrudService<HealthInstitutionSurvey>, IHealthInstitutionSurveyService
    {
        public HealthInstitutionSurveyService(DatabaseContext context) : base(context)
        {

        }

        public bool HasPatientAlreadySubmitedSurvey(Patient patient) {
            return _entities.Where(his => his.Patient == patient && his.CreatedAt.AddDays(30) > DateTime.Now).ToList().Count != 0;
        }

        public double AverageClearliness()
        {
            double avg = _entities.Select(his => his.Cleanliness).ToList().DefaultIfEmpty(0).Average();
            return Math.Round(avg, 2);
        }

        public double AverageServiceQuality()
        {
            double avg = _entities.Select(his => his.ServiceQuality).ToList().DefaultIfEmpty(0).Average();
            return Math.Round(avg, 2);
        }

        public double AverageServiceSatisfaction()
        {
            double avg = _entities.Select(his => his.ServiceSatisfaction).ToList().DefaultIfEmpty(0).Average();
            return Math.Round(avg, 2);
        }

        public double AverageRecommendation()
        {
            double avg = _entities.Select(his => his.Recommendation).ToList().DefaultIfEmpty(0).Average();
            return Math.Round(avg, 2);
        }

        public int RatesPerSurveyCategory(int rate, string cat)
        {
            int numOfRates;
            switch (cat)
            {
                case "Clearliness":
                    numOfRates = _entities.Where(his => his.Cleanliness == rate).Count();
                break;
                case "Service quality":
                    numOfRates = _entities.Where(his => his.ServiceQuality == rate).Count();
                break;
                case "Service satisfaction":
                    numOfRates = _entities.Where(his => his.ServiceSatisfaction == rate).Count();
                break;
                case "Recommendation":
                    numOfRates = _entities.Where(his => his.Recommendation == rate).Count();
                break;
                default:
                    numOfRates = _entities.Where(his => his.Cleanliness == rate).Count();
                break;
            }
            return numOfRates;
        }
    }
}