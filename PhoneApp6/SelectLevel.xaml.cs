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

namespace PhoneApp6
{
    public partial class SelectLevel : PhoneApplicationPage
    {
        ObservableCollection<Button> list = new ObservableCollection<Button>();
        Button l1 = new Button();
        Others ot = new Others();
        public string level = "1";
        public int levelInt;

        public SelectLevel()
        {
            InitializeComponent();
            ot.ReadFile(level);
            levelInt = int.Parse(level);
            MessageBox.Show(levelInt.ToString());            
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
            NavigationService.Navigate(new Uri("/MainMenu.xaml", UriKind.Relative));
        }
    }
}