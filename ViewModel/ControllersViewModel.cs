﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Project.ViewModel
{
    public class ControllersViewModel : INotifyPropertyChanged
    {
        private IAppModel model;

        private double rudder, elevator, aileron, throttle;

        private const double Ratio = 168.421052631579;

        public double VM_Rudder
        {
            get { return rudder; }
            set
            {
                if (value != rudder && value != rudder / Ratio)
                {
                    rudder = value;
                    model.Rudder = rudder;
                }
            }
        }
        public double VM_Elevator
        {
            get { return elevator; }
            set
            {
                if (value != elevator && value != elevator / Ratio)
                {
                    elevator = value;
                    model.Elevator = elevator;
                }

            }
        }
        public double VM_Aileron
        {
            get { return aileron; }
            set
            {
                aileron = value;
                model.ControlAileron(aileron);
            }
        }
        public double VM_Throttle
        {
            get { return throttle; }
            set
            {
                throttle = value;
                model.ControlThrottle(throttle);
            }
        }
        
        public ControllersViewModel(IAppModel model)
        {
            this.model = model;
            this.model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
