using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using Windows.Storage;

namespace PhoneApp6
{
    public partial class SelectLevel : PhoneApplicationPage
    {
        ObservableCollection<Button> list = new ObservableCollection<Button>();
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        Button l1 = new Button();
        Others ot = new Others();
        public string level = "2";
        public int levelInt;

        public SelectLevel()
        {
            InitializeComponent();
            Components();
            EnableControls();
            
            



        }

        private void Components()
        {
            
            var obj = App.Current as App;

            Object value = localSettings.Values["level"];
            if (value == null)
            {

                obj.unlockedLevel = 1;
                localSettings.Values["level"] = 1;

            }

            else
            {
                localSettings.Values["level"] = obj.unlockedLevel;

            }



        }

        private void EnableControls()
        {

            var obj = App.Current as App;
            if(obj.unlockedLevel > 0 && obj.unlockedLevel < 2)
            {
                l2.IsEnabled = true;
            }

            if(obj.unlockedLevel == 2)
            {
                l2.IsEnabled = true;
                l3.IsEnabled = true;
            }

            if (obj.unlockedLevel == 3)
            {
                l2.IsEnabled = true;
                l3.IsEnabled = true;
                l4.IsEnabled = true;
            }
        }

        private void selectLevelButton_Click(object sender, RoutedEventArgs e)
        {
            if (levelsList.SelectedIndex == -1)
            {
                MessageBox.Show("Select level");
            }
            else
            {
                var obj = App.Current as App;
                obj.selectedLevel = levelsList.SelectedIndex + 1;
                NavigationService.Navigate(new Uri("/Labirynth.xaml", UriKind.Relative));

            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            var obj = App.Current as App;

            Object value = localSettings.Values["level"];
            if (value == null)
            {

                obj.unlockedLevel = 1;
                localSettings.Values["level"] = 1;

            }

            else
            {
                localSettings.Values["level"] = obj.unlockedLevel;

            }
            NavigationService.Navigate(new Uri("/MainMenu.xaml", UriKind.Relative));
        }
    }
}