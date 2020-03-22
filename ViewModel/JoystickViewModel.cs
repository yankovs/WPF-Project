using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Project.Model;

namespace WPF_Project.ViewModel
{
    class JoystickViewModel : INotifyPropertyChanged
    {
        private IJoystickModel joystickModel;
        private AppViewModel appViewModel;

        private double rudder, elevator;

        private const double Ratio = 168.421052631579;

        public double VM_Rudder
        {
            get { return rudder; }
            set
            {
                if(value != rudder && value != rudder/Ratio)
                {
                    rudder = value;
                    appViewModel.VM_Rudder = rudder;                                      
                }              
            }
        }
        public double VM_Elevator
        {
            get { return elevator; }
            set
            {
                if (value != elevator && value != -elevator/Ratio)
                {
                    elevator = value;
                    appViewModel.VM_Elevator = elevator;                    
                }                
            }
        }

        public JoystickViewModel(IJoystickModel joystickModel, AppViewModel appViewModel)
        {
            this.joystickModel = joystickModel;
            this.appViewModel = appViewModel;

            this.joystickModel.PropertyChanged +=
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
            appViewModel.NotifyPropertyChanged(propName);
        }
    }
}
