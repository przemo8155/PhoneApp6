using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace PhoneApp6
{
    public partial class LoadScreen : PhoneApplicationPage
    {
        private DispatcherTimer dispatcherTimer;
        public int _wait = 0;

        public LoadScreen()
        {
            InitializeComponent();
            CreateTimer();
        }

        public void CreateTimer()
        {
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            _wait++;
            if (_wait >= 3)
            {
                NavigationService.Navigate(new Uri("/MainMenu.xaml", UriKind.Relative));
                dispatcherTimer.Stop();
            }
        }
    }
}