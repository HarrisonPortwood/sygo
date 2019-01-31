using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
using System.Windows.Threading;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private DateTime lastPointInTime = DateTime.MinValue;
        private TimeSpan totalElapsed = new TimeSpan(0);

        private void dtTicker(object sender, EventArgs e)
        {
            var nextPointInTime = DateTime.Now;
            totalElapsed += (nextPointInTime - lastPointInTime);
            lastPointInTime = nextPointInTime;
            lblTimer.Content = totalElapsed.ToString(@"hh\:mm\:ss");
        }

        private DateTime started = DateTime.MinValue;
        DispatcherTimer dt = new DispatcherTimer();

        private void StartStop_Click(object sender, EventArgs e)
        {
            if ((string)StartStop.Content == "START")
            {
                dt.Interval = new TimeSpan(0, 0, 1);
                dt.Tick += dtTicker;
                lastPointInTime = DateTime.Now;
                dt.Start();
                StartStop.Content = "STOP";
                StartStop.BorderBrush = Brushes.Red;
                //EmailResult.Content = "";
                Reset.Visibility = Visibility.Hidden;
            }
            else if ((string)StartStop.Content == "STOP")
            {
                dt.Stop();
                StartStop.Content = "START";
                StartStop.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 0, 127, 70));
                //EmailResult.Content = "Click here to email this time.";
                Reset.Visibility = Visibility.Visible;
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            totalElapsed = new TimeSpan(0);
            Reset.Visibility = Visibility.Hidden;
            lblTimer.Content = "00:00:00";
            //EmailResult.Content = "";

        }

        private void OnNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(e.Uri.AbsoluteUri);
            e.Handled = true;
        }

        private void EmailResult_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
