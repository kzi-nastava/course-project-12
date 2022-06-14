﻿using System.Collections.Generic;
using System.Windows.Input;
using HealthInstitution.Commands.manager;
using HealthInstitution.Model.room;
using HealthInstitution.Services.Interfaces;

namespace HealthInstitution.ViewModel.manager
{
    public class RoomCreateViewModel : ViewModelBase
    {
        public readonly IRoomService _roomService;
        public readonly IRoomRenovationService _roomRenovationService;
        public ICommand? AddRoomCommand { get; }

        private string _roomName;
        public string? RoomName
        {
            get => _roomName;
            set
            {
                _roomName = value;
                OnPropertyChanged(nameof(RoomName));
            }
        }

        private RoomType _selectedType;
        public RoomType SelectedType
        {
            get => _selectedType;
            set
            {
                _selectedType = value;
                OnPropertyChanged(nameof(SelectedType));
            }
        }

        private List<RoomType> _roomTypes;
        public List<RoomType> RoomTypes
        {
            get => _roomTypes;
            set
            {
                _roomTypes = value;
                OnPropertyChanged(nameof(RoomTypes));
            }
        }

        public RoomCreateViewModel(IRoomService roomService, IRoomRenovationService roomRenovationService)
        {
            _roomService = roomService;
            _roomRenovationService = roomRenovationService;
            SelectedType = RoomType.ExaminationRoom;
            RoomTypes = new List<RoomType>();
            RoomTypes.Add(RoomType.ExaminationRoom);
            RoomTypes.Add(RoomType.OperationRoom);
            RoomTypes.Add(RoomType.RestingRoom);
            AddRoomCommand = new AddRoomCommand(this);
        }
    }
}
