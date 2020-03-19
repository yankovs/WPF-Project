using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WPF_Project.Server;

namespace WPF_Project
{
    class AppModel : IAppModel
    {
        private double positionLongitudeDeg;
        private double positionLatitudeDeg;
        private double rudder;
        private double elevator;
        private double aileron;
        private double throttle;
        private double indicatedHeadingDeg;
        private double gpsIndicatedVerticalSpeed;
        private double gpsIndicatedGroundSpeedKt;
        private double airspeedIndicatorIndicatedSpeedKt;
        private double gpsIndicatedAltitudeFt;
        private double attitudeIndicatorInternalRollDeg;
        private double attitudeIndicatorInternalPitchDeg;
        private double altimeterIndicatedAltitudeFt;

        IServer server;
        volatile Boolean stop;

        public double PositionLongitudeDeg
        {
            get { return positionLongitudeDeg; }
            set
            {
                positionLongitudeDeg = value;
                NotifyPropertyChanged("/position/longitude-deg");
            }
        }
        public double PositionLatitudeDeg
        {
            get { return positionLatitudeDeg; }
            set
            {
                positionLatitudeDeg = value;
                NotifyPropertyChanged("/position/latitude-deg");
            }
        }
        public double Rudder
        {
            get { return rudder; }
            set
            {
                rudder = value;
                NotifyPropertyChanged("/controls/flight/rudder");
            }
        }
        public double Elevator
        {
            get { return elevator; }
            set
            {
                elevator = value;
                NotifyPropertyChanged("/controls/flight/elevator");
            }
        }
        public double Aileron
        {
            get { return aileron; }
            set
            {
                aileron = value;
                NotifyPropertyChanged("/controls/flight/aileron");
            }
        }
        public double Throttle
        {
            get { return throttle; }
            set
            {
                throttle = value;
                NotifyPropertyChanged("/controls/engines/engine/throttle");
            }
        }
        public double IndicatedHeadingDeg
        {
            get { return indicatedHeadingDeg; }
            set
            {
                indicatedHeadingDeg = value;
                NotifyPropertyChanged("/instrumentation/heading-indicator/indicated-heading-deg");
            }
        }
        public double GpsIndicatedVerticalSpeed
        {
            get { return gpsIndicatedVerticalSpeed; }
            set
            {
                gpsIndicatedVerticalSpeed = value;
                NotifyPropertyChanged("/instrumentation/gps/indicated-vertical-speed");
            }
        }
        public double GpsIndicatedGroundSpeedKt
        {
            get { return gpsIndicatedGroundSpeedKt; }
            set
            {
                gpsIndicatedGroundSpeedKt = value;
                NotifyPropertyChanged("/instrumentation/gps/indicated-ground-speed-kt");
            }
        }
        public double AirspeedIndicatorIndicatedSpeedKt
        {
            get { return airspeedIndicatorIndicatedSpeedKt; }
            set
            {
                airspeedIndicatorIndicatedSpeedKt = value;
                NotifyPropertyChanged("/instrumentation/airspeed-indicator/indicated-speed-kt");
            }
        }
        public double GpsIndicatedAltitudeFt
        {
            get { return gpsIndicatedAltitudeFt; }
            set
            {
                gpsIndicatedAltitudeFt = value;
                NotifyPropertyChanged("/instrumentation/gps/indicated-altitude-ft");
            }
        }
        public double AttitudeIndicatorInternalRollDeg
        {
            get { return attitudeIndicatorInternalRollDeg; }
            set
            {
                attitudeIndicatorInternalRollDeg = value;
                NotifyPropertyChanged("/instrumentation/attitude-indicator/internal-roll-deg");
            }
        }
        public double AttitudeIndicatorInternalPitchDeg
        {
            get { return attitudeIndicatorInternalPitchDeg; }
            set
            {
                attitudeIndicatorInternalPitchDeg = value;
                NotifyPropertyChanged("/instrumentation/attitude-indicator/internal-pitch-deg");
            }
        }
        public double AltimeterIndicatedAltitudeFt
        {
            get { return altimeterIndicatedAltitudeFt; }
            set
            {
                altimeterIndicatedAltitudeFt = value;
                NotifyPropertyChanged("/instrumentation/altimeter/indicated-altitude-ft");
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

        public AppModel(IServer server)
        {
            this.server = server;
            stop = false;
        }

        public void connect(string ip, int port)
        {
            server.Connect(ip, port);
        }

        public void disconnect()
        {
            stop = true;
            server.CloseConnection();
        }

        public void start()
        {
            new Thread(delegate ()
            {
                while (!stop)
                {

                }
            }).Start();
        }

        public void controlJoystick(double r, double e)
        {
            throw new NotImplementedException();
        }

        public void controlAileron(double a)
        {
            throw new NotImplementedException();
        }

        public void controlThrottle(double t)
        {
            throw new NotImplementedException();
        }
    }
}
