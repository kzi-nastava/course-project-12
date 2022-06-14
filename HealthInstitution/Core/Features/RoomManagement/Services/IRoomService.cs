﻿using System.Collections.Generic;
using HealthInstitution.Core.Features.EquipmentManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Services.Interfaces;

namespace HealthInstitution.Core.Features.RoomManagement.Services
{
    public interface IRoomService
    {
        public IEnumerable<Room> ReadRooms(RoomType rt);

        public IEnumerable<Room> ReadRooms(List<RoomType> types);

        IEnumerable<Room> ReadRoomsWithName(string name);

        public IEnumerable<Room> FilterRoomsLowOnEquipment(string searchText);

        public IEnumerable<Room> GetRoomsLowOnEquipment();

        public Room GetStorage();

        public void AddItemQuantityToStorage(Entry<Equipment> deliveredEquipment);

        public void AddItemQuantity(Room room, Entry<Equipment> deliveredEquipment);

        public void IncreaseItemQuantity(Room room, Entry<Equipment> deliveredEquipment);

    }
}