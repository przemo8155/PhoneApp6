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

            if (obj.unlockedLevel == 4)
            {
                l2.IsEnabled = true;
                l3.IsEnabled = true;
                l4.IsEnabled = true;
                l5.IsEnabled = true;
            }

            if (obj.unlockedLevel == 5)
            {
                l2.IsEnabled = true;
                l3.IsEnabled = true;
                l4.IsEnabled = true;
                l5.IsEnabled = true;
                l6.IsEnabled = true;
            }

            if (obj.unlockedLevel == 6)
            {
                l2.IsEnabled = true;
                l3.IsEnabled = true;
                l4.IsEnabled = true;
                l5.IsEnabled = true;
                l6.IsEnabled = true;
                l7.IsEnabled = true;
            }

            if (obj.unlockedLevel == 7)
            {
                l2.IsEnabled = true;
                l3.IsEnabled = true;
                l4.IsEnabled = true;
                l5.IsEnabled = true;
                l6.IsEnabled = true;
                l7.IsEnabled = true;
                l8.IsEnabled = true;
            }

            if (obj.unlockedLevel == 8)
            {
                l2.IsEnabled = true;
                l3.IsEnabled = true;
                l4.IsEnabled = true;
                l5.IsEnabled = true;
                l6.IsEnabled = true;
                l7.IsEnabled = true;
                l8.IsEnabled = true;
                l9.IsEnabled = true;
            }

            if (obj.unlockedLevel == 9)
            {
                l2.IsEnabled = true;
                l3.IsEnabled = true;
                l4.IsEnabled = true;
                l5.IsEnabled = true;
                l6.IsEnabled = true;
                l7.IsEnabled = true;
                l8.IsEnabled = true;
                l9.IsEnabled = true;
                l10.IsEnabled = true;
            }

            if (obj.unlockedLevel == 10)
            {
                l2.IsEnabled = true;
                l3.IsEnabled = true;
                l4.IsEnabled = true;
                l5.IsEnabled = true;
                l6.IsEnabled = true;
                l7.IsEnabled = true;
                l8.IsEnabled = true;
                l9.IsEnabled = true;
                l10.IsEnabled = true;
                l11.IsEnabled = true;
            }

            if (obj.unlockedLevel == 11)
            {
                l2.IsEnabled = true;
                l3.IsEnabled = true;
                l4.IsEnabled = true;
                l5.IsEnabled = true;
                l6.IsEnabled = true;
                l7.IsEnabled = true;
                l8.IsEnabled = true;
                l9.IsEnabled = true;
                l10.IsEnabled = true;
                l11.IsEnabled = true;
                l12.IsEnabled = true;
            }

            if (obj.unlockedLevel == 12)
            {
                l2.IsEnabled = true;
                l3.IsEnabled = true;
                l4.IsEnabled = true;
                l5.IsEnabled = true;
                l6.IsEnabled = true;
                l7.IsEnabled = true;
                l8.IsEnabled = true;
                l9.IsEnabled = true;
                l10.IsEnabled = true;
                l11.IsEnabled = true;
                l12.IsEnabled = true;
                l13.IsEnabled = true;
            }

            if (obj.unlockedLevel == 13)
            {
                l2.IsEnabled = true;
                l3.IsEnabled = true;
                l4.IsEnabled = true;
                l5.IsEnabled = true;
                l6.IsEnabled = true;
                l7.IsEnabled = true;
                l8.IsEnabled = true;
                l9.IsEnabled = true;
                l10.IsEnabled = true;
                l11.IsEnabled = true;
                l12.IsEnabled = true;
                l13.IsEnabled = true;
                l14.IsEnabled = true;
            }

            if (obj.unlockedLevel == 14)
            {
                l2.IsEnabled = true;
                l3.IsEnabled = true;
                l4.IsEnabled = true;
                l5.IsEnabled = true;
                l6.IsEnabled = true;
                l7.IsEnabled = true;
                l8.IsEnabled = true;
                l9.IsEnabled = true;
                l10.IsEnabled = true;
                l11.IsEnabled = true;
                l12.IsEnabled = true;
                l13.IsEnabled = true;
                l14.IsEnabled = true;
                l15.IsEnabled = true;
            }

            if (obj.unlockedLevel == 15)
            {
                l2.IsEnabled = true;
                l3.IsEnabled = true;
                l4.IsEnabled = true;
                l5.IsEnabled = true;
                l6.IsEnabled = true;
                l7.IsEnabled = true;
                l8.IsEnabled = true;
                l9.IsEnabled = true;
                l10.IsEnabled = true;
                l11.IsEnabled = true;
                l12.IsEnabled = true;
                l13.IsEnabled = true;
                l14.IsEnabled = true;
                l15.IsEnabled = true;
                l16.IsEnabled = true;
            }

            if (obj.unlockedLevel == 16)
            {
                l2.IsEnabled = true;
                l3.IsEnabled = true;
                l4.IsEnabled = true;
                l5.IsEnabled = true;
                l6.IsEnabled = true;
                l7.IsEnabled = true;
                l8.IsEnabled = true;
                l9.IsEnabled = true;
                l10.IsEnabled = true;
                l11.IsEnabled = true;
                l12.IsEnabled = true;
                l13.IsEnabled = true;
                l14.IsEnabled = true;
                l15.IsEnabled = true;
                l16.IsEnabled = true;
                l17.IsEnabled = true;
            }

            if (obj.unlockedLevel == 17)
            {
                l2.IsEnabled = true;
                l3.IsEnabled = true;
                l4.IsEnabled = true;
                l5.IsEnabled = true;
                l6.IsEnabled = true;
                l7.IsEnabled = true;
                l8.IsEnabled = true;
                l9.IsEnabled = true;
                l10.IsEnabled = true;
                l11.IsEnabled = true;
                l12.IsEnabled = true;
                l13.IsEnabled = true;
                l14.IsEnabled = true;
                l15.IsEnabled = true;
                l16.IsEnabled = true;
                l17.IsEnabled = true;
                l18.IsEnabled = true;
            }

            if (obj.unlockedLevel == 18)
            {
                l2.IsEnabled = true;
                l3.IsEnabled = true;
                l4.IsEnabled = true;
                l5.IsEnabled = true;
                l6.IsEnabled = true;
                l7.IsEnabled = true;
                l8.IsEnabled = true;
                l9.IsEnabled = true;
                l10.IsEnabled = true;
                l11.IsEnabled = true;
                l12.IsEnabled = true;
                l13.IsEnabled = true;
                l14.IsEnabled = true;
                l15.IsEnabled = true;
                l16.IsEnabled = true;
                l17.IsEnabled = true;
                l18.IsEnabled = true;
                l19.IsEnabled = true;
            }

            if (obj.unlockedLevel > 18)
            {
                l2.IsEnabled = true;
                l3.IsEnabled = true;
                l4.IsEnabled = true;
                l5.IsEnabled = true;
                l6.IsEnabled = true;
                l7.IsEnabled = true;
                l8.IsEnabled = true;
                l9.IsEnabled = true;
                l10.IsEnabled = true;
                l11.IsEnabled = true;
                l12.IsEnabled = true;
                l13.IsEnabled = true;
                l14.IsEnabled = true;
                l15.IsEnabled = true;
                l16.IsEnabled = true;
                l17.IsEnabled = true;
                l18.IsEnabled = true;
                l19.IsEnabled = true;
                l20.IsEnabled = true;
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