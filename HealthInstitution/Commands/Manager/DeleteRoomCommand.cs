﻿using HealthInstitution.Model;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;
using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthInstitution.Commands
{
    public class DeleteRoomCommand : CommandBase
    {
        private readonly RoomsCRUDViewModel? _viewModel;
        private Room _selectedRoom;
        private IAppointmentService _appointmentService;
        private IRoomService _roomService;

        public DeleteRoomCommand(RoomsCRUDViewModel viewModel, IAppointmentService appointmentService, IRoomService roomService)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
            _appointmentService = appointmentService;
            _roomService = roomService;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.SelectedRoom))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.SelectedRoom != null;
        }

        public override void Execute(object? parameter)
        {
            _selectedRoom = GlobalStore.ReadObject<Room>("SelectedRoom");
            var apts = _appointmentService.ReadRoomAppointments(_selectedRoom);
            

            if (_selectedRoom.Inventory.Count() != 0)
            {
                MessageBox.Show("Room with that name has equipment!");
            }
            else if (apts.Count() != 0)
            {
                MessageBox.Show("Room with that name has appointments!");
            }
            else 
            {
                _roomService.Delete(_selectedRoom.Id);
                MessageBox.Show("Room deleted successfully!");
                EventBus.FireEvent("RoomsOverview");
            }

        }
    }
}