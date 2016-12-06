using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Net;
using System.Windows;
using Windows.Devices.Geolocation;
using ModelClassLibrary.Model;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Microsoft.Phone.Net.NetworkInformation;
using System.IO.IsolatedStorage;

namespace WeatherApp15_sairam.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// 

        private string homecity;
        private DayWiseWeather.L _currentDay = null;

        public DayWiseWeather.L CurrentDay
        {
            get { return _currentDay; }
            set
            {
                if (_currentDay == value)
                {
                    return;
                }
                _currentDay = value;
                RaisePropertyChanged("CurrentDay");
            }
        }

        private ObservableCollection<string> _background;
        public ObservableCollection<string> Background
        {
            get
            {
                return _background;
            }
            set
            {
                if (_background == value)
                {
                    return;
                }
                _background = value;
                RaisePropertyChanged("background");
            }

        }

        private ObservableCollection<string> _iconpath;
        public ObservableCollection<string> Iconpath
        {
            get
            {
                return _iconpath;
            }
            set
            {
                if(_iconpath == value)
                {
                    return;
                }
                _iconpath = value;
                RaisePropertyChanged("iconpath");
            }

        }
        private ObservableCollection<string> _iconpath2;
        public ObservableCollection<string> Iconpath2
        {
            get
            {
                return _iconpath2;
            }
            set
            {
                if (_iconpath2 == value)
                {
                    return;
                }
                _iconpath2 = value;
                RaisePropertyChanged("iconpath2");
            }

        }
        

        private ObservableCollection<double> _temp;
        public ObservableCollection<double> Temp
        {
            get
            {
                return _temp;
            }
            set
            {
                if(_temp == value)
                {
                    return;
                }
                _temp = value;
                RaisePropertyChanged("temp");
            }
        }
      
        //HUMIDITY
        private ObservableCollection<double> _humid = null;
        // this is the list for main class which returns temp humidity etc
        public ObservableCollection<double> Humid
        {
            get
            {
                return _humid;
            }
            set
            {
                if (_humid == value)
                {
                    return;
                }
                _humid = value;
                RaisePropertyChanged("humid");
            }
        }
        //PRESSURE
        private ObservableCollection<double> _pressure = null;
        // this is the list for main class which returns temp humidity etc
        public ObservableCollection<double> Pressure
        {
            get
            {
                return _pressure;
            }
            set
            {
                if (_pressure == value)
                {
                    return;
                }
                _pressure = value;
                RaisePropertyChanged("pressure");
            }
        }

        //maxtemp
        private ObservableCollection<double> _maxtemp = null;
        // this is the list for main class which returns temp humidity etc
        public ObservableCollection<double> Maxtemp
        {
            get
            {
                return _maxtemp;
            }
            set
            {
                if (_maxtemp == value)
                {
                    return;
                }
                _maxtemp = value;
                RaisePropertyChanged("maxtemp");
            }
        }

        //min temp

        private ObservableCollection<double> _mintemp = null;
        // this is the list for main class which returns temp humidity etc
        public ObservableCollection<double> Mintemp
        {
            get
            {
                return _mintemp;
            }
            set
            {
                if (_mintemp == value)
                {
                    return;
                }
                _mintemp = value;
                RaisePropertyChanged("mintemp");
            }
        }
        private ObservableCollection<double> _wind = null;
        // this is the list for main class which returns temp humidity etc
        public ObservableCollection<double> Wind
        {
            get
            {
                return _wind;
            }
            set
            {
                if (_wind == value)
                {
                    return;
                }
                _wind = value;
                RaisePropertyChanged("wind");
            }
        }



        //this is the message thats displayed at loading

        private ObservableCollection<string> _message = new ObservableCollection<string>();
        public ObservableCollection<string> Message
        {
            get
            {
                return _message;
            }
            set
            {
                if (_message == value)
                {
                    return;
                }
                _message = value;
                RaisePropertyChanged("message");
            }
        }

        //city value to search

        private string _city = null;

        public string City
        {
            get { return _city; }
            set
            {
                if (_city == value)
                {
                    return;
                }
                _city = value;
                RaisePropertyChanged("city");
            }
        }

        private ObservableCollection<string> _description = null;
        public ObservableCollection<string> description
        {
            get
            {
                return _description;
            }
            set
            {
                if (_description == value)
                {
                    return;
                }
                _description = value;
                RaisePropertyChanged("description");
            }
        }

        //name of city
        private ObservableCollection<string> _Cname = null;
        public ObservableCollection<string> cname
        {
            get
            {
                return _Cname;
            }
            set
            {
                if (_Cname == value)
                {
                    return;
                }
                _Cname = value;
                RaisePropertyChanged("cname");
            }
        }




        private ObservableCollection<DayWiseWeather.L> _daylist = null;
        public ObservableCollection<DayWiseWeather.L> Daylist
        {
            get
            {
                return _daylist;
            }
            set
            {
                if(_daylist == value)
                {
                    return;
                }

                _daylist = value;
                RaisePropertyChanged("Daylist");
            }
        }
        private string lo = "", lat = "";
        private bool _IsBusy;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set { _IsBusy = value; RaisePropertyChanged("IsBusy"); }
        }

        private bool _IsC = true;
        public bool IsC
        {
            get { return _IsC; }
            set { _IsC = value; RaisePropertyChanged("IsC"); }
        }
        private string units = "metric";

        private string _M;
        public string M
        {
            get { return _M; }
            set { _M = value; RaisePropertyChanged("M"); }
        }
        public RelayCommand StartSaveCommand { get; private set; }
        public RelayCommand _ShowProgressBar { get; set; }
        public RelayCommand _setUnit { get; set; }


        private void ShowProgressBar()
        {
            IsBusy = true;

            M = "Loading..";
        }
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
                Daylist = new ObservableCollection<DayWiseWeather.L>();
                Temp = new ObservableCollection<double>();
                Temp.Add(23.54);
                cname = new ObservableCollection<string>();
                cname.Add("Cuttack");
                description = new ObservableCollection<string>();
                description.Add("sky is clear la lal al ala");
                Iconpath = new ObservableCollection<string>();
                Background = new ObservableCollection<string>();
                Background.Add("Assets/Background/clearsky.jpg");
                Iconpath.Add("Assets/Weathericon/Weather.png");
            }
            else
            {
                // Code runs "for real"
                _ShowProgressBar = new RelayCommand(() => ShowProgressBar()
               );
                
                bool b = checkNetworkConnection();

                if (!b)
                {

                    MessageBoxResult result = MessageBox.Show("Seems like a connectivity issue!", "No Connectivity!", MessageBoxButton.OK);
                    if (result == MessageBoxResult.OK)
                    {

                        Application.Current.Terminate();

                    }
                }
                else
                {
                    initob();
                    checkUnits();
                    IsolatedStorageSettings settingpage = IsolatedStorageSettings.ApplicationSettings;
                    if(settingpage.Contains("sethome"))
                    {
                        if(settingpage["sethome"].ToString().Equals("yes"))
                        {
                           if(settingpage.Contains("home"))
                           {
                               homecity = settingpage["home"].ToString();
                           }
                            homeset();
                        }
                       else
                       {
                           gloc();
                        }
                        
                    }
                    else
                    {
                        gloc();
                    }
                   // gloc();
                    
                    
                    StartSaveCommand = new RelayCommand(RefreshData);
                }
            }
        }

        private void homeset()
        {
            //throw new NotImplementedException();
            WebClient w = new WebClient();

            this.IsBusy = true;
            w.DownloadStringCompleted += whome_DownloadStringCompleted;
            w.DownloadStringAsync(new System.Uri("http://api.openweathermap.org/data/2.5/weather?q="+homecity+ "&mode=json&units=" + units));


            WebClient wbc = new WebClient();
            wbc.DownloadStringCompleted += wbchome_DownloadStringCompleted;
            wbc.DownloadStringAsync(new System.Uri("http://api.openweathermap.org/data/2.5/forecast/daily?q=" + homecity + "&mode=json&units=" + units + "&cnt=7"));
           
        }

        private void whome_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.Error == null)
            {
                WeatherContext.RootObject r = null;
                r = JsonConvert.DeserializeObject<WeatherContext.RootObject>(e.Result);
                //MessageBox.Show(r.main.ToString());

                //Temp.Add(r.main.temp);
                //Humid.Add(r.main.humidity);
                // Pressure.Add(r.main.pressure);
                // Maxtemp.Add(r.main.temp_max);
                // Mintemp.Add(r.main.temp_min);
                /// Wind.Add(r.wind.speed);
                cname.Add(r.name);
                description.Add(r.weather[0].description);
                addIcon(r);
                addBackground(r);

                IsolatedStorageSettings settingpage = IsolatedStorageSettings.ApplicationSettings;

                string description1 = description[0].ToString();
                if (settingpage.Contains("description1"))
                {
                    settingpage["description1"] = description1;

                }
                else
                {

                    settingpage.Add("description1", description1);
                }

                string cityname = cname[0].ToString();
                if (settingpage.Contains("cityname"))
                {
                    settingpage["cityname"] = cityname;

                }
                else
                {

                    settingpage.Add("cityname", cityname);
                }

            }
            else
            {

                MessageBox.Show("Something went wrong. Please try again after sometime!");


            }
        }

        private void wbchome_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.Error == null)
            {


                DayWiseWeather.RootObject d = null;
                d = JsonConvert.DeserializeObject<DayWiseWeather.RootObject>(e.Result);
                //Test = new ObservableCollection<string>();
                //Test.Add(d.city.name);
                ///MessageBox.Show(Test[0]);
                ///
                //cname.Add(d.city.name);
                // Daylist.Add(d.list[0]);
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
                Maxtemp.Add(d.list[0].temp.max);
                Temp.Add(temperature);
                Humid.Add(d.list[0].humidity);
                Pressure.Add(d.list[0].pressure);

                Mintemp.Add(d.list[0].temp.min);
                Wind.Add(d.list[0].speed);
                Daylist.Add(d.list[1]);
                Daylist.Add(d.list[2]);
                Daylist.Add(d.list[3]);
                Daylist.Add(d.list[4]);
                Daylist.Add(d.list[5]);
                Daylist.Add(d.list[6]);
                addIcon2(d);
                CurrentDay = Daylist[0];



                IsolatedStorageSettings settingpage = IsolatedStorageSettings.ApplicationSettings;

               
                if (settingpage.Contains("Temp"))
                {
                    settingpage["Temp"] = temperature;

                }
                else
                {

                    settingpage.Add("Temp", temperature);
                }

                string mintemperature = Mintemp[0].ToString();
                if (settingpage.Contains("Mintemp"))
                {
                    settingpage["Mintemp"] = mintemperature;

                }
                else
                {

                    settingpage.Add("Mintemp", mintemperature);
                }

                string maxtemperature = Maxtemp[0].ToString();
                if (settingpage.Contains("Mintemp"))
                {
                    settingpage["Maxtemp"] = maxtemperature;

                }
                else
                {

                    settingpage.Add("Maxtemp", maxtemperature);
                }
                string humidity = Humid[0].ToString();
                if (settingpage.Contains("Humidity"))
                {
                    settingpage["Humidity"] = humidity;

                }
                else
                {

                    settingpage.Add("Humidity", humidity);
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



                this.IsBusy = false;
            }
            else
            {
                MessageBox.Show("Something went wrong. Please try again after sometime!");
            }
        }

        private void checkUnits()
        {
           // throw new NotImplementedException();
            IsolatedStorageSettings settingpage = IsolatedStorageSettings.ApplicationSettings;
            if( settingpage.Contains("Units"))
            {
                if (settingpage["Units"].ToString().Equals("Metric"))
                {
                    units = "Metric";
                }
                else
                {
                    units = "Imperial";
                }
            }
        }

        private bool checkNetworkConnection()
        {
            //00throw new NotImplementedException();
            var ni = NetworkInterface.NetworkInterfaceType;

            bool IsConnected = false;
            if ((ni == NetworkInterfaceType.Wireless80211) || (ni == NetworkInterfaceType.MobileBroadbandCdma) || (ni == NetworkInterfaceType.MobileBroadbandGsm))
                IsConnected = true;
            else if (ni == NetworkInterfaceType.None)
                IsConnected = false;
            return IsConnected;
        }

        private async void gloc()
        {
            //throw new System.NotImplementedException();
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 1000;

            try
            {
                this.IsBusy = true;
                //MessageBox.Show("in geolocation");
                Geoposition geoposition = await geolocator.GetGeopositionAsync(
                 maximumAge: TimeSpan.FromMinutes(5),
                 timeout: TimeSpan.FromSeconds(10));


                //Geocoordinate x = await GetSinglePositionAsync();

                //lo = "";
                //lat = "";
                lat = geoposition.Coordinate.Latitude.ToString("00.000");
                lo = geoposition.Coordinate.Longitude.ToString("00.000");
                //MessageBox.Show("in geolocation");
                //MessageBox.Show("in fetchin data");
                //lat = "12.3";
                //lo = "76.6";
                WebClient w = new WebClient();


                w.DownloadStringCompleted += w_DownloadStringCompleted;
                w.DownloadStringAsync(new System.Uri("http://api.openweathermap.org/data/2.5/weather?lat=" + lat + "&lon=" + lo + "&units="+units+"&APPID= 8a5cad6deaac563e8625e20f01b4ca4b"));


                WebClient wbc = new WebClient();
                wbc.DownloadStringCompleted += wbc_DownloadStringCompleted;
                wbc.DownloadStringAsync(new System.Uri("http://api.openweathermap.org/data/2.5/forecast/daily?lat=" + lat + "&lon=" + lo + "&mode=json&units="+units+"&cnt=7&APPID= 8a5cad6deaac563e8625e20f01b4ca4b"));
            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x80004004)
                {
                    // the application does not have the right capability or the location master switch is off
                    MessageBox.Show("location  is disabled in phone settings.");
                }
            }
        }

            void w_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            //throw new System.NotImplementedException();
           // string st;

            //MessageBox.Show("after geo");
            if (e.Error == null)
            {
                WeatherContext.RootObject r = null;
                r = JsonConvert.DeserializeObject<WeatherContext.RootObject>(e.Result);
                //MessageBox.Show(r.main.ToString());

                //Temp.Add(r.main.temp);
                //Humid.Add(r.main.humidity);
               // Pressure.Add(r.main.pressure);
               // Maxtemp.Add(r.main.temp_max);
               // Mintemp.Add(r.main.temp_min);
               /// Wind.Add(r.wind.speed);
                cname.Add(r.name);
                description.Add(r.weather[0].description);
                addIcon(r);
                addBackground(r);

                IsolatedStorageSettings settingpage = IsolatedStorageSettings.ApplicationSettings;
               
                string description1 = description[0].ToString();
                if (settingpage.Contains("description1"))
                {
                    settingpage["description1"] = description1;

                }
                else
                {

                    settingpage.Add("description1", description1);
                }

                string cityname = cname[0].ToString();
                if (settingpage.Contains("cityname"))
                {
                    settingpage["cityname"] = cityname;

                }
                else
                {

                    settingpage.Add("cityname", cityname);
                }

            }
            else
            {

                MessageBox.Show("Something went wrong. Please try again after sometime!");
                

            }

        }

           

         public string returnsback()
            {
                return Background[0];
            }
            private void addBackground(WeatherContext.RootObject r)
            {
               // throw new NotImplementedException();
                if(r.weather[0].id == 804)
                {
                    Background.Add("Assets/Background/overcast.jpg");
                }
                else if (r.weather[0].id == 802)
                {
                    Background.Add("Assets/Background/scatteredclouds.jpg");
                }
                else if (r.weather[0].id == 801)
                {
                    if (r.weather[0].icon.Contains("n"))
                    {
                        Background.Add("Assets/Background/nightfewclouds.jpg");
                    }
                    else
                        Background.Add("Assets/Background/fewclouds.jpg");
                    
                    
                }
                else if(r.weather[0].id == 600 || r.weather[0].id == 601 || r.weather[0].id == 602 || r.weather[0].id == 611 || r.weather[0].id == 612 || r.weather[0].id == 615 || r.weather[0].id == 616 || r.weather[0].id == 620 || r.weather[0].id == 621 || r.weather[0].id == 622)
                {
                    if (r.weather[0].icon.Contains("n"))
                    {
                        Background.Add("Assets/Background/nightsnow.jpg");
                    }
                    else
                        Background.Add("Assets/Background/snow.jpg");
                    
                }
                else if ( r.weather[0].id == 301 || r.weather[0].id == 500 || r.weather[0].id == 302 || r.weather[0].id == 310 || r.weather[0].id == 311 || r.weather[0].id == 312 || r.weather[0].id == 313 || r.weather[0].id == 314 )
                {
                    Background.Add("Assets/Background/lightrain.jpg");
                }
                else if (r.weather[0].id == 501 || r.weather[0].id == 502 || r.weather[0].id == 503 || r.weather[0].id == 504 || r.weather[0].id == 511 || r.weather[0].id == 520 || r.weather[0].id == 521 || r.weather[0].id == 522 || r.weather[0].id == 531)
                {
                    Background.Add("Assets/Background/heavyrain.jpg");
                }
                else if (r.weather[0].id == 300 || r.weather[0].id == 321)
                {
                    Background.Add("Assets/Background/rain.jpg");
                }
                else
                {
                    if(r.weather[0].icon.Contains("n"))
                    {
                        Background.Add("Assets/Background/nightclear.jpg");
                    }
                    else
                    Background.Add("Assets/Background/clearsky.jpg");

                }
            }

            private void addIcon(WeatherContext.RootObject r)
            {
                string st;
                if (r.weather[0].icon.Contains("n"))
                {
                    st = "Assets/Weathericonwhite/";
                }
                else
                {
                    st = "Assets/Weathericonwhite/";
                }

                if (r.weather[0].id == 800)
                {
                    Iconpath.Add(st + "Weather.png");
                }
                else if (r.weather[0].id == 802 || r.weather[0].id == 803 || r.weather[0].id == 801)
                {
                    Iconpath.Add(st + "Cloud-Sun.png");

                }
                else if (r.weather[0].id == 600 || r.weather[0].id == 601 || r.weather[0].id == 602 || r.weather[0].id == 611 || r.weather[0].id == 612 || r.weather[0].id == 615 || r.weather[0].id == 616 || r.weather[0].id == 620 || r.weather[0].id == 621 || r.weather[0].id == 622)
                {
                    Iconpath.Add(st + "Weather-Snow.png");
                }
                else if (r.weather[0].id == 804)
                {
                    Iconpath.Add(st + "Overcast-Weather.png");
                }
                else if (r.weather[0].id == 300 || r.weather[0].id == 301 || r.weather[0].id == 302 || r.weather[0].id == 310 || r.weather[0].id == 311 || r.weather[0].id == 312 || r.weather[0].id == 616 || r.weather[0].id == 313 || r.weather[0].id == 314 || r.weather[0].id == 321)
                {
                    Iconpath.Add(st + "Weather-Rain.png");
                }
                else if (r.weather[0].id == 500 || r.weather[0].id == 501 || r.weather[0].id == 502 || r.weather[0].id == 503 || r.weather[0].id == 504 || r.weather[0].id == 511 || r.weather[0].id == 520 || r.weather[0].id == 521 || r.weather[0].id == 522 || r.weather[0].id == 531)
                {
                    Iconpath.Add(st + "Weather-Rain.png");
                }
                else if (r.weather[0].id == 900 || r.weather[0].id == 902 || r.weather[0].id == 960 || r.weather[0].id == 962)
                {
                    Iconpath.Add(st + "Hurricane.png");
                }
                else if (r.weather[0].id == 901 || r.weather[0].id == 905 || r.weather[0].id == 961 || r.weather[0].id == 959)
                {
                    Iconpath.Add(st + "Cloud-Cyclone.png");
                }
                else if (r.weather[0].id == 201 || r.weather[0].id == 200 || r.weather[0].id == 210 || r.weather[0].id == 211 || r.weather[0].id == 212 || r.weather[0].id == 221 || r.weather[0].id == 230 || r.weather[0].id == 231 || r.weather[0].id == 232)
                {
                    Iconpath.Add(st + "Cloud-Cyclone.png");
                }
                else
                {
                    Iconpath.Add(st + "Weather.png");
                }

                /*switch (r.weather[0].main)
                {
                    case "clear":
                        Iconpath.Add(st+"Weather.png");
                        break;
                    default:
                        Iconpath.Add(st+"Weather.png");
                        break;
                }*/
                //throw new NotImplementedException();
            }

        
        void wbc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {


                DayWiseWeather.RootObject d = null;
                d = JsonConvert.DeserializeObject<DayWiseWeather.RootObject>(e.Result);
                //Test = new ObservableCollection<string>();
                //Test.Add(d.city.name);
                ///MessageBox.Show(Test[0]);
                ///
                //cname.Add(d.city.name);
                // Daylist.Add(d.list[0]);
               Maxtemp.Add(d.list[0].temp.max);
               Temp.Add(d.list[0].temp.morn);
               Humid.Add(d.list[0].humidity);
               Pressure.Add(d.list[0].pressure);
               
               Mintemp.Add(d.list[0].temp.min);
               Wind.Add(d.list[0].speed);
                Daylist.Add(d.list[1]);
                Daylist.Add(d.list[2]);
                Daylist.Add(d.list[3]);
                Daylist.Add(d.list[4]);
                Daylist.Add(d.list[5]);
                Daylist.Add(d.list[6]);
                addIcon2(d);
                CurrentDay = Daylist[0];



                IsolatedStorageSettings settingpage = IsolatedStorageSettings.ApplicationSettings;
                int temperature = Convert.ToInt32(Temp[0]);
                if(settingpage.Contains("Temp"))
                {
                    settingpage["Temp"] = temperature;

                }
                else
                {
                    
                    settingpage.Add("Temp", temperature);
                }

                string mintemperature = Mintemp[0].ToString();
                if (settingpage.Contains("Mintemp"))
                {
                    settingpage["Mintemp"] = mintemperature;

                }
                else
                {

                    settingpage.Add("Mintemp", mintemperature);
                }

                string maxtemperature = Maxtemp[0].ToString();
                if (settingpage.Contains("Mintemp"))
                {
                    settingpage["Maxtemp"] = maxtemperature;

                }
                else
                {

                    settingpage.Add("Maxtemp", maxtemperature);
                }
                string humidity = Humid[0].ToString();
                if (settingpage.Contains("Humidity"))
                {
                    settingpage["Humidity"] = humidity;

                }
                else
                {

                    settingpage.Add("Humidity",humidity);
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

                string d3temp = Convert.ToInt32( d.list[3].temp.day).ToString();
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

                if (settingpage.Contains("sethome"))
                {
                    settingpage["sethome"] = "no";
                }
                else
                {
                    settingpage.Add("sethome", "no");
                }


                this.IsBusy = false;
            }
            else
            {
                MessageBox.Show("Something went wrong. Please try again after sometime!");
            }

        }

        private void check()
        {
            //throw new NotImplementedException();
            if (Temp.Count == 1)
            {
                Temp.RemoveAt(0);
            }
            if (Humid.Count == 1)
            {
                Humid.RemoveAt(0);
            }
            if (Temp.Count == 1)
            {
                Temp.RemoveAt(0);
            } if (Mintemp.Count == 1)
            {
                Mintemp.RemoveAt(0);
            } if (Maxtemp.Count == 1)
            {
                Maxtemp.RemoveAt(0);
            } if (Pressure.Count == 1)
            {
                Pressure.RemoveAt(0);
            }
            if (description.Count == 1)
            {
                description.RemoveAt(0);
            }
            if (cname.Count == 1)
            {
                cname.RemoveAt(0);
            } if (Iconpath.Count == 1)
            {
                Iconpath.RemoveAt(0);
            } if (Wind.Count == 1)
            {
                Wind.RemoveAt(0);
            }
            int x = Daylist.Count;
            while (x != 0)
            {

                Daylist.RemoveAt(x - 1);
                x--;

            }
            if (Background.Count == 1)
            {
                Background.RemoveAt(0);
            }
        }


        private void addIcon2(DayWiseWeather.RootObject d)
        {
            //throw new NotImplementedException();
            if (d.list[1].weather[0].id == 0)
            {

            }

        }

        private void initob()
        {
            //throw new System.NotImplementedException();
            Temp = new ObservableCollection<double>();
            Daylist = new ObservableCollection<DayWiseWeather.L>();
            //Temp = new ObservableCollection<double>();
            Humid = new ObservableCollection<double>();
            Mintemp = new ObservableCollection<double>();
            Maxtemp = new ObservableCollection<double>();
            Pressure = new ObservableCollection<double>();
            //Place = new ObservableCollection<string>();
            Iconpath = new ObservableCollection<string>();
            description = new ObservableCollection<string>();
            cname = new ObservableCollection<string>();
            Wind = new ObservableCollection<double>();
            Background = new ObservableCollection<string>();
        }

        private void RefreshData()
        {
            IsolatedStorageSettings settingpage = IsolatedStorageSettings.ApplicationSettings;
            if (settingpage.Contains("Units"))
            {
                


                    if (!settingpage["sethome"].ToString().Equals(settingpage["Currentsethome"].ToString()))
                    {
                        initob();
                        checkUnits();

                        if (settingpage.Contains("sethome"))
                        {
                            if (settingpage["sethome"].ToString().Equals("yes"))
                            {
                                if (settingpage.Contains("home"))
                                {
                                    homecity = settingpage["home"].ToString();
                                }
                                homeset();
                            }
                            else
                            {
                                gloc();
                            }

                        }
                    }
                      else  if (!settingpage["Units"].ToString().Equals(settingpage["Current"].ToString()))
                    
                    {
                          initob();
                        checkUnits();

                        if (settingpage.Contains("sethome"))
                        {
                            if (settingpage["sethome"].ToString().Equals("yes"))
                            {
                                if (settingpage.Contains("home"))
                                {
                                    homecity = settingpage["home"].ToString();
                                }
                                homeset();
                            }
                            else
                            {
                                gloc();
                            }

                        }
                      }

                    else  if (!settingpage["Units"].ToString().Equals(settingpage["Current"].ToString()) && !settingpage["sethome"].ToString().Equals(settingpage["Currentsethome"].ToString()))
                        {



                        // check();
                        initob();
                        checkUnits();

                        if (settingpage.Contains("sethome"))
                        {
                            if (settingpage["sethome"].ToString().Equals("yes"))
                            {
                                if (settingpage.Contains("home"))
                                {
                                    homecity = settingpage["home"].ToString();
                                }
                                homeset();
                            }
                            else
                            {
                                gloc();
                            }

                        }
                        else
                        {
                            gloc();
                        }
                    }
                
            }
        }
    }
}