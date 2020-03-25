using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Project.Model;

namespace WPF_Project.ViewModel
{
    class AppViewModel : INotifyPropertyChanged
    {
        private IAppModel model;

        private double aileron, throttle;

        private JoystickViewModel vm_joystick; //viewmodel of joystick inside general viewmodel

        private const double Ratio = 168.421052631579;

        public string VM_ConnectionButton
        {
            get { return model.ConnectionButton; }
        }

        public JoystickViewModel VM_JoystickModel
        {
            get { return vm_joystick; }
            set
            {
                vm_joystick.VM_Rudder = value.VM_Rudder;
                vm_joystick.VM_Elevator = value.VM_Elevator;

                model.controlJoystick(value.VM_Rudder, value.VM_Elevator);
            }
        }

        //properties related to joystick
        public double VM_Rudder
        {
            get { return VM_JoystickModel.VM_Rudder; }
            set
            {
                VM_JoystickModel.VM_Rudder = value / Ratio;
                model.controlJoystick(VM_JoystickModel.VM_Rudder, VM_JoystickModel.VM_Elevator);
            }
        }
        public double VM_Elevator
        {
            get { return VM_JoystickModel.VM_Elevator; }
            set
            {
                VM_JoystickModel.VM_Elevator = -value / Ratio;
                model.controlJoystick(VM_JoystickModel.VM_Rudder, VM_JoystickModel.VM_Elevator);
            }
        }

        public double VM_Aileron
        {
            get { return aileron; }
            set
            {
                aileron = value;
                model.controlAileron(aileron);
            }
        }
        public double VM_Throttle
        {
            get { return throttle; }
            set
            {
                throttle = value;
                model.controlThrottle(throttle);
            }
        }

        public double VM_PositionLongitudeDeg
        {
            get { return model.PositionLongitudeDeg; }
        }
        public double VM_PositionLatitudeDeg
        {
            get { return model.PositionLatitudeDeg; }
        }
        public Location VM_Location
        {
            get { return model.Location; }
        }

        public double VM_IndicatedHeadingDeg
        {
            get { return model.IndicatedHeadingDeg; }
        }
        public double VM_GpsIndicatedVerticalSpeed
        {
            get { return model.GpsIndicatedVerticalSpeed; }
        }
        public double VM_GpsIndicatedGroundSpeedKt
        {
            get { return model.GpsIndicatedGroundSpeedKt; }
        }
        public double VM_AirspeedIndicatorIndicatedSpeedKt
        {
            get { return model.AirspeedIndicatorIndicatedSpeedKt; }
        }
        public double VM_GpsIndicatedAltitudeFt
        {
            get { return model.GpsIndicatedAltitudeFt; }
        }
        public double VM_AttitudeIndicatorInternalRollDeg
        {
            get { return model.AttitudeIndicatorInternalRollDeg; }
        }
        public double VM_AttitudeIndicatorInternalPitchDeg
        {
            get { return model.AttitudeIndicatorInternalPitchDeg; }
        }
        public double VM_AltimeterIndicatedAltitudeFt
        {
            get { return model.AltimeterIndicatedAltitudeFt; }
        }

        public AppViewModel(IAppModel model)
        {
            this.model = model;
            this.vm_joystick = new JoystickViewModel(model.JoystickModel, this); //creating joystick part
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
