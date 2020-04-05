using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.Threading.Tasks;
using System.Windows;
using WPF_Project.Server;
using System.Diagnostics;

namespace WPF_Project
{
    public class AppModel : IAppModel
    {
        private double positionLongitudeDeg;
        private double positionLatitudeDeg;
        private Location location;
        private string visibilityOfMap;
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

        private string connectionMode;
        private string isError;

        private string ip;
        private int port;

        private const double Ratio = 168.421052631579;

        public Queue<int> queueSets;

        private volatile Stopwatch timerForException;
        private volatile bool reachedToTimeout;

        IServer server;
        volatile Boolean stop;

        public void stopModel()
        {
            this.stop = true;
            if (ConnectionMode == "Connected" || ConnectionMode == "Start Of App")
            {
                ConnectionMode = "Disconnected";
            }
            Location = new Location(32.009444, 34.876944); //default - location of Ben Gurion Airport
            VisibilityOfMap = "Visible";
        }

        public void startModel()
        {
            this.stop = false;
            IsError = "No";
            if (ConnectionMode != "Connected")
            {
                ConnectionMode = "Connected";
            }
            //reseting queue if needed
            if (queueSets != null)
            {
                if (queueSets.Count != 0)
                {
                    queueSets.Clear();
                }
            }
        }
        public bool getStop()
        {
            return this.stop;
        }

        public string ConnectionMode
        {
            get
            {
                return connectionMode;
            }
            set
            {
                connectionMode = value;
                NotifyPropertyChanged("ConnectionMode");
            }
        }

        public string IsError
        {
            get
            {
                return isError;
            }
            set
            {
                isError = value;
                NotifyPropertyChanged("IsError");
            }
        }


        public double PositionLongitudeDeg
        {
            get { return positionLongitudeDeg; }
            set
            {
                if (value >= -90 && value <= 90)
                {
                    positionLongitudeDeg = value;
                    NotifyPropertyChanged("PositionLongitudeDeg");
                    // "/position/longitude-deg"
                }
                else
                {
                    throw new Exception("Map problem");
                }

            }
        }
        public double PositionLatitudeDeg
        {
            get { return positionLatitudeDeg; }
            set
            {
                if (value >= -180 && value <= 180)
                {
                    positionLatitudeDeg = value;
                    NotifyPropertyChanged("PositionLatitudeDeg");
                    // "/position/latitude-deg"
                }
                else
                {
                    throw new Exception("Map problem");
                }
            }
        }

        public Location Location
        {
            get { return location; }
            set
            {
                location = value;
                PositionLatitudeDeg = location.Latitude;
                PositionLongitudeDeg = location.Longitude;
                NotifyPropertyChanged("Location");
            }
        }

        public string VisibilityOfMap
        {
            get { return visibilityOfMap; }
            set
            {
                visibilityOfMap = value;
                NotifyPropertyChanged("VisibilityOfMap");
            }
        }

