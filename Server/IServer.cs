using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WPF_Project.Model;

namespace WPF_Project.Server
{
    interface IServer
    {
        void Connect(ISettingsModel settings);
        void write(string command);
        string read();
        void CloseConnection();
        TcpClient GetClient();
    }
}
