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
            InitializeComponent();
            DataContext = (Application.Current as App).AppViewModel;
            avm = DataContext as AppViewModel;
        }

        private void Button_Click(object Sender, RoutedEventArgs e)
        {
            var button = Sender as Button;
            if ((string)button.Content == "Connect")
            {
                avm.VM_ConnectionMode = "Connected";                
            }
            else if ((string)button.Content == "Disconnect")
            {
                avm.VM_ConnectionMode = "Disconnected";                
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (ConnectionMode.Text != "Disconnected")
            {
                avm.VM_ConnectionMode = "Disconnected";
            }
        }

        private void ConnectionMode_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ConnectionMode.Text == "Connected")
            {
                ConnectionBtn.Content = "Disconnect";
            }
            else if (ConnectionMode.Text == "Disconnected")
            {
                ConnectionBtn.Content = "Connect";
            }
            else if(ConnectionMode.Text == "Connection Error")
            {
                ConnectionBtn.Content = "Connect";
                MessageBox.Show("Couldn't connect, is the server on?");               
            }
        }
    }
}
