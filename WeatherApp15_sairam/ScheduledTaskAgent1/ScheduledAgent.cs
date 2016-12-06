using System.Diagnostics;
using System.Windows;
using Microsoft.Phone.Scheduler;
using System;
using Microsoft.Phone.Shell;
using System.Linq;
using System.IO.IsolatedStorage;
using System.Net;
using ModelClassLibrary.Model;
using Newtonsoft.Json;
using Microsoft.Phone.Net.NetworkInformation;
using MidSizeTileLibrary;
using LargeTilelibrary;
using System.Windows.Media.Imaging;
using Largetileback;
using Midsizeback;



namespace ScheduledTaskAgent1
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        /// 

        const string filename = "/Shared/ShellContent/CustomTile.png";
        const string filenameLarge = "/Shared/ShellContent/CustomLargeTile.png";
        const string filenameLargeBack = "/Shared/ShellContent/CustomLargeTileBack.png";
        const string filenameBack = "/Shared/ShellContent/CustomTileBack.png";

            
        IsolatedStorageSettings settingpage = IsolatedStorageSettings.ApplicationSettings;
        string city = null;
        string u=null;
        int count = 1;
        

        static ScheduledAgent()
        {
            // Subscribe to the managed exception handler
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                
                Application.Current.UnhandledException += UnhandledException;
                
            });
        }

        private static void forImage()
        {
            // throw new NotImplementedException();
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
            bmp.Render(customTile, null);
            bmp.Invalidate();
            bmplb.Render(customLargeTileBack, null);
            bmplb.Invalidate();
            bmpb.Render(customTileBack, null);
            bmpb.Invalidate();


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

        }

        /// Code to execute on Unhandled Exceptions
        private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>
        protected override  void OnInvoke(ScheduledTask task)
        {
            //TODO: Add code to perform your task in background
            if(task is PeriodicTask)
            {
                var ni = NetworkInterface.NetworkInterfaceType;
                

               

                if(settingpage.Contains("Counter"))
                {
                    count = Convert.ToInt32(settingpage["Counter"].ToString());
                }
                else
                {
                    settingpage.Add("Counter", 3);
                    count = Convert.ToInt32(settingpage["Counter"].ToString());
                }
                if(count%8 == 0)
                {



                    if (ni == NetworkInterfaceType.None)
                    {
                        NotifyComplete();
                    }
                    if (settingpage.Contains("Notifications"))
                    {

                        if (settingpage["Notifications"].ToString().Equals("no"))
                        {
                            NotifyComplete();
                        }

                        else
                        {

                            if (settingpage.Contains("home"))
                            {
                                // city = null;
                                city = settingpage["home"].ToString();

                            }
                            else if (settingpage.Contains("cityname"))
                            {
                                city = settingpage["cityname"].ToString();

                            }
                            if (city == null)
                            {
                                NotifyComplete();
                            }
                            string units = "metric";
                            if (settingpage.Contains("Units"))
                            {
                                if (settingpage["Units"].ToString().Equals("Metric"))
                                {
                                    units = "Metric";
                                    u = "c";
                                }
                                else
                                {
                                    units = "Imperial";
                                    u = "f";
                                }
                            }

                            WebClient wbc = new WebClient();

                            wbc.DownloadStringAsync(new System.Uri("http://api.openweathermap.org/data/2.5/forecast/daily?q=" + city + "&mode=json&units=" + units + "&cnt=1&APPID= 8a5cad6deaac563e8625e20f01b4ca4b"));

                            wbc.DownloadStringCompleted += wbc_DownloadStringCompleted;

                        }
                    }



                   


                }
                else if (count%3 == 0)
                {
                    if (settingpage.Contains("home"))
                    {
                        // city = null;
                        city = settingpage["home"].ToString();

                    }
                    else if (settingpage.Contains("cityname"))
                    {
                        city = settingpage["cityname"].ToString();

                    }
                    if (city == null)
                    {
                        NotifyComplete();
                    }
                    string units = "metric";
                    if (settingpage.Contains("Units"))
                    {
                        if (settingpage["Units"].ToString().Equals("Metric"))
                        {
                            units = "Metric";
                            u = "c";
                        }
                        else
                        {
                            units = "Imperial";
                            u = "f";
                        }
                    }

                    WebClient wbc2 = new WebClient();

                    wbc2.DownloadStringAsync(new System.Uri("http://api.openweathermap.org/data/2.5/forecast/daily?q=" + city + "&mode=json&units=" + units + "&cnt=4&APPID= 8a5cad6deaac563e8625e20f01b4ca4b"));

                    wbc2.DownloadStringCompleted += wbc2_DownloadStringCompleted;
                   
                }

                else
                {
                    count++;
                    settingpage["Counter"] = count;
                    settingpage.Save();
                    NotifyComplete();
                }

            }

//#if DEBUG
//            ScheduledActionService.LaunchForTest(task.Name, TimeSpan.FromSeconds(15));
//#endif


            
        }

        private void wbc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            DayWiseWeather.RootObject d = null;
            d = JsonConvert.DeserializeObject<DayWiseWeather.RootObject>(e.Result);
            //IsolatedStorageSettings settingpage = IsolatedStorageSettings.ApplicationSettings;

            
            int temperature;
            if (DateTime.Now.Hour > 12 && DateTime.Now.Hour < 16)
            {
                temperature = Convert.ToInt32(d.list[0].temp.day);
            }
            else if (DateTime.Now.Hour > 16 && DateTime.Now.Hour < 18)
            {
                temperature = Convert.ToInt32(d.list[0].temp.eve);
            }
            else if (DateTime.Now.Hour > 18 && DateTime.Now.Hour < 4)
            {
                temperature = Convert.ToInt32(d.list[0].temp.night);

            }
            else if (DateTime.Now.Hour > 4 && DateTime.Now.Hour < 12)
            {
                temperature = Convert.ToInt32(d.list[0].temp.morn);

            }
            else
            {
                temperature = Convert.ToInt32(d.list[0].temp.day);

            }
            if (settingpage.Contains("Temper"))
            {
                settingpage["Temper"] = temperature;

            }
            else
            {

                settingpage.Add("Temper", temperature);
            }

            string humidity = d.list[0].humidity.ToString();
            if (settingpage.Contains("Humidity"))
            {
                settingpage["Humidity"] = humidity;

            }
            else
            {

                settingpage.Add("Humidity", humidity);
            }
            string descrip = d.list[0].weather[0].description;
            if (settingpage.Contains("descrip"))
            {
                settingpage["descrip"] = descrip;

            }
            else
            {

                settingpage.Add("descrip", descrip);
            }
            /*int temperature = Convert.ToInt32(d.list[0].temp.morn);
            if (settingpage.Contains("Temp"))
            {
                settingpage["Temp"] = temperature;

            }
            else
            {

                settingpage.Add("Temp", temperature);
            }
            int mintemperature = Convert.ToInt32(d.list[0].temp.min);
            if (settingpage.Contains("Mintemp"))
            {
                settingpage["Mintemp"] = mintemperature;

            }
            else
            {

                settingpage.Add("Mintemp", mintemperature);
            }
            int maxtemperature = Convert.ToInt32(d.list[0].temp.max);
            if (settingpage.Contains("Maxtemp"))
            {
                settingpage["Maxtemp"] = maxtemperature;

            }
            else
            {

                settingpage.Add("Maxtemp", maxtemperature);
            }
            
            string humidity = d.list[0].humidity.ToString();
            if (settingpage.Contains("Humidity"))
            {
                settingpage["Humidity"] = humidity;

            }
            else
            {

                settingpage.Add("Humidity", humidity);
            }
            string descrip = d.list[0].weather[0].description;
            if (settingpage.Contains("description1"))
            {
                settingpage["description1"] = descrip;

            }
            else
            {

                settingpage.Add("description1", descrip);
            }*/
            if (settingpage.Contains("cityname"))
            {
                settingpage["cityname"] = city;
            }
            string toaststring = settingpage["Temper"].ToString();
            string toaststring2 = settingpage["descrip"].ToString();
            if (u != null)
            {


                if (u.Equals("c"))
                {
                    toaststring = string.Format("{0:F1} °c - ", toaststring);
                }
                else
                {
                    toaststring = string.Format("{0:F1} °f - ", toaststring);
                }
            }


            toaststring = string.Concat(toaststring, toaststring2);
            ShellToast toast = new ShellToast();
            toast.Content = toaststring;
            toast.Title = city;
            toast.Show();
            count++;
            settingpage["Counter"] = count;
            settingpage.Save();
            NotifyComplete();
            
        }

       
        private void wbc2_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.Error == null)
            {


                DayWiseWeather.RootObject d = null;
                d = JsonConvert.DeserializeObject<DayWiseWeather.RootObject>(e.Result);
                //IsolatedStorageSettings settingpage = IsolatedStorageSettings.ApplicationSettings;
                int temperature;
                if(DateTime.Now.Hour > 12 && DateTime.Now.Hour<16)
                {
                    temperature = Convert.ToInt32(d.list[0].temp.day);
                }
                else if(DateTime.Now.Hour > 16 && DateTime.Now.Hour <18)
                {
                     temperature = Convert.ToInt32(d.list[0].temp.eve);
                }
                else if(DateTime.Now.Hour > 18 && DateTime.Now.Hour < 4)
                {
                     temperature = Convert.ToInt32(d.list[0].temp.night);

                }
                else if (DateTime.Now.Hour > 4 && DateTime.Now.Hour<12)
                {
                     temperature = Convert.ToInt32(d.list[0].temp.morn);

                }
                else
                {
                    temperature = Convert.ToInt32(d.list[0].temp.day);

                }
                
            if (settingpage.Contains("Temp"))
            {
                settingpage["Temp"] = temperature;

            }
            else
            {

                settingpage.Add("Temp", temperature);
            }
            int mintemperature = Convert.ToInt32(d.list[0].temp.min);
            if (settingpage.Contains("Mintemp"))
            {
                settingpage["Mintemp"] = mintemperature;

            }
            else
            {

                settingpage.Add("Mintemp", mintemperature);
            }
            int maxtemperature = Convert.ToInt32(d.list[0].temp.max);
            if (settingpage.Contains("Maxtemp"))
            {
                settingpage["Maxtemp"] = maxtemperature;

            }
            else
            {

                settingpage.Add("Maxtemp", maxtemperature);
            }
            
            string humidity = d.list[0].humidity.ToString();
            if (settingpage.Contains("Humidity"))
            {
                settingpage["Humidity"] = humidity;

            }
            else
            {

                settingpage.Add("Humidity", humidity);
            }
            string descrip = d.list[0].weather[0].description;
            if (settingpage.Contains("description1"))
            {
                settingpage["description1"] = descrip;

            }
            else
            {

                settingpage.Add("description1", descrip);
            }
                string d1temp = Convert.ToInt32(d.list[1].temp.day).ToString();
                string d1mintemp = Convert.ToInt32(d.list[1].temp.min).ToString();
                string d1maxtemp = Convert.ToInt32(d.list[1].temp.max).ToString();

                if (settingpage.Contains("d1temp"))
                {
                    settingpage["d1temp"] = d1temp;

                }
                else
                {

                    settingpage.Add("d1temp", d1temp);
                }
                if (settingpage.Contains("d1mintemp"))
                {
                    settingpage["d1mintemp"] = d1mintemp;

                }
                else
                {

                    settingpage.Add("d1mintemp", d1mintemp);
                }

                if (settingpage.Contains("d1maxtemp"))
                {
                    settingpage["d1maxtemp"] = d1maxtemp;

                }
                else
                {

                    settingpage.Add("d1maxtemp", d1maxtemp);
                }
                //data for day2 

                string d2temp = Convert.ToInt32(d.list[2].temp.day).ToString();
                string d2mintemp = Convert.ToInt32(d.list[2].temp.min).ToString();
                string d2maxtemp = Convert.ToInt32(d.list[2].temp.max).ToString();

                if (settingpage.Contains("d2temp"))
                {
                    settingpage["d2temp"] = d2temp;

                }
                else
                {

                    settingpage.Add("d2temp", d2temp);
                }
                if (settingpage.Contains("d2mintemp"))
                {
                    settingpage["d2mintemp"] = d2mintemp;

                }
                else
                {

                    settingpage.Add("d2mintemp", d2mintemp);
                }

                if (settingpage.Contains("d2maxtemp"))
                {
                    settingpage["d2maxtemp"] = d2maxtemp;

                }
                else
                {

                    settingpage.Add("d2maxtemp", d2maxtemp);
                }
                //day 3

                string d3temp = Convert.ToInt32(d.list[3].temp.day).ToString();
                string d3mintemp = Convert.ToInt32(d.list[3].temp.min).ToString();
                string d3maxtemp = Convert.ToInt32(d.list[3].temp.max).ToString();

                if (settingpage.Contains("d3temp"))
                {
                    settingpage["d3temp"] = d3temp;

                }
                else
                {

                    settingpage.Add("d3temp", d3temp);
                }
                if (settingpage.Contains("d3mintemp"))
                {
                    settingpage["d3mintemp"] = d3mintemp;

                }
                else
                {

                    settingpage.Add("d3mintemp", d3mintemp);
                }

                if (settingpage.Contains("d3maxtemp"))
                {
                    settingpage["d3maxtemp"] = d3maxtemp;

                }
                else
                {

                    settingpage.Add("d3maxtemp", d3maxtemp);
                }

                Deployment.Current.Dispatcher.BeginInvoke(delegate
                {

                    forImage();

                });

                try
                {


                    FlipTileData flip = new FlipTileData()
                    {
                        BackgroundImage = new Uri("isostore:" + filename, UriKind.Absolute),
                        BackBackgroundImage = new Uri("isostore:" + filenameBack, UriKind.Absolute),
                        WideBackgroundImage = new Uri("isostore:" + filenameLarge, UriKind.Absolute),
                        WideBackBackgroundImage = new Uri("isostore:" + filenameLargeBack, UriKind.Absolute),
                    };
                    ShellTile apptile = ShellTile.ActiveTiles.FirstOrDefault();
                    if (apptile != null)
                    {
                        apptile.Update(flip);


                    }
                }
                catch (Exception exc)
                {
                    // MessageBox.Show(e.Message);
                }
                count++;
                settingpage["Counter"] = count;
                settingpage.Save();
                NotifyComplete();



                   
            //}
                
            }
           

        }
    }
}