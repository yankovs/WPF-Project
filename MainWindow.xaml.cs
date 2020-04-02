using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
        AppViewModel avm;
        public MainWindow()
        {
            DataContext = (Application.Current as App).AppViewModel;
            avm = DataContext as AppViewModel;
            InitializeComponent();            
        }

        public void Button_Click(object Sender, RoutedEventArgs e)
        {
            var button = Sender as Button;
            if ((string)button.Content == "Connect")
            {
                avm.VM_ConnectionMode = "Connected";  
                if(avm.VM_IsError == "Yes")
                {
                    avm.VM_ConnectionMode = "Disconnected";
                }
                else
                {
                    StatusTxt.Text = "Connected";
                }
            }
            else if ((string)button.Content == "Disconnect")
            {
                avm.VM_ConnectionMode = "Disconnected";
                StatusTxt.Text = "Disconnected";
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (ConnectionMode.Text != "Disconnected")
            {
                avm.VM_ConnectionMode = "Disconnected";
            }
        }

        private async void ConnectionMode_TextChanged(object sender, TextChangedEventArgs e)
        {           

            if (ConnectionMode.Text == "Connected")
            {
                ConnectionBtn.Content = "Disconnect";
            }
            else if (ConnectionMode.Text == "Disconnected")
            {
                if(avm.VM_IsError == "Yes")
                {
                    //MessageBox.Show("Couldn't connect, is the server on?");
                    ConnectionBtn.Visibility = Visibility.Hidden;
                    ConnectionFailed.Visibility = Visibility.Visible;
                    await Task.Delay(5000);
                    ConnectionFailed.Visibility = Visibility.Hidden;
                    ConnectionBtn.Visibility = Visibility.Visible;                    
                }
                ConnectionBtn.Content = "Connect";
            }            
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            Settings s = new Settings(this);
            s.Show();
        }        

        private async void IsError_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(IsError.Text == "Timeout")
            {
                Map.Visibility = Visibility.Hidden;
                Dashboard.Visibility = Visibility.Hidden; 
                Controllers.Visibility = Visibility.Hidden;
                ConnectionBtn.Visibility = Visibility.Hidden;
                SettingsBtn.Visibility = Visibility.Hidden;
                StatusTxt.Text = "Timeout Occurred (Disconnecting)";                
                avm.VM_ConnectionMode = "Disconnected";
                await Task.Delay(5000);                
                StatusTxt.Text = "Disconnected";
                ConnectionBtn.Visibility = Visibility.Visible;
                SettingsBtn.Visibility = Visibility.Visible;
                Map.Visibility = Visibility.Visible;
                Dashboard.Visibility = Visibility.Visible;
                Controllers.Visibility = Visibility.Visible;
            }
            else if(IsError.Text == "Server's disconnection")
            {
                Map.Visibility = Visibility.Hidden;
                Dashboard.Visibility = Visibility.Hidden;
                Controllers.Visibility = Visibility.Hidden;
                ConnectionBtn.Visibility = Visibility.Hidden;
                SettingsBtn.Visibility = Visibility.Hidden;
                StatusTxt.Text = "Server's Disconnection (Disconnecting)";
                avm.VM_ConnectionMode = "Disconnected";
                await Task.Delay(5000);
                StatusTxt.Text = "Disconnected";
                ConnectionBtn.Visibility = Visibility.Visible;
                SettingsBtn.Visibility = Visibility.Visible;
                Map.Visibility = Visibility.Visible;
                Dashboard.Visibility = Visibility.Visible;
                Controllers.Visibility = Visibility.Visible;
            }
        }

        
    }
}
