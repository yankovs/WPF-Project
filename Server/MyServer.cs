using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Project.Server
{
    class MyServer : IServer
    {
        TcpClient client = null;
        NetworkStream ns = null;
        StreamWriter sw = null;
        StreamReader sr = null;

        public void Connect(string ip, int port)
        {
            try
            {
                this.client = new TcpClient();
                client.Connect(IPAddress.Parse(ip), port);
                this.ns = client.GetStream();
                this.sw = new StreamWriter(ns);
                this.sr = new StreamReader(ns);
            }
            catch (SocketException)
            {
                throw new Exception("Unable to connect");
            }            
        }

        public void write(string command)
        {
            if(client != null)
            {
                try
                {
                    this.sw.WriteLine(command);
                    this.sw.Flush();
                }
                catch
                {
                    throw new Exception("Unable to write");
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
                catch
                {
                    throw new Exception("Unable to read");
                }
            }
            else
            {
                throw new Exception("Client is null");
            }
        }

        public void disconnect()
        {
            if(client != null)
            {
                ns.Close();
                sw.Close();
                sr.Close();
                client.Close();
            }            
        }
    }
}
