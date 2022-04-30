﻿using HealthInstitution.Commands;
using HealthInstitution.Model;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthInstitution.ViewModel
{
    public class RoomsCRUDViewModel : ViewModelBase
    {
        public readonly IRoomService roomService;
        public readonly IAppointmentService appointmentService;

        public ICommand? ViewRoomEquipmentCommand { get; }

        public ICommand? CreateRoomCommand { get; }

        public ICommand? OpenUpdateRoomCommand { get; }

        public ICommand? DeleteRoomCommand { get; }

        private List<Room> _rooms;

        public List<Room> Rooms
        {
            get => _rooms;
            set
            {
                _rooms = value;
                OnPropertyChanged(nameof(Rooms));
            }
        }

        private Room _selectedRoom;
        public Room SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                _selectedRoom = value;
                GlobalStore.AddObject("SelectedRoom", value);
                OnPropertyChanged(nameof(SelectedRoom));
            }
        }

        public RoomsCRUDViewModel(IRoomService roomService, IAppointmentService appointmentService)
        {
            roomService = roomService;
            appointmentService = appointmentService;
            
            ViewRoomEquipmentCommand = new ViewRoomEquipmentCommand();
            CreateRoomCommand = new CreateRoomCommand();
            OpenUpdateRoomCommand = new OpenUpdateRoomCommand();
            DeleteRoomCommand = new DeleteRoomCommand(appointmentService, roomService);
            
            Rooms = roomService.ReadAll().ToList();
            
        }
    }
}
