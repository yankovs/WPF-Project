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
    /// Interaction logic for Controllers.xaml
    /// </summary>
    public partial class Controllers : UserControl
    {
        ControllersViewModel cvm;

        public Controllers()
        {
            InitializeComponent();
            DataContext = (Application.Current as App).ControllersViewModel;
            cvm = DataContext as ControllersViewModel;
            myJoystick.DataContext = cvm;
        }

        private void aileronSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            cvm.VM_Aileron = Convert.ToDouble(e.NewValue);
        }

        private void throttleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            cvm.VM_Throttle = Convert.ToDouble(e.NewValue);
        }
    }
}
