using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Project.ViewModel
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private IAppModel model;
        private string ip;
        private int port;

        /* Properties in any ViewModel are the same as in Model but with "VM_" prefix
           which is exactly the same as shown in week 4.
           We are aware that those names aren't like it should've been, code conventions wise. */

        public string VM_IP
        {
            get
            {
                return ip;
            }
            set
            {
                ip = value;
                model.IP = ip;
            }
        }

        public int VM_Port
        {
            get
            {
                return port;
            }
            set
            {
                port = value;
                model.Port = port;
            }
        }

        public SettingsViewModel(IAppModel model)
        {
            this.model = model;
            this.model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
