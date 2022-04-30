﻿using HealthInstitution.Model;
using HealthInstitution.Utility;
using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace HealthInstitution.Commands
{
    public class ConfirmArrangementCommand : CommandBase
    {
        private DateTime _arrangeTime;
        private DateTime _nowTime;
        private readonly ArrangeEquipmentViewModel? _viewModel;
        public ConfirmArrangementCommand(ArrangeEquipmentViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _arrangeTime = GlobalStore.ReadObject<DateTime>("ArrangeTime");
            _nowTime = DateTime.Now;

            if (_arrangeTime < _nowTime)
            {
                updateRoomEquipment();
                EventBus.FireEvent("ArrangeEquipment");
            }
            else
            {
                MessageBox.Show("kasnije");
                Thread t = new Thread(new ThreadStart(timerStart));
                t.Start();
                t.Join();
            }
        }

        private double getTimeDifference()
        {
            TimeSpan timeDifference = _arrangeTime - _nowTime;
            return timeDifference.TotalMilliseconds;
        }

        private void timerStart()
        {
            
            double milliSecs = getTimeDifference();
            MessageBox.Show(milliSecs.ToString());
            System.Timers.Timer timer = new System.Timers.Timer(milliSecs);
            timer.AutoReset = false;
            timer.Enabled = true;
            timer.Elapsed += updateDatebaseOnTime;
        }

        public void updateRoomEquipment()
        {
            _viewModel.Room1.Inventory.Clear();
            foreach (var item in _viewModel.Inventory1)
            {
                _viewModel.Room1.Inventory.Add(item);
            }
            bool b1 = true;
            foreach (var item in _viewModel.Room1.Inventory)
            {
                if (item.Quantity != 0)
                {
                    b1 = false;
                    break;
                }
            }
            if (b1)
            {
                _viewModel.Room1.Inventory.Clear();
            }
            _viewModel._roomService.Update(_viewModel.Room1);
           
            _viewModel.Room2.Inventory.Clear();
            foreach (var item in _viewModel.Inventory2)
            {
                _viewModel.Room2.Inventory.Add(item);
            }
            bool b2 = true;
            foreach (var item in _viewModel.Room2.Inventory)
            {
                if (item.Quantity != 0)
                {
                    b2 = false;
                    break;
                }
            }
            if (b2)
            {
                _viewModel.Room2.Inventory.Clear();
            }
            _viewModel._roomService.Update(_viewModel.Room2);

            MessageBox.Show("Database has been updated with arrangement changes.");

        }

        public void updateDatebaseOnTime(object source, ElapsedEventArgs e)
        {
            updateRoomEquipment();
        }
    }
}