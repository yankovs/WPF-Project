using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_Project.ViewModel;

namespace WPF_Project
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        SettingsViewModel svm;
        MainWindow mw;
        public Settings(MainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
            DataContext = (Application.Current as App).SettingsViewModel;
            svm = DataContext as SettingsViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            svm.VM_IP = IPBox.Text;
            svm.VM_Port = int.Parse(PortBox.Text);
            (Application.Current.MainWindow as MainWindow).Button_Click(sender, e);
            Close();
        }
    }
}
