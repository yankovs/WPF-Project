using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.Maps.MapControl.WPF;
using System.Windows;

namespace FlightSimulatorApp.Model
{
    public interface IAppModel : INotifyPropertyChanged
    {
        /*Position properties for map*/
        double PositionLongitudeDeg { set; get; } // /position/longitude-deg
        double PositionLatitudeDeg { set; get; } // /position/latitude-deg
        Location Location { set; get; }
        string VisibilityOfMap { set; get; }
        /*Controllers properties*/
        double Rudder { set; get; } // "/controls/flight/rudder"
        double Elevator { set; get; } // "/controls/flight/elevator"
        double Aileron { set; get; } // /controls/flight/aileron
        double Throttle { set; get; } // /controls/engines/engine/throttle
        /*Dashboard properties*/
        double IndicatedHeadingDeg { set; get; } // /instrumentation/heading-indicator/indicated-heading-deg
        double GpsIndicatedVerticalSpeed { set; get; } // /instrumentation/gps/indicated-vertical-speed
        double GpsIndicatedGroundSpeedKt { set; get; } // /instrumentation/gps/indicated-ground-speed-kt
        double AirspeedIndicatorIndicatedSpeedKt { set; get; } // /instrumentation/airspeed-indicator/indicated-speed-kt
        double GpsIndicatedAltitudeFt { set; get; } // /instrumentation/gps/indicated-altitude-ft
        double AttitudeIndicatorInternalRollDeg { set; get; } // /instrumentation/attitude-indicator/internal-roll-deg
        double AttitudeIndicatorInternalPitchDeg { set; get; } // /instrumentation/attitude-indicator/internal-pitch-deg
        double AltimeterIndicatedAltitudeFt { set; get; } // /instrumentation/altimeter/indicated-altitude-ft

        /*returns whether it's connected to server or not, (for content of button)*/
        string ConnectionMode { set; get; }
        string IsError { set; get; }

        /* IP and Port */
        string IP { set; get; }
        int Port { set; get; }

        void NotifyPropertyChanged(string propName);
        void ControlAileron(double a);
        void ControlThrottle(double t);
    }
}
