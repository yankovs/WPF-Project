﻿using FlightSimulatorApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for Map.xaml
    /// </summary>
    public partial class Map : UserControl
    {
        MapViewModel mvm;
        public Map()
        {
            InitializeComponent();
            DataContext = (Application.Current as App).MapViewModel;
            mvm = DataContext as MapViewModel;
        }

        private void VisibilityMode_TextChanged(object sender, TextChangedEventArgs e)
        {
            string vis = VisibilityMode.Text;
            if (mvm.VM_ConnectionMode == "Connected")
            {
                //it means the app is connected to server, so the map is also connected
                if (vis == "Hidden")
                {
                    Map1.Visibility = Visibility.Hidden;
                    Pikachu.Visibility = Visibility.Visible;
                    MapErrorMsg.Visibility = Visibility.Visible;
                }
                else
                {
                    Pikachu.Visibility = Visibility.Hidden;
                    MapErrorMsg.Visibility = Visibility.Hidden;
                    Map1.Visibility = Visibility.Visible;
                }
            }
            else
            {
                if (Pikachu != null)
                {
                    if (Pikachu.Visibility == Visibility.Visible)
                    {
                        Pikachu.Visibility = Visibility.Hidden;
                        MapErrorMsg.Visibility = Visibility.Hidden;
                        Map1.Visibility = Visibility.Visible;
                    }
                }
            }
        }
    }
}
