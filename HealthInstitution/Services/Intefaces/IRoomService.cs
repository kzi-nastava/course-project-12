﻿using HealthInstitution.Model;
using System.Collections.Generic;

namespace HealthInstitution.Services.Intefaces
{
    public interface IRoomService : ICrudService<Room>
    {
        IEnumerable<Room> ReadRoomsWithType(RoomType rt);

        IEnumerable<Room> ReadRoomsWithName(string name);

    }
}
