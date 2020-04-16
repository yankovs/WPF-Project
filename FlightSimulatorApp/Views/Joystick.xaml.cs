using FlightSimulatorApp.ViewModel;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {
        private Point firstPoint = new Point();
        private Storyboard sb;
        //mode: 0: before moving  1: while moving  2: after moving
        private volatile int mode;
        private ControllersViewModel cvm;
        private double x, y;

        public Joystick()
        {
            InitializeComponent();
            sb = Knob.FindResource("CenterKnob") as Storyboard;
            mode = 0;
        }

        private void centerKnob_Completed(object sender, EventArgs e)
        {
            cvm = DataContext as ControllersViewModel;
            sb.Stop();
            cvm.VM_Rudder = 0;
            cvm.VM_Elevator = 0;
            mode = 0;
        }

        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                firstPoint.X = e.GetPosition(this).X;
                firstPoint.Y = e.GetPosition(this).Y;
                mode = 1;
            }
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            cvm = DataContext as ControllersViewModel;
            if (mode == 1)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    x = e.GetPosition(this).X - firstPoint.X;
                    y = e.GetPosition(this).Y - firstPoint.Y;
                    if (Math.Sqrt(x * x + y * y) < Base.Width / 2)
                    {
                        cvm.VM_Rudder = x;
                        cvm.VM_Elevator = y;
                    }
                }
                else
                {
                    mode = 2;
                }
            }
        }

        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            sb.Begin();
        }
    }
}
