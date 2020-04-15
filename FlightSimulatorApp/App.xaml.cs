using FlightSimulatorApp.Model;
using FlightSimulatorApp.Server;
using FlightSimulatorApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MyServer ms = new MyServer();
        private AppModel am;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this.am = new AppModel(this.ms);
            AppViewModel = new AppViewModel(am);
            ControllersViewModel = new ControllersViewModel(am);
            DashboardViewModel = new DashboardViewModel(am);
            MapViewModel = new MapViewModel(am);
            SettingsViewModel = new SettingsViewModel(am);
            MainWindow wnd = new MainWindow();
            wnd.Show();
        }

        public AppViewModel AppViewModel { get; set; }

        public ControllersViewModel ControllersViewModel { get; set; }

        public DashboardViewModel DashboardViewModel { get; set; }

        public MapViewModel MapViewModel { get; set; }

        public SettingsViewModel SettingsViewModel { get; set; }
    }
}
