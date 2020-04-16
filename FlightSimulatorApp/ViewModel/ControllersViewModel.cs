using FlightSimulatorApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModel
{
    public class ControllersViewModel : INotifyPropertyChanged
    {
        private IAppModel model;

        private double rudder, elevator, aileron, throttle;

        private const double Ratio = 170; //not accurate but relatively good enough

        /* Properties in any ViewModel are the same as in Model but with "VM_" prefix
           which is exactly the same as shown in week 4.
           We are aware that those names aren't like it should've been, code conventions wise. */

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

        //Those "real" properties are only for showing on screen real values, they aren't in Model.
        public double VM_RealRudder
        {
            get { return VM_Rudder / Ratio; }
        }

        public double VM_RealElevator
        {
            get { return -VM_Elevator / Ratio; }
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

            if (propName == "VM_Rudder")
            {
                NotifyPropertyChanged("VM_RealRudder");
            }
            else if (propName == "VM_Elevator")
            {
                NotifyPropertyChanged("VM_RealElevator");
            }
        }
    }
}
