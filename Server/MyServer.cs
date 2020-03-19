using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WPF_Project.Model;

namespace WPF_Project.Server
{
    class MyServer : IServer
    {
        TcpListener server = null;
        TcpClient client = null;
        StreamReader ns;
        public void Connect(ISettingsModel settings)
        {
            try
            {
                string ip = settings.IP;
                int destPort = settings.destPort;
                this.server = new TcpListener(System.Net.IPAddress.Parse(ip), destPort);

                this.server.Start();
                this.client = this.server.AcceptTcpClient();
                this.ns = new StreamReader(this.client.GetStream());
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
                    //StreamWriter sw = new StreamWriter();
                    //sw.WriteLine(command);
                    //sw.Flush();
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
                    return ns.ReadLine();           
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
