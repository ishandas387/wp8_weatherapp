using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using System.IO;
using System.Windows.Media;

namespace WeatherApp15_sairam
{
    public partial class SearchPage : PhoneApplicationPage
    {
       // public IsolatedStorageSettings settingsp2 = IsolatedStorageSettings.ApplicationSettings;
        //List<string> abcd = new List<string>();
        List<string> history = new List<string>();
        //private Color _cp2;


        //public Color CP2
        //{

        //    get { return _cp2; }

        //    set
        //    {
        //        _cp2 = value;
        //        settingsp2["clr"] = _cp2;


        //    }

        //}
           
        public SearchPage()
        {
            InitializeComponent();

           
        }

        private void navigateResult(object sender, RoutedEventArgs e)
        {
            if (tbCity.Text == "")
            {
                MessageBox.Show("Please enter a city");
            }
            else
            {
                using (IsolatedStorageFile appStore = IsolatedStorageFile.GetUserStoreForApplication())
                {

                    StreamWriter sr = new StreamWriter(new IsolatedStorageFileStream("History.txt", FileMode.Append, appStore));
                    sr.WriteLine(tbCity.Text);
                    sr.Close();
                    //MessageBox.Show("Added to Favorites!");
                }
                
                using (IsolatedStorageFile appStorage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (appStorage.FileExists("History.txt"))
                    {
                        using (StreamReader reader = new StreamReader(new IsolatedStorageFileStream("History.txt", System.IO.FileMode.Open, FileAccess.Read, appStorage)))
                        {
                            var uri = reader.ReadLine();
                            while (!string.IsNullOrEmpty(uri))
                            {
                                history.Add(uri);
                                uri = reader.ReadLine();
                            }
                            reader.Close();
                        }
                    }
                }
                lbHistory.ItemsSource = history;
                NavigationService.Navigate(new Uri("/Result.xaml", UriKind.Relative));
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //if (settingsp2.Contains("clr"))
            //{
            //    //aset.Add("tno", s);
            //   ContentPanel.Background = new SolidColorBrush((Color)settingsp2["clr"]);
            //   // settingsp2["b"] = "no";
            //}
            //else
            //{
            //    settingsp2.Add("clr", CP2);
            //    //ssettings["b"] = "no";
            //}

           // citysearch.ItemsSource = abcd;
            if (lbHistory.ItemsSource == null)
            {
                using (IsolatedStorageFile appStorage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (appStorage.FileExists("History.txt"))
                    {
                        using (StreamReader reader = new StreamReader(new IsolatedStorageFileStream("History.txt", System.IO.FileMode.Open, FileAccess.Read, appStorage)))
                        {
                            var uri = reader.ReadLine();
                            while (!string.IsNullOrEmpty(uri))
                            {
                                history.Add(uri);
                                uri = reader.ReadLine();
                            }
                            reader.Close();
                        }
                    }
                }
                lbHistory.ItemsSource = history;
            }

        }

        private void homeNavigate(object sender, EventArgs e)
        {
            
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void clearAll(object sender, EventArgs e)
        {
            using (IsolatedStorageFile appStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (appStorage.FileExists("History.txt"))
                {
                    appStorage.DeleteFile("History.txt");
                    lbHistory.ItemsSource = null;
                }
                else
                    MessageBox.Show("You currently don't have anything here!");
            }
        }

        private void itemclicked(object sender, SelectionChangedEventArgs e)
        {
            if (lbHistory.ItemsSource != null)
            {


                tbCity.Text = lbHistory.SelectedItem.ToString();
            }

        }

        //private void pickcolorsearchbackgrd(object sender, EventArgs e)
        //{
        //   cpick.Visibility = Visibility.Visible;
        //    bok.Visibility = Visibility.Visible;
        //}

        //private void changedcolor(object sender, System.Windows.Media.Color color)
        //{
        //    ContentPanel.Background = new SolidColorBrush(color);
        //    CP2 = color;
        //}
        //private void doneColoring(object sender, EventArgs e)
        //{
        //    // NavigationService.Navigate(new Uri("/SearchPage.xaml", UriKind.Relative));

        //    cpick.Visibility = Visibility.Collapsed;
        //    bok.Visibility = Visibility.Collapsed;
        //}
    }
}