using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.IO.IsolatedStorage;

namespace WeatherApp15_sairam
{
    public partial class Settings : PhoneApplicationPage
    {
        public Settings()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            IsolatedStorageSettings settingpage = IsolatedStorageSettings.ApplicationSettings;
            if(settingpage.Contains("Units"))
            {
                if(settingpage["Units"].ToString().Equals("Metric"))
                {
                    rbc.IsChecked = true;
                }
                else
                {
                    rbf.IsChecked = true;
                }
                if(settingpage.Contains("Current"))
                {
                    settingpage["Current"] = settingpage["Units"];
                }
                else
                {
                    settingpage.Add("Current", settingpage["Units"]);
                }
                
            }
            if (settingpage.Contains("Notifications"))
            {
                if (settingpage["Notifications"].ToString().Equals("yes"))
                {
                    notfon.IsChecked = true;
                }
                else
                {
                    notoff.IsChecked = true;
                }
            }
            if (settingpage.Contains("sethome"))
            {
                if (settingpage["sethome"].ToString().Equals("yes"))
                {
                    rbhome.IsChecked = true;
                    tbyescheck.Visibility = Visibility.Visible;
                    tbnocheck.Visibility = Visibility.Collapsed;
                    tbwhatshome.Text = settingpage["home"].ToString();
                    
                }
                else
                {
                    rbnohome.IsChecked = true;
                    tbyescheck.Visibility = Visibility.Collapsed;
                    tbnocheck.Visibility = Visibility.Visible;
                    tbwhatshome.Text = "Current Location";
                }

                
            }
            if (settingpage.Contains("Currentsethome"))
            {
                settingpage["Currentsethome"] = settingpage["sethome"];
            }
            else
            {
                settingpage.Add("Currentsethome", settingpage["sethome"]);
            }

        }

        private void bSave(object sender, RoutedEventArgs e)
        {
            IsolatedStorageSettings settingpage = IsolatedStorageSettings.ApplicationSettings;

           if(rbc.IsChecked.HasValue && rbc.IsChecked.Value)
           {
               if (settingpage.Contains("Units"))
               {
                   settingpage["Units"] = "Metric";
               }
               else
                   settingpage.Add("Units", "Metric");

           }
           else if (rbf.IsChecked.HasValue && rbf.IsChecked.Value)
           {
               if (settingpage.Contains("Units"))
               {
                   settingpage["Units"] = "Imperial";
               }
               else
                   settingpage.Add("Units", "Imperial");

           }

           if (notfon.IsChecked.HasValue && notfon.IsChecked.Value)
           {
               if (settingpage.Contains("Notifications"))
               {
                   settingpage["Notifications"] = "yes";
               }
               else
                   settingpage.Add("Notifications", "yes");

           }

           else if (notoff.IsChecked.HasValue && notoff.IsChecked.Value)
           {
               if (settingpage.Contains("Notifications"))
               {
                   settingpage["Notifications"] = "no";
               }
               else
                   settingpage.Add("Notifications", "no");
           }

           if (rbhome.IsChecked.HasValue && rbhome.IsChecked.Value)
           {
               if (settingpage.Contains("sethome"))
               {
                   settingpage["sethome"] = "yes";
                   if (settingpage.Contains("home"))
                   {
                       settingpage["home"] = settingpage["cityname"].ToString();
                   }
                   else
                   {


                       settingpage.Add("home", settingpage["cityname"].ToString());
                   }
               }
               else
               {
                   settingpage.Add("sethome", "yes");
                   if (settingpage.Contains("home"))
                   {
                       settingpage["home"] = settingpage["cityname"].ToString();
                   }
                   else
                   {


                       settingpage.Add("home", settingpage["cityname"].ToString());
                   }
               }


           }

           else if (rbnohome.IsChecked.HasValue && rbnohome.IsChecked.Value)
           {
               if (settingpage.Contains("sethome"))
               {
                   settingpage["sethome"] = "no";
               }
               else
                   settingpage.Add("sethome", "no");
           }




           settingpage.Save();
           NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));

        }

        private void settinghomechecked(object sender, RoutedEventArgs e)
        {
            tbnocheck.Visibility = Visibility.Collapsed;
            tbyescheck.Visibility = Visibility.Visible;
        }

        private void useloc(object sender, RoutedEventArgs e)
        {
            tbnocheck.Visibility = Visibility.Visible;
            tbyescheck.Visibility = Visibility.Collapsed;
        }

        /*private void willnotuseloc(object sender, RoutedEventArgs e)
        {
            tbnocheck.Visibility = Visibility.Collapsed;
            tbyescheck.Visibility = Visibility.Visible;
        }*/

       /* private void nohomecheck(object sender, RoutedEventArgs e)
        {
            tbnocheck.Visibility = Visibility.Visible;
            tbyescheck.Visibility = Visibility.Collapsed;
        }*/

       
    }
}