using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPF_Project.Server
{
    public class MyServer : IServer
    {
        TcpClient client = null;
        NetworkStream ns = null;
        StreamWriter sw = null;
        StreamReader sr = null;

        volatile bool connected = false;
        volatile bool writing = false;
        volatile bool reading = false;

        public void Connect(string ip, int port)
        {
            if (!IsConnected())
            {
                try
                {
                    this.client = new TcpClient();
                    this.client.SendTimeout = 10000;
                    this.client.ReceiveTimeout = 10000;
                    this.client.Connect(ip, port);
                    connected = true;
                    this.ns = client.GetStream();
                    this.sw = new StreamWriter(ns);
                    this.sw.BaseStream.WriteTimeout = 10000;
                    this.sr = new StreamReader(ns);
                    this.sr.BaseStream.ReadTimeout = 10000;
                }
                catch (SocketException)
                {
                    throw new Exception("Unable to connect");
                }
            }

        }

        public void Write(string command)
        {
            if (client != null && IsConnected())
            {
                try
                {
                    writing = true;
                    this.sw.WriteLine(command);
                    this.sw.Flush();
                    writing = false;
                }
                catch (Exception e)
                {
                    writing = false;
                    Console.WriteLine(e.Message);
                    this.sw.Flush();
                    if (e.Message.Contains("the connected party did not properly respond after a period of time, or established " +
                        "connection failed because connected host has failed to respond."))
                    {                        
                        throw new Exception("Timeout (writing)");
                    }
                    else if (e.Message.Contains("An existing connection was forcibly closed by the remote host."))
                    {                       
                        throw new Exception("Server's disconnection");
                    }
                    else if (e.Message.Contains("A blocking operation was interrupted by a call to WSACancelBlockingCall."))
                    {                        
                        throw new Exception("User's disconnection while using the server");
                    }
                    else
                    {                        
                        throw new Exception("Unable to read");
                    }                   
                }
            }
            else
            {
                throw new Exception("Client is null");
            }
        }

        public string Read()
        {
            if (client != null && IsConnected())
            {
                try
                {
                    reading = true;
                    string line = sr.ReadLine();
                    Console.WriteLine(line);
                    reading = false;
                    return line;                    
                }
                catch (Exception e)
                {
                    reading = false;
                    Console.WriteLine(e.Message);
                    if (e.Message.Contains("the connected party did not properly respond after a period of time, or established " +
                        "connection failed because connected host has failed to respond."))
                    {                        
                        throw new Exception("Timeout (reading)");
                    }
                    else if (e.Message.Contains("An existing connection was forcibly closed by the remote host."))
                    {                        
                        throw new Exception("Server's disconnection");
                    }
                    else if(e.Message.Contains("A blocking operation was interrupted by a call to WSACancelBlockingCall."))
                    {                       
                        throw new Exception("User's disconnection while using the server");
                    }
                    else
                    {                        
                        throw new Exception("Unable to read");
                    }
                }
            }
            else
            {
                throw new Exception("Client is null");
            }
        }

        public void Disconnect()
        {
            if (IsConnected())
            {                
                sw.Close();
                sr.Close();
                ns.Close();
                client.Close();
                //if disconnection happens while writing/reading, it's still "connected"
                if(!IsWriting() && !IsReading())
                {
                    connected = false;
                }                
                
            }
        }

        public bool IsConnected()
        {
            return connected;
        }

        public bool IsWriting()
        {
            return writing;
        }

        public bool IsReading()
        {
            return reading;
        }

    }
}