        public double Rudder
        {
            get { return rudder; }
            set
            {
                if (value != rudder)
                {
                    rudder = value / Ratio;
                    NotifyPropertyChanged("Rudder");
                    // "/controls/flight/rudder"
                }
            }
        }
        public double Elevator
        {
            get { return elevator; }
            set
            {
                if (value != elevator)
                {
                    elevator = -value / Ratio;
                    NotifyPropertyChanged("Elevator");
                    // "/controls/flight/elevator"
                }
            }
        }
        public double Aileron
        {
            get { return aileron; }
            set
            {
                aileron = value;
                NotifyPropertyChanged("Aileron");
                // "/controls/flight/aileron"
            }
        }
        public double Throttle
        {
            get { return throttle; }
            set
            {
                throttle = value;
                NotifyPropertyChanged("Throttle");
                // "/controls/engines/current-engine/throttle"
            }
        }
        public double IndicatedHeadingDeg
        {
            get { return indicatedHeadingDeg; }
            set
            {
                indicatedHeadingDeg = value;
                NotifyPropertyChanged("IndicatedHeadingDeg");
                // "/instrumentation/heading-indicator/indicated-heading-deg"
            }
        }
        public double GpsIndicatedVerticalSpeed
        {
            get { return gpsIndicatedVerticalSpeed; }
            set
            {
                gpsIndicatedVerticalSpeed = value;
                NotifyPropertyChanged("GpsIndicatedVerticalSpeed");
                // "/instrumentation/gps/indicated-vertical-speed"
            }
        }
        public double GpsIndicatedGroundSpeedKt
        {
            get { return gpsIndicatedGroundSpeedKt; }
            set
            {
                gpsIndicatedGroundSpeedKt = value;
                NotifyPropertyChanged("GpsIndicatedGroundSpeedKt");
                // "/instrumentation/gps/indicated-ground-speed-kt"
            }
        }
        public double AirspeedIndicatorIndicatedSpeedKt
        {
            get { return airspeedIndicatorIndicatedSpeedKt; }
            set
            {
                airspeedIndicatorIndicatedSpeedKt = value;
                NotifyPropertyChanged("AirspeedIndicatorIndicatedSpeedKt");
                // "/instrumentation/airspeed-indicator/indicated-speed-kt"
            }
        }
        public double GpsIndicatedAltitudeFt
        {
            get { return gpsIndicatedAltitudeFt; }
            set
            {
                gpsIndicatedAltitudeFt = value;
                NotifyPropertyChanged("GpsIndicatedAltitudeFt");
                // "/instrumentation/gps/indicated-altitude-ft"
            }
        }
        public double AttitudeIndicatorInternalRollDeg
        {
            get { return attitudeIndicatorInternalRollDeg; }
            set
            {
                attitudeIndicatorInternalRollDeg = value;
                NotifyPropertyChanged("AttitudeIndicatorInternalRollDeg");
                // "/instrumentation/attitude-indicator/internal-roll-deg"
            }
        }
        public double AttitudeIndicatorInternalPitchDeg
        {
            get { return attitudeIndicatorInternalPitchDeg; }
            set
            {
                attitudeIndicatorInternalPitchDeg = value;
                NotifyPropertyChanged("AttitudeIndicatorInternalPitchDeg");
                // "/instrumentation/attitude-indicator/internal-pitch-deg"
            }
        }
        public double AltimeterIndicatedAltitudeFt
        {
            get { return altimeterIndicatedAltitudeFt; }
            set
            {
                altimeterIndicatedAltitudeFt = value;
                NotifyPropertyChanged("AltimeterIndicatedAltitudeFt");
                // "/instrumentation/altimeter/indicated-altitude-ft"
            }
        }

        public string IP
        {
            get { return ip; }
            set
            {
                ip = value;
                NotifyPropertyChanged("IP");
            }
        }

        public int Port
        {
            get { return port; }
            set
            {
                port = value;
                NotifyPropertyChanged("Port");
            }
        }

