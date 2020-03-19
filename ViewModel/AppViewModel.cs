using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Project.ViewModel
{
    class AppViewModel : INotifyPropertyChanged
    {
        private IAppModel model;

        private double rudder, elevator, aileron, throttle;

        public double VM_Rudder
        {
            get { return rudder; }
            set
            {
                rudder = value;
                model.controlJoystick(rudder, elevator);
            }
        }
        public double VM_Elevator
        {
            get { return elevator; }
            set
            {
                elevator = value;
                model.controlJoystick(rudder, elevator);
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
            model.PropertyChanged +=
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
