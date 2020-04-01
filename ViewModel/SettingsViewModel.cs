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
        private string IP;
        private int Port;
        public string VM_IP
        {
            get
            {
                return IP;
            }
            set
            {
                IP = value;
                model.IP = IP;
            }
        }

        public int VM_Port
        {
            get
            {
                return Port;
            }
            set
            {
                Port = value;
                model.Port = Port;
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
