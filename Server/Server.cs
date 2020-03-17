using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Project.Server
{
    interface Server
    {
        void Connect();

        void CloseConnection();

        TcpClient GetClient();
    }
}
