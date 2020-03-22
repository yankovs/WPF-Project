using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Project.Model
{
    interface IJoystickModel : INotifyPropertyChanged
    {
        double Rudder { set; get; } // /controls/flight/rudder
        double Elevator { set; get; } // /controls/flight/elevator   
        
        void controlJoystick(double r, double e);
    }
}
