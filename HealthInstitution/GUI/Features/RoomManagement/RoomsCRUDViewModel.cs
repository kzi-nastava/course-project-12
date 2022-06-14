﻿using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HealthInstitution.Core.Features.RoomManagement.Commands.ManagerCMD;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Services;
using HealthInstitution.Core.Services.Interfaces;
using HealthInstitution.GUI.Utility.Navigation;
using HealthInstitution.GUI.Utility.ViewModel;

namespace HealthInstitution.GUI.Features.RoomManagement
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

            SelectedRoom = null;
            ViewRoomEquipmentCommand = new ViewRoomEquipmentCommand(this);
            CreateRoomCommand = new CreateRoomCommand();
            OpenUpdateRoomCommand = new OpenUpdateRoomCommand(this);
            DeleteRoomCommand = new DeleteRoomCommand(this, appointmentService, roomService);

            Rooms = roomService.ReadAll().ToList();
            Rooms = Rooms.OrderBy(x => x.Name).ToList();

        }
    }
}