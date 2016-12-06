using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WeatherApp15_sairam.Resources;
using System.IO.IsolatedStorage;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WeatherApp15_sairam.ViewModel;
using System.Windows.Data;
using Microsoft.Phone.Scheduler;
using Cimbalino;
using MidSizeTileLibrary;
using Microsoft.Phone.Tasks;
using LargeTilelibrary;
using Largetileback;
using Midsizeback;


namespace WeatherApp15_sairam
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
        private Color _c;


        public Color C

        {
  
            get { return _c; }
  
            set {
                _c = value;
                settings["clr"] = _c;
                
            
            }

        }
        

        public MainPage()
        {
            InitializeComponent();
           registerAgent();


            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void registerAgent()
        {
            string taskname = "updateWeather";
            try
            {
                if (ScheduledActionService.Find(taskname) != null)
                {
                    ScheduledActionService.Remove(taskname);
                }
                PeriodicTask periodicTask = new PeriodicTask(taskname);
                periodicTask.Description = "random numbers";
                ScheduledActionService.Add(periodicTask);
//#if DEBUG
//                ScheduledActionService.LaunchForTest(taskname, TimeSpan.FromSeconds(15));
//#endif
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (SchedulerServiceException s)
            {
                MessageBox.Show(s.Message);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            base.OnNavigatedTo(e);


            while (NavigationService.CanGoBack)
            {
                NavigationService.RemoveBackEntry();
            }
            if (settings.Contains("b"))
            {
                if(settings["b"].ToString() == "yes")
                {
                    ViewModelLocator v = new ViewModelLocator();
                    MainViewModel m = v.Main;
                    if (m.Background.Count == 0)
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
                            new BitmapImage(new Uri(m.Background[0].ToString(), UriKind.Relative)),
                            Stretch = Stretch.Fill

                        };
                        LayoutRoot.Background = back;
                    }
                    

                    //LayoutRoot.SetBinding(Grid.back);
                    
                    //Binding myBinding = new Binding();
                    
                    //myBinding.Source = m.Background[0].ToString();

                   // LayoutRoot.SetBinding(Grid.BackgroundProperty , myBinding);
                }

                else if (settings.Contains("clr"))
                {
                    //aset.Add("tno", s);
                    LayoutRoot.Background = new SolidColorBrush((Color)settings["clr"]);
                    settings["b"] = "no";
                }
                else
                {
                    settings.Add("clr", C);
                    settings["b"] = "no";
                }
            }
            
           /* System.Diagnostics.Debug.WriteLine("into the app");
            try
            {
                System.Diagnostics.Debug.WriteLine("Retrieving values");
               /* gameMusic.IsChecked = (bool)settings["gamemusic"];
                timed.IsChecked = (bool)settings["timed"];
                slider1.Value = ( Int16)settings["diff"];*/
 
           /* }
            catch(KeyNotFoundException ex)
            {
                System.Diagnostics.Debug.WriteLine("First Time using the app");
                /*settings.Add("timed", false);
                settings.Add("gamemusic", false);
                settings.Add("diff", 1);
                settings.Save();
 
            }*/
 
        
        }
       

        private void navigateSearch(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SearchPage.xaml", UriKind.Relative));
        }

        private void pick(object sender, EventArgs e)
        {
            //NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
            cpick.Visibility = Visibility.Visible;
            bok.Visibility = Visibility.Visible;
        }
        private void changed(object sender, System.Windows.Media.Color color)
        {
            LayoutRoot.Background = new SolidColorBrush(color);
            C = color;
            settings["b"] = "no";

        }
        private void doneColoring(object sender, EventArgs e)
        {
           // NavigationService.Navigate(new Uri("/SearchPage.xaml", UriKind.Relative));
            
                cpick.Visibility = Visibility.Collapsed;
            bok.Visibility = Visibility.Collapsed;
        }

        private void background(object sender, EventArgs e)
        {

            if (settings.Contains("b"))
            {

                if (settings["b"].ToString() == "yes")
                {

                    MessageBox.Show("You already have background activated!");
                    return;

                }

                else
                {
                    ViewModelLocator v = new ViewModelLocator();
                    MainViewModel m = v.Main;
                    ImageBrush back = new ImageBrush

                    {

                        ImageSource = new BitmapImage(new Uri(m.Background[0].ToString(), UriKind.Relative)),
                        Stretch = Stretch.Fill

                    };
                    LayoutRoot.Background = back;
                    settings["b"] = "yes";
                }


            }
            else
            {


                settings.Add("b","yes");
                MessageBox.Show("Backgrounds will load automatically");
                ViewModelLocator v = new ViewModelLocator();
                MainViewModel m = v.Main;
                ImageBrush back = new ImageBrush

                {

                    ImageSource = new BitmapImage(new Uri(m.Background[0].ToString(), UriKind.Relative)),
                    Stretch = Stretch.Fill

                };
                LayoutRoot.Background = back;
            }

        }

        

       

      
        private void loadchanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            IsolatedStorageSettings settingpage = IsolatedStorageSettings.ApplicationSettings;
            if(settingpage.Contains("Units"))
            {
                checkistore();
            }
            else
            {
                settingpage.Add("Units","Metric");
                checkistore();

            }
            TopGrid.Visibility = Visibility.Visible;
            BotGrid.Visibility = Visibility.Visible;
            nexttempgrid.Visibility = Visibility.Visible;
            lbDay.Visibility = Visibility.Visible;
            ShellTile apptile = ShellTile.ActiveTiles.First();
            if (apptile != null)
            {
                //pinclick1.IsEnabled = false;
            }
        }

        
            private void checkistore()
            {
                IsolatedStorageSettings settingpage = IsolatedStorageSettings.ApplicationSettings;
                if(settingpage["Units"].ToString().Equals("Metric"))
                {
                    todayTemp.Text = string.Format("{0:D} C", todayTemp.Text);
                   // nextTemp.Text = string.Format("{0:F1} C", nextTemp.Text);
                    wind.Text = string.Format("{0:F} m/s", wind.Text);
                   // wind2.Text = string.Format("{0:F} m/s", wind2.Text);
                }
                else
                {
                    todayTemp.Text = string.Format("{0:F1} F", todayTemp.Text);
                    //nextTemp.Text = string.Format("{0:F1} F", nextTemp.Text);
                   // wind.Text = string.Format("{0:F} mph", wind.Text);
                   // wind2.Text = string.Format("{0:F} mph", wind2.Text);

                }

            }
            

           
           

        private void pinclick(object sender, EventArgs e)
        {

           /*try{
               FlipTileData tileData = new FlipTileData
                {
                    //BackgroundImage = new Uri("isostore:" + filename, UriKind.Absolute),
                    // BackgroundImage = new Uri("/Assets/Images/sunny.png", UriKind.Relative),
                    
                    BackContent = "Hi test",
                };
               string tileUri = "/Mainpage.xaml";
                ShellTile.Create(new Uri(tileUri, UriKind.Relative), tileData, true);

                //MessageBox.Show(filename.ToString());
                //this.Background = new Uri(filename,UriKind.Absolute);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
            UpdateTile();
        }

        private void navigatetoSettings(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
        }
        public void UpdateTile()
        {
            var customTile = new MidTile();
            var customLargeTile = new LargeTile();
            var customLargeTileBack = new Largetilebackclass();
            var customTileBack = new Midsizetileback();

            customLargeTile.Measure(new Size(691, 336));
            customLargeTile.Arrange(new Rect(0, 0, 691, 336));
            customLargeTileBack.Measure(new Size(691, 336));
            customLargeTileBack.Arrange(new Rect(0, 0, 691, 336));
            customTile.Measure(new Size(336, 336));
            customTile.Arrange(new Rect(0, 0, 336, 336));
            customTileBack.Measure(new Size(336, 336));
            customTileBack.Arrange(new Rect(0, 0, 336, 336));

            var bmp = new WriteableBitmap(336, 336);
            var bmpL = new WriteableBitmap(691, 336);
            var bmplb = new WriteableBitmap(691, 336);
            var bmpb = new WriteableBitmap(336, 336);


            bmpL.Render(customLargeTile, null);
            
            bmpL.Invalidate();

            bmplb.Render(customLargeTileBack, null);
            bmplb.Invalidate();

            bmp.Render(customTile, null);
            bmp.Invalidate();

            bmpb.Render(customTileBack, null);
            bmpb.Invalidate();

            
            const string filename = "/Shared/ShellContent/CustomTile.png";
            const string filenameLarge = "/Shared/ShellContent/CustomLargeTile.png";
            const string filenameLargeBack = "/Shared/ShellContent/CustomLargeTileBack.png";
            const string filenameBack = "/Shared/ShellContent/CustomTileBack.png";


            using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!isf.DirectoryExists("/CustomLiveTiles"))
                {
                    isf.CreateDirectory("/CustomLiveTiles");
                }

                using (var stream = isf.OpenFile(filename, System.IO.FileMode.OpenOrCreate))
                {
                    //bmp.SaveJpeg(stream, 336, 366, 0, 100);
                    //bmp.SavePng(stream);
                    Cimbalino.Phone.Toolkit.Extensions.WriteableBitmapExtensions.SavePng(bmp, stream);
                    
                }
                using (var stream = isf.OpenFile(filenameLarge, System.IO.FileMode.OpenOrCreate))
                {
                    //bmp.SaveJpeg(stream, 336, 366, 0, 100);
                    //bmp.SavePng(stream);
                    Cimbalino.Phone.Toolkit.Extensions.WriteableBitmapExtensions.SavePng(bmpL, stream);

                }
                using (var stream = isf.OpenFile(filenameLargeBack, System.IO.FileMode.OpenOrCreate))
                {
                    //bmp.SaveJpeg(stream, 336, 366, 0, 100);
                    //bmp.SavePng(stream);
                    Cimbalino.Phone.Toolkit.Extensions.WriteableBitmapExtensions.SavePng(bmplb, stream);

                }
                using (var stream = isf.OpenFile(filenameBack, System.IO.FileMode.OpenOrCreate))
                {
                    //bmp.SaveJpeg(stream, 336, 366, 0, 100);
                    //bmp.SavePng(stream);
                    Cimbalino.Phone.Toolkit.Extensions.WriteableBitmapExtensions.SavePng(bmpb, stream);

                }

            }
            try
            {

                FlipTileData tileData = new FlipTileData
                {
                    //Title = "CustomSecondaryTile", 
                   // WideBackgroundImage = new Uri("isostore:" + filename, UriKind.Absolute),
                    BackgroundImage = new Uri("isostore:" + filename, UriKind.Absolute),
                    BackBackgroundImage = new Uri("isostore:" + filenameBack, UriKind.Absolute),
                    WideBackgroundImage = new Uri("isostore:" + filenameLarge, UriKind.Absolute),
                    WideBackBackgroundImage = new Uri("isostore:" + filenameLargeBack, UriKind.Absolute),
                };

                string tileUri = string.Concat("/MainPage.xaml?", "");
                ShellTile.Create(new Uri(tileUri, UriKind.Relative), tileData, true);
            }
            catch (Exception e)
            {
                MessageBox.Show("Seems like you already have it pinned!" );
            }
        }

        private void updated(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            nextTemp.Text = string.Format("{0:F1} C", nextTemp.Text);

        }

        private void Review(object sender, EventArgs e)
        {
            MarketplaceReviewTask r = new MarketplaceReviewTask();
            r.Show();
        }

       /* private void imageChange(object sender, SelectionChangedEventArgs e)
        {
            if (tbDescription.Text.Equals("sky is clear"))
            {
                BitmapImage b = new BitmapImage(new Uri("/Assets/Weathericonwhite/Weather-Rain.png"));
                image2ndhalf.Source = b;
            }
            else
            {
                //BitmapImage b = new BitmapImage(new Uri("/Assets/Weathericonwhite/Weather.png"));
                //image2ndhalf.Source = b;
            }
        }*/

        

       
    }
}