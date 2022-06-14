﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository.Implementation
{
    public class OffDaysRequestRepository : CrudRepository<OffDaysRequest>, IOffDaysRequestRepository
    {
        public OffDaysRequestService(DatabaseContext context) : base(context)
        {
            
        }
    }
}
