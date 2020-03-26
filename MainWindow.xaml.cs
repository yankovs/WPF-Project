using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
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
using WPF_Project.Model;
using WPF_Project.Server;
using WPF_Project.ViewModel;
using WPF_Project.Views;

namespace WPF_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AppViewModel vm;
        private MyServer ms = new MyServer();
        private AppModel am;

        public MainWindow()
        {
            this.am = new AppModel(this.ms);
            vm = new AppViewModel(am);
            DataContext = vm;
            InitializeComponent();
            myJoystick.DataContext = vm.VM_JoystickModel;            
        }

        private void aileronSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            vm.VM_Aileron = Convert.ToDouble(e.NewValue);
        }

        private void throttleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            vm.VM_Throttle = Convert.ToDouble(e.NewValue);
        }

        private void Button_Click(object Sender, RoutedEventArgs e)
        {
            var button = Sender as Button;
            if ((string)button.Content == "Connect")
            {
                try
                {
                    if (am.getStop())
                    {
                        am.startModel();
                    }
                    ms.Connect(ConfigurationManager.AppSettings["IP"], int.Parse(ConfigurationManager.AppSettings["Port"]));                    
                    am.start();
                }
                catch (Exception)
                {
                    MessageBox.Show("Couldn't connect, is the server on?");
                    ms.disconnect();
                    am.stopModel();
                }
            }
            else if ((string)button.Content == "Disconnect")
            {
                ms.disconnect();
                am.stopModel();                
            }



        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (ms.isConnected())
            {
                ms.disconnect();
                am.stopModel();
            }
        }       

        private void VisibilityMode_TextChanged(object sender, TextChangedEventArgs e)
        {
            string vis = VisibilityMode.Text;            
            if (!am.getStop())
            {
                if (vis == "Hidden")
                {
                    Map.Visibility = Visibility.Hidden;                    
                    Pikachu.Visibility = Visibility.Visible;
                }
                else
                {
                    Pikachu.Visibility = Visibility.Hidden;
                    Map.Visibility = Visibility.Visible;              
                }                
            }
            else
            {
                if(Pikachu.Visibility == Visibility.Visible)
                {
                    Pikachu.Visibility = Visibility.Hidden;
                    Map.Visibility = Visibility.Visible;
                }
            }
            
        }
    }
}
