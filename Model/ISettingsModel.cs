using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Project.Model
{
    public interface ISettingsModel
    {
        string IP { get; set; }
        int sourcePort { get; set; }
        int destPort { get; set; }
    }
}
