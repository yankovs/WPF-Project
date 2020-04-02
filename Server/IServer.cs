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
        void write(string command);
        string read();
        void disconnect();
        bool isConnected();
        bool isWriting();
        bool isReading();
    }
}
