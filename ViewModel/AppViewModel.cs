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

        //first one is for view -> model
        public string VM_ConnectionMode
        {
            get { return conMode; }
            set
            {
                conMode = value;
                model.ConnectionMode = conMode;
                NotifyPropertyChanged("VM_ConnectionMode");
            }
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
