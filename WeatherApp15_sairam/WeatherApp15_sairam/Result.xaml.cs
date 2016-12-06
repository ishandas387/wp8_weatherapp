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
using WeatherApp15_sairam.ViewModel;
using System.Windows.Media.Imaging;

namespace WeatherApp15_sairam
{
    public partial class Result : PhoneApplicationPage
    {
         IsolatedStorageSettings settings2 = IsolatedStorageSettings.ApplicationSettings;
        private Color _c2;


        public Color C2
        {

            get { return _c2; }

            set
            {
                _c2 = value;
                settings2["clr"] = _c2;


            }

        }
        
        public Result()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            base.OnNavigatedTo(e);



            if (settings2.Contains("b"))
            {
                if (settings2["b"].ToString() == "yes")
                {
                    ViewModelLocator v = new ViewModelLocator();
                    SearchViewModel s = v.Search;
                    if (s.Background.Count == 0)
                    {
                        ImageBrush back = new ImageBrush


                        {

                            ImageSource =
                            new BitmapImage(new Uri("/Assets/Background/clearsky.jpg", UriKind.Relative)),
                            Stretch = Stretch.Fill

                        };

                    }
                    else
                    {


                        ImageBrush back = new ImageBrush


                        {

                            ImageSource =
                            new BitmapImage(new Uri(s.Background[0].ToString(), UriKind.Relative)),
                            Stretch = Stretch.Fill

                        };
                        LayoutRoot.Background = back;
                        settings2.Add("b", "yes");
                    }


                    //LayoutRoot.SetBinding(Grid.back);

                    //Binding myBinding = new Binding();

                    //myBinding.Source = m.Background[0].ToString();

                    // LayoutRoot.SetBinding(Grid.BackgroundProperty , myBinding);
                }

                else if (settings2.Contains("clr"))
                {
                    //aset.Add("tno", s);
                    LayoutRoot.Background = new SolidColorBrush((Color)settings2["clr"]);
                    settings2["b"] = "no";
                }
                else
                {
                    settings2.Add("clr", C2);
                    settings2["b"] = "no";
                }
            }
           
        }
        private void homeNavi(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
        private void loadchanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            IsolatedStorageSettings settingpage = IsolatedStorageSettings.ApplicationSettings;
            if (settingpage.Contains("Units"))
            {
                if (settingpage["Units"].ToString().Equals("Metric"))
                {
                    Temp1.Text = string.Format("{0:F1} C", Temp1.Text);
                    //temp2.Text = string.Format("{0:F1} C", temp2.Text);
                    wind1.Text = string.Format("{0:F} m/s", wind1.Text);
                   // wind2.Text = string.Format("{0:F} kmph", wind2.Text);
                }
                else
                {
                    Temp1.Text = string.Format("{0:F1} F", Temp1.Text);
                   // temp2.Text = string.Format("{0:F1} F", temp2.Text);
                    // wind.Text = string.Format("{0:F} mph", wind.Text);
                    // wind2.Text = string.Format("{0:F} mph", wind2.Text);

                }

            }
            

            TopGrid.Visibility = Visibility.Visible;
            BotGrid.Visibility = Visibility.Visible;
           // nexttempgrid.Visibility = Visibility.Visible;
            lbDay.Visibility = Visibility.Visible;
        }
        private void coloring(object sender, EventArgs e)
        {
            //NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            cpick.Visibility = Visibility.Visible;
            bok.Visibility = Visibility.Visible;
        }
        private void changed(object sender, System.Windows.Media.Color color)
        {
            LayoutRoot.Background = new SolidColorBrush(color);
            C2 = color;

        }
        private void doneColoring(object sender, EventArgs e)
        {
            // NavigationService.Navigate(new Uri("/SearchPage.xaml", UriKind.Relative));

            cpick.Visibility = Visibility.Collapsed;
            bok.Visibility = Visibility.Collapsed;
        }

        private void goBack(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }
        private void background(object sender, EventArgs e)
        {

            if (settings2.Contains("b"))
            {

                if (settings2["b"].ToString() == "yes")
                {

                    MessageBox.Show("You already have background activated!");
                    return;

                }

                else
                {
                    ViewModelLocator v = new ViewModelLocator();
                    SearchViewModel s = v.Search;
                    ImageBrush back = new ImageBrush

                    {

                        ImageSource = new BitmapImage(new Uri(s.Background[0].ToString(), UriKind.Relative)),
                        Stretch = Stretch.Fill

                    };
                    LayoutRoot.Background = back;
                    settings2["b"] = "yes";
                }


            }
            else
            {


                settings2.Add("b", "yes");
                MessageBox.Show("Backgrounds will load automatically");
                ViewModelLocator v = new ViewModelLocator();
                SearchViewModel s = v.Search;
                ImageBrush back = new ImageBrush

                {

                    ImageSource = new BitmapImage(new Uri(s.Background[0].ToString(), UriKind.Relative)),
                    Stretch = Stretch.Fill

                };
                LayoutRoot.Background = back;
            }

        }

        private void setresultashome(object sender, EventArgs e)
        {
           MessageBoxResult result= MessageBox.Show("Click OK to set this as home, To use location service, disable -set as home- in app settings.","Alright!",MessageBoxButton.OKCancel);
            if(result == MessageBoxResult.OK)
            {
                IsolatedStorageSettings settingpage = IsolatedStorageSettings.ApplicationSettings;
                if(settingpage.Contains("searchedresult"))
                {
                    if(settingpage.Contains("sethome"))
                    {
                        settingpage["sethome"] = "yes";
                        if (settingpage.Contains("home"))
                        {
                            settingpage["home"] = settingpage["searchedresult"].ToString();
                        }
                        else
                        {
                            settingpage["home"] = settingpage["searchedresult"].ToString();
                        }

                    }
                    else
                    {
                        settingpage.Add("sethome", "yes");
                        if (settingpage.Contains("home"))
                        {
                            settingpage["home"] = settingpage["searchedresult"].ToString();
                        }
                        else
                        {
                            settingpage["home"] = settingpage["searchedresult"].ToString();
                        }
                    }
                   
                }
            }
        }
    }
}