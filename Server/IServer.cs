using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Project.Server
{
    public interface IServer
    {
        void Connect(string ip, int port);
        void Write(string command);
        string Read();
        void Disconnect();
        bool IsConnected();
        bool IsWriting();
        bool IsReading();
    }
}
