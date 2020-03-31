using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Project.ViewModel
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private IAppModel model;

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

        public DashboardViewModel(IAppModel model)
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
