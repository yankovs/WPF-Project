using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Project.Server
{
    class MyServer : IServer
    {
        TcpClient client = null;
        NetworkStream ns;
        public void Connect(string ip, int port)
        {
            try
            {
                client = new TcpClient(ip, port);
                ns = client.GetStream();
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
                    StreamWriter sw = new StreamWriter(ns);
                    sw.WriteLine(command);
                    sw.Flush();
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
                    StreamReader sr = new StreamReader(ns);
                    return sr.ReadLine();                    
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

        public void CloseConnection()
        {
            if(client != null)
            {
                ns.Close();
                client.Close();
            }            
        }

        public TcpClient GetClient()
        {
            return client;
        }
    }
}
