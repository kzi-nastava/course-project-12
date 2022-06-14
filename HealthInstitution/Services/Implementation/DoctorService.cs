﻿using HealthInstitution.Model;
using HealthInstitution.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Model.doctor;
using HealthInstitution.Model.user;
using HealthInstitution.Services.Interfaces;

namespace HealthInstitution.Services.Implementation
{
    public class DoctorService : UserService<Doctor>, IDoctorService
    {
        private readonly IOffDaysRequestService _offDaysRequestService;
        public DoctorService(DatabaseContext context, IOffDaysRequestService offDaysRequestService) : base(context)
        {
            _offDaysRequestService = offDaysRequestService;
        }

        public IList<Doctor> GetDoctorsForDoctorSpecialization(DoctorSpecialization doctorSpecialization)
        {
            return _entities.Where(d => d.Specialization == doctorSpecialization).ToList();
            
        }

        public IEnumerable<Doctor> FindDoctorsWithSpecialization(DoctorSpecialization specialization)
        {
            return _entities.Where(doc => doc.Specialization == specialization);
        }

        public IEnumerable<Doctor> FilterDoctorsBySearchText(string searchText)
        {
            searchText = searchText.ToLower();
            var specializations = Enum.GetValues(typeof(DoctorSpecialization)).Cast<DoctorSpecialization>().Where(text => Enum.GetName(typeof(DoctorSpecialization), text).ToLower().Contains(searchText));
            return _entities.Where(doc => doc.FirstName.ToLower().Contains(searchText)
           || doc.LastName.ToLower().Contains(searchText) || specializations.Contains(doc.Specialization));
        }

        public bool IsInOffice(Doctor doctor, DateTime fromDate, DateTime toDate)
        {
            return _offDaysRequestService.ReadAll()
                .Where(e => e.Doctor == doctor)
                .Where(e => e.Status == Status.Approved)
                .Count(e => e.StartDate <= toDate && fromDate <= e.EndDate) == 0;
        }
    }
}
