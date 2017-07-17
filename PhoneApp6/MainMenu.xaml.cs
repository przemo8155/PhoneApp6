using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.BackgroundAudio;

namespace PhoneApp6
{
    
    public partial class MainMenu : PhoneApplicationPage
    {
        Others othersClass = new Others();

        public MainMenu()
        {
            InitializeComponent();
            
        }

        public void PlayBackgroundMusic()
        {
            string fileUrl = "Music/black.mp3";
            BackgroundAudioPlayer.Instance.Track = new AudioTrack(new Uri(fileUrl, UriKind.Relative), "title", "artist", "album", null);
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SelectLevel.xaml", UriKind.Relative));
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Terminate();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Confirm Exit?",
                            MessageBoxButton.OKCancel) != MessageBoxResult.OK)
            {
                e.Cancel = true;

            }
            else
            {
                Application.Current.Terminate();
            }
            //base.OnBackKeyPress(e);
        }


    }
}