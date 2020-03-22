using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Project.Model
{
    class JoystickModel : IJoystickModel
    {
        private double rudder;
        private double elevator;

        private IAppModel appModel; //this joystick model has ref to general model

        private const double Ratio = 168.421052631579;

        public double Rudder
        {
            get
            {
                return rudder;
            }
            set
            {
                if(value != rudder)
                {
                    rudder = value / Ratio;
                    NotifyPropertyChanged("Rudder");                    
                    appModel.NotifyPropertyChanged("Rudder");
                }
               
                // "/controls/flight/rudder"
            }
        }
        public double Elevator
        {
            get { return elevator; }
            set
            {
                if(value != elevator)
                {
                    elevator = -value / Ratio;
                    NotifyPropertyChanged("Elevator");                    
                    appModel.NotifyPropertyChanged("Elevator");
                }
                
              
                // "/controls/flight/elevator"
            }
        }

        //dealing with property changing
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));                
            }
        }

        public JoystickModel(IAppModel model)
        {
            this.appModel = model;            
        }

        public void controlJoystick(double r, double e)
        {
            this.Rudder = r;
            this.Elevator = e;
        }
    }
}
