﻿using HealthInstitution.Model;
using HealthInstitution.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Model.patient;
using HealthInstitution.Model.user;
using HealthInstitution.Services.Interfaces;

namespace HealthInstitution.Services.Implementation
{
    public class ActivityService : CrudService<Activity>, IActivityService
    {
        public ActivityService(DatabaseContext context) : base(context)
        {

        }

        public IEnumerable<Activity> ReadPatientUpdateOrRemoveActivity(Patient pt, int interval)
        {
            return _entities.Where(act => act.Patient == pt && act.DateOfAction > DateTime.Now.AddDays(-interval) && act.ActivityType != ActivityType.Create);
        }

        public IEnumerable<Activity> ReadPatientMakeActivity(Patient pt, int interval)
        {
            return _entities.Where(act => act.Patient == pt && act.DateOfAction > DateTime.Now.AddDays(-interval) && act.ActivityType == ActivityType.Create);
        }
    }
}
