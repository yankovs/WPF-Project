using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Project.ViewModel
{
    public class MapViewModel : INotifyPropertyChanged
    {
        private IAppModel model;

        /* Properties in any ViewModel are the same as in Model but with "VM_" prefix
           which is exactly the same as shown in week 4.
           We are aware that those names aren't like it should've been, code conventions wise. */

        public double VM_PositionLongitudeDeg
        {
            get { return model.PositionLongitudeDeg; }
        }
        public double VM_PositionLatitudeDeg
        {
            get { return model.PositionLatitudeDeg; }
        }
        public Location VM_Location
        {
            get { return model.Location; }
        }
        //needed for airplane's direction
        public double VM_IndicatedHeadingDeg
        {
            get { return model.IndicatedHeadingDeg; }
        }
        //Those properties are helpful for deciding whether to show map or not
        public string VM_VisibilityOfMap
        {
            get { return model.VisibilityOfMap; }
        }
        public string VM_ConnectionMode
        {
            get { return model.ConnectionMode; }
        }

        public MapViewModel(IAppModel model)
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
