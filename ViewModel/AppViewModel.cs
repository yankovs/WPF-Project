using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_Project.ViewModel
{
    public class AppViewModel : INotifyPropertyChanged
    {
        private IAppModel model;

        private string conMode;

        /* Properties in any ViewModel are the same as in Model but with "VM_" prefix
           which is exactly the same as shown in week 4.
           We are aware that those names aren't like it should've been, code conventions wise. */

        public string VM_ConnectionMode
        {
            get { return conMode; }
            set
            {
                conMode = value;
                model.ConnectionMode = conMode;
            }
        }

        public string VM_IsError
        {
            get { return model.IsError; }
        }

        public AppViewModel(IAppModel model)
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