        //dealing with property changing
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {

                //dealing with set commands:
                if (queueSets != null)
                {
                    if (propName == "Rudder" && !queueSets.Contains(1))
                    {
                        queueSets.Enqueue(1);
                    }
                    if (propName == "Elevator" && !queueSets.Contains(2))
                    {
                        queueSets.Enqueue(2);
                    }
                    if (propName == "Aileron" && !queueSets.Contains(3))
                    {
                        queueSets.Enqueue(3);
                    }
                    if (propName == "Throttle" && !queueSets.Contains(4))
                    {
                        queueSets.Enqueue(4);
                    }
                }

                //dealing with server connectivity
                if (propName == "ConnectionMode")
                {
                    if (ConnectionMode == "Connected")
                    {
                        try
                        {                            
                            if (timerForException.ElapsedMilliseconds > 0 && timerForException.ElapsedMilliseconds < 10000)
                            {
                                throw new Exception("server is still connected");
                            }                            
                            else if (timerForException.ElapsedMilliseconds >= 10000)
                            {
                                timerForException.Reset();                                                             
                            }
                            reachedToTimeout = false;
                            if (getStop())
                            {
                                startModel();
                            }
                            if (IP != ConfigurationManager.AppSettings["IP"] || Port != int.Parse(ConfigurationManager.AppSettings["Port"]))
                            {
                                connect(IP, Port);
                            }
                            else
                            {
                                connect(ConfigurationManager.AppSettings["IP"],
                                               int.Parse(ConfigurationManager.AppSettings["Port"]));
                            }
                            start();
                        }
                        catch (Exception e)
                        {                            
                            if (e.Message == "server is still connected")
                            {                               
                                if(!reachedToTimeout)
                                {
                                    timerForException.Reset();
                                    IsError = e.Message;
                                }
                                else
                                {
                                    IsError = "Yes";
                                    reachedToTimeout = true;
                                }
                            }
                            else
                            {
                                IsError = "Yes";
                            }
                            disconnect();
                            stopModel();
                        }
                    }
                    else if (ConnectionMode == "Disconnected")
                    {                                                
                        disconnect();
                        stopModel();
                    }
                }
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public AppModel(IServer server)
        {
            this.server = server;
            ConnectionMode = "Start Of App";
            //Default IP and Port
            IP = ConfigurationManager.AppSettings["IP"];
            Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
            stopModel();
            Location = new Location(32.009444, 34.876944); //default - location of Ben Gurion Airport
            VisibilityOfMap = "Visible";
            queueSets = new Queue<int>();
            timerForException = new Stopwatch();            
            reachedToTimeout = false;
        }

        public void connect(string ip, int port)
        {
            server.Connect(ip, port);
        }

        public void disconnect()
        {
            stop = true;
            server.disconnect();
        }

        public void start()
        {
            new Thread(delegate ()
            {
                String r;
                while (!stop)
                {
                    try
                    {
                        //if ERR value is sent from server, Double.NaN it'll be instead of a number

                        //Dashboard:                        
                        server.write("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                        r = server.read();
                        if (r != "ERR")
                        {
                            IndicatedHeadingDeg = Math.Round(Double.Parse(r), 6);
                        }
                        else
                        {
                            IndicatedHeadingDeg = Double.NaN;
                        }

                        server.write("get /instrumentation/gps/indicated-vertical-speed\n");
                        r = server.read();
                        if (r != "ERR")
                        {
                            GpsIndicatedVerticalSpeed = Math.Round(Double.Parse(r), 6);
                        }
                        else
                        {
                            GpsIndicatedVerticalSpeed = Double.NaN;
                        }


                        server.write("get /instrumentation/gps/indicated-ground-speed-kt\n");
                        r = server.read();
                        if (r != "ERR")
                        {
                            GpsIndicatedGroundSpeedKt = Math.Round(Double.Parse(r), 6);
                        }
                        else
                        {
                            GpsIndicatedGroundSpeedKt = Double.NaN;
                        }


                        server.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                        r = server.read();
                        if (r != "ERR")
                        {
                            AirspeedIndicatorIndicatedSpeedKt = Math.Round(Double.Parse(r), 6);
                        }
                        else
                        {
                            AirspeedIndicatorIndicatedSpeedKt = Double.NaN;
                        }


                        server.write("get /instrumentation/gps/indicated-altitude-ft\n");
                        r = server.read();
                        if (r != "ERR")
                        {
                            GpsIndicatedAltitudeFt = Math.Round(Double.Parse(r), 6);
                        }
                        else
                        {
                            GpsIndicatedAltitudeFt = Double.NaN;
                        }


                        server.write("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                        r = server.read();
                        if (r != "ERR")
                        {
                            AttitudeIndicatorInternalRollDeg = Math.Round(Double.Parse(r), 6);
                        }
                        else
                        {
                            AttitudeIndicatorInternalRollDeg = Double.NaN;
                        }

                        server.write("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                        r = server.read();
                        if (r != "ERR")
                        {
                            AttitudeIndicatorInternalPitchDeg = Math.Round(Double.Parse(r), 6);
                        }
                        else
                        {
                            AttitudeIndicatorInternalPitchDeg = Double.NaN;
                        }

                        server.write("get /instrumentation/gps/indicated-altitude-ft\n");
                        r = server.read();
                        if (r != "ERR")
                        {
                            AltimeterIndicatedAltitudeFt = Math.Round(Double.Parse(r), 6);
                        }
                        else
                        {
                            AltimeterIndicatedAltitudeFt = Double.NaN;
                        }


                        //Controllers:
                        //sets are only sent if needed
                        if (queueSets.Count != 0 && queueSets.Peek() == 1)
                        {
                            server.write("set /controls/flight/rudder " + Rudder + "\n");
                            r = server.read();
                            if (r != "ERR")
                            {
                                Rudder = Math.Round(Double.Parse(r), 6);
                            }
                            queueSets.Dequeue();
                        }
                        if (queueSets.Count != 0 && queueSets.Peek() == 2)
                        {
                            server.write("set /controls/flight/elevator " + Elevator + "\n");
                            r = server.read();
                            if (r != "ERR")
                            {
                                Elevator = Math.Round(Double.Parse(r), 6);
                            }
                            queueSets.Dequeue();
                        }
                        if (queueSets.Count != 0 && queueSets.Peek() == 3)
                        {
                            server.write("set /controls/flight/aileron " + Aileron + "\n");
                            r = server.read();
                            if (r != "ERR")
                            {
                                Aileron = Math.Round(Double.Parse(r), 6);
                            }
                            queueSets.Dequeue();
                        }
                        if (queueSets.Count != 0 && queueSets.Peek() == 4)
                        {
                            server.write("set /controls/engines/current-engine/throttle " + Throttle + "\n");
                            r = server.read();
                            if (r != "ERR")
                            {
                                Throttle = Math.Round(Double.Parse(r), 6);
                            }
                            queueSets.Dequeue();
                        }

                        if (VisibilityOfMap == "Hidden")
                        {
                            Thread.Sleep(25);
                        }

                        //Position:
                        //try-catch blocks try to distinguish between map and connectivity problems                       
                        server.write("get /position/longitude-deg\n");
                        r = server.read();
                        try
                        {
                            if (r != "ERR")
                            {
                                PositionLongitudeDeg = Math.Round(Double.Parse(r), 6);
                            }
                            else
                            {
                                throw new Exception("Map problem");
                            }
                        }
                        catch (Exception e)
                        {
                            if (e.Message == "Map problem")
                            {
                                if (VisibilityOfMap != "Hidden")
                                {
                                    VisibilityOfMap = "Hidden";
                                }
                                continue;
                            }
                            else
                            {
                                throw e;
                            }
                        }

                        server.write("get /position/latitude-deg\n");
                        r = server.read();
                        try
                        {
                            if (r != "ERR")
                            {
                                PositionLatitudeDeg = Math.Round(Double.Parse(r), 6);
                            }
                            else
                            {
                                throw new Exception("Map problem");
                            }
                        }
                        catch (Exception e)
                        {
                            if (e.Message == "Map problem")
                            {
                                if (VisibilityOfMap != "Hidden")
                                {
                                    VisibilityOfMap = "Hidden";
                                }
                                continue;
                            }
                            else
                            {
                                throw e;
                            }
                        }

                        //if we reached to this part, location is fine

                        Location = new Location(PositionLatitudeDeg, PositionLongitudeDeg); //updating location      

                        if (VisibilityOfMap == "Visible")
                        {
                            Thread.Sleep(25);
                        }
                        else
                        {
                            //reachable only after Location is fine again
                            VisibilityOfMap = "Visible";
                        }
                    }
                    catch (Exception e)
                    {
                        if (e.Message == "Timeout (writing)" || e.Message == "Timeout (reading)")
                        {
                            IsError = "Timeout";
                            reachedToTimeout = true;
                        }
                        else if (e.Message == "Server's disconnection")
                        {
                            IsError = "Server's disconnection";
                        }
                        else if (e.Message == "User's disconnection while using the server")
                        {
                            IsError = "User's disconnection while using the server";
                            //the timer is used for denying the client to connect again (till the server stops running)
                            if(!reachedToTimeout)
                            {
                                timerForException.Start();                                
                            }                            
                        }
                        else
                        {
                            server.disconnect();
                            stopModel();
                        }
                    }
                }
            }).Start();
        }

        public void controlAileron(double a)
        {
            if (a > 1)
            {
                Aileron = 1;
            }
            else if (a < -1)
            {
                Aileron = -1;
            }
            else
            {
                Aileron = a;
            }
        }

        public void controlThrottle(double t)
        {
            if (t > 1)
            {
                Throttle = 1;
            }
            else if (t < 0)
            {
                Throttle = 0;
            }
            else
            {
                Throttle = t;
            }
        }
    }
}
