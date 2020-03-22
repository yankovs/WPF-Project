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
using WPF_Project.Server;
using WPF_Project.ViewModel;

namespace WPF_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AppViewModel vm;
        private MyServer ms = new MyServer();
        public MainWindow()
        {
            InitializeComponent();
            vm = new AppViewModel(new AppModel(ms));
            DataContext = vm;
        }

        private void aileronSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            vm.VM_Aileron = Convert.ToDouble(e.NewValue);            
        }

        private void throttleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            vm.VM_Throttle = Convert.ToDouble(e.NewValue);            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ms.Connect("127.0.0.1", 5402);
        }
    }
}
