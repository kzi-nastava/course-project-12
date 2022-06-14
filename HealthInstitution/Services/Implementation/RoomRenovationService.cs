﻿using HealthInstitution.Model;
using HealthInstitution.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Model.room;
using HealthInstitution.Services.Interfaces;

namespace HealthInstitution.Services.Implementation
{
    public class RoomRenovationService : CrudService<RoomRenovation>, IRoomRenovationService
    {
        public RoomRenovationService(DatabaseContext context) : base(context)
        {

        }

        public bool IsRoomNotRenovating(Room room, DateTime fromDate, DateTime toDate)
        {
            return ((_entities.Where(renovation => renovation.RenovatedRoom == room && renovation.StartTime < toDate && fromDate < renovation.EndTime)).Count() == 0)
                && IsRoomRenovatingMerge(room, fromDate, toDate);
        }

        public bool IsRoomRenovatingMerge(Room room, DateTime fromDate, DateTime toDate)
        {
            return ((_entities.Where(renovation => renovation.AdvancedDivide == false && 
            (renovation.RenovatedSmallRoom1Name == room.Name || renovation.RenovatedSmallRoom2Name == room.Name) && 
            renovation.StartTime < toDate && fromDate < renovation.EndTime)).Count() == 0);
        }
    }
}
