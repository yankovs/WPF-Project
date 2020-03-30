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
    class MyServer : IServer
    {
        TcpClient client = null;
        NetworkStream ns = null;
        StreamWriter sw = null;
        StreamReader sr = null;

        bool connected = false;

        public void Connect(string ip, int port)
        {
            if (!isConnected())
            {
                try
                {
                    this.client = new TcpClient();
                    this.client.SendTimeout = 10000;
                    this.client.ReceiveTimeout = 10000;
                    this.client.Connect(ip, port);
                    this.ns = client.GetStream();
                    this.sw = new StreamWriter(ns);
                    this.sw.BaseStream.WriteTimeout = 10000;
                    this.sr = new StreamReader(ns);
                    this.sr.BaseStream.ReadTimeout = 10000;
                    connected = true;
                }
                catch (SocketException)
                {
                    throw new Exception("Unable to connect");
                }
            }

        }

        public void write(string command)
        {
            if (client != null)
            {
                try
                {
                    this.sw.WriteLine(command);
                    this.sw.Flush();
                }
                catch (Exception e)
                {
                    this.sw.Flush();
                    if (e.Message != "Unable to read data from the transport connection: A connection attempt failed" +
                        "because the connected party did not properly respond after a period of time, or established" +
                        " connection failed because connected host has failed to respond.")
                    {
                        throw new Exception("Unable to write");
                    }
                    else
                    {
                        throw new Exception("Timeout (writing)");
                    }
                }
            }
            else
            {
                throw new Exception("Client is null");
            }
        }

        public string read()
        {
            if (client != null)
            {
                try
                {
                    string line = sr.ReadLine();
                    Console.WriteLine(line);
                    return line;
                }
                catch (Exception e)
                {
                    sr.DiscardBufferedData();
                    if (e.Message != "Unable to read data from the transport connection: A connection attempt failed" +
                        "because the connected party did not properly respond after a period of time, or established" +
                        " connection failed because connected host has failed to respond.")
                    {
                        throw new Exception("Unable to read");
                    }
                    else
                    {
                        throw new Exception("Timeout (reading)");
                    }
                }
            }
            else
            {
                throw new Exception("Client is null");
            }
        }

        public void disconnect()
        {
            if (isConnected())
            {
                sw.Close();
                sr.Close();
                ns.Close();
                client.Close();
                connected = false;
            }
        }

        public bool isConnected()
        {
            return connected;
        }

    }
}
