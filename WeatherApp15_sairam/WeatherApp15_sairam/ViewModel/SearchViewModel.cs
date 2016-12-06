using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ModelClassLibrary.Model;
using System.IO.IsolatedStorage;

namespace WeatherApp15_sairam.ViewModel
{
    public class SearchViewModel : ViewModelBase
    {

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
        private ObservableCollection<DayWiseWeather.L> _daylist = null;
        public ObservableCollection<DayWiseWeather.L> Daylist
        {
            get
            {
                return _daylist;
            }
            set
            {
                if (_daylist == value)
                {
                    return;
                }

                _daylist = value;
                RaisePropertyChanged("Daylist");
            }
        }

        private ObservableCollection<double> _temp= null;
        public ObservableCollection<double> Temp
        {
            get
            {
                return _temp;
            }
            set
            {
                if (_temp == value)
                {
                    return;
                }
                _temp = value;
                RaisePropertyChanged("temp");
            }
        }

        private string units = "metric";
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
        private ObservableCollection<string> _iconpath;
        public ObservableCollection<string> Iconpath
        {
            get
            {
                return _iconpath;
            }
            set
            {
                if (_iconpath == value)
                {
                    return;
                }
                _iconpath = value;
                RaisePropertyChanged("iconpath");
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


        private string _M;
        public string M
        {
            get { return _M; }
            set { _M = value; RaisePropertyChanged("M"); }
        }
        private bool _IsBusy;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set { _IsBusy = value; RaisePropertyChanged("IsBusy"); }
        }

        public RelayCommand _ShowProgressBar { get; set; }


        private void ShowProgressBar()
        {
            IsBusy = true;

            M = "Loading..";
        }

        private string _city = "";

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
        public RelayCommand StartSearchCommand { get; private set; }
        public SearchViewModel()
        {
            if (IsInDesignMode)
            {
                /*Temperature = new ObservableCollection<double>();
                Temperature.Add(20);*/
                Temp = new ObservableCollection<double>();
                Temp.Add(23.45);
                cname = new ObservableCollection<string>();
                cname.Add("Cuttack");
                description = new ObservableCollection<string>();
                description.Add("sky is clear la lal al ala");
                Iconpath = new ObservableCollection<string>();
                Iconpath.Add("Assets/Weathericondark/Weather.png");
                Background = new ObservableCollection<string>();
                Background.Add("Assets/Background/clearsky.jpg");
            }
            else
            {
                newObjects();
                

                //Temperature.Add(24);
                

                StartSearchCommand = new RelayCommand(Search_City);



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
            if(Background.Count == 1)
            {
                Background.RemoveAt(0);
            }
        }

        private void newObjects()
        {
            //throw new NotImplementedException();
            Temp = new ObservableCollection<double>();
            //Daylist = new ObservableCollection<DayWiseWeather.L>();
            //Temp = new ObservableCollection<double>();
            Humid = new ObservableCollection<double>();
            Mintemp = new ObservableCollection<double>();
            Maxtemp = new ObservableCollection<double>();
            Pressure = new ObservableCollection<double>();
            //Place = new ObservableCollection<string>();
            //Iconpath = new ObservableCollection<string>();
            description = new ObservableCollection<string>();
            cname = new ObservableCollection<string>();
            Wind = new ObservableCollection<double>();
            Iconpath = new ObservableCollection<string>();
            Daylist = new ObservableCollection<DayWiseWeather.L>();
            Background = new ObservableCollection<string>();

        }
        private void Search_City()
        {
            if (string.IsNullOrEmpty(City))
            {
                return;

            }

            this.IsBusy = true;
            check();
            checkUnits();
            WebClient w = new WebClient();
            w.DownloadStringCompleted += w_downloadStringCompleted;
            w.DownloadStringAsync(new System.Uri("http://api.openweathermap.org/data/2.5/find?q="+City+"&mode=json&units="+units+"&type=like&APPID= 8a5cad6deaac563e8625e20f01b4ca4b"));

            WebClient wbc = new WebClient();
            wbc.DownloadStringCompleted += wbc_DownloadStringCompleted;
            wbc.DownloadStringAsync(new System.Uri("http://api.openweathermap.org/data/2.5/forecast/daily?q="+City+"&mode=json&units="+units+"&cnt=7&type=like&APPID= 8a5cad6deaac563e8625e20f01b4ca4b"));
        }

        private void w_downloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {


                SearchWeather.RootObject r = null;

                r = JsonConvert.DeserializeObject<SearchWeather.RootObject>(e.Result);
                //MessageBox.Show(r.main.ToString());
                //Temp = new ObservableCollection<double>();

                if (r.count == 0)
                {

                    MessageBox.Show("Seems like you have entered a wrong city");
                    return;


                }
                else
                {



                    //Temp.Add(r.list[0].main.temp);

                    //Humid.Add(r.list[0].main.humidity);

                    //Pressure.Add(r.list[0].main.pressure);

                    //Maxtemp.Add(r.list[0].main.temp_max);

                    //Mintemp.Add(r.list[0].main.temp_min);

                    //Wind.Add(r.list[0].wind.speed);
                    IsolatedStorageSettings settingpage = IsolatedStorageSettings.ApplicationSettings;
                   
                    cname.Add(r.list[0].name);
                    if (settingpage.Contains("searchedresult"))
                    {
                        settingpage["searchedresult"] = cname[0].ToString();
                    }
                    else
                    {
                        settingpage.Add("searchedresult", cname[0].ToString());
                    }


                    description.Add(r.list[0].weather[0].description);

                    string st;

                    if (r.list[0].weather[0].icon.Contains("n"))
                    {

                        st = "Assets/Weathericonwhite/";

                    }

                    else
                    {

                        st = "Assets/Weathericonwhite/";

                    }

                    if (r.list[0].weather[0].id == 800)
                    {
                        Iconpath.Add(st + "Weather.png");
                    }
                    else if (r.list[0].weather[0].id == 802 || r.list[0].weather[0].id == 803 || r.list[0].weather[0].id == 801)
                    {
                        Iconpath.Add(st + "Cloud-Sun.png");

                    }
                    else if (r.list[0].weather[0].id == 600 || r.list[0].weather[0].id == 601 || r.list[0].weather[0].id == 602 || r.list[0].weather[0].id == 611 || r.list[0].weather[0].id == 612 || r.list[0].weather[0].id == 615 || r.list[0].weather[0].id == 616 || r.list[0].weather[0].id == 620 || r.list[0].weather[0].id == 621 || r.list[0].weather[0].id == 622)
                    {
                        Iconpath.Add(st + "Weather-Snow.png");
                    }
                    else if (r.list[0].weather[0].id == 804)
                    {
                        Iconpath.Add(st + "Overcast-Weather.png");
                    }
                    else if (r.list[0].weather[0].id == 300 || r.list[0].weather[0].id == 301 || r.list[0].weather[0].id == 302 || r.list[0].weather[0].id == 310 || r.list[0].weather[0].id == 311 || r.list[0].weather[0].id == 312 || r.list[0].weather[0].id == 616 || r.list[0].weather[0].id == 313 || r.list[0].weather[0].id == 314 || r.list[0].weather[0].id == 321)
                    {
                        Iconpath.Add(st + "Weather-Rain.png");
                    }
                    else if (r.list[0].weather[0].id == 500 || r.list[0].weather[0].id == 501 || r.list[0].weather[0].id == 502 || r.list[0].weather[0].id == 503 || r.list[0].weather[0].id == 504 || r.list[0].weather[0].id == 511 || r.list[0].weather[0].id == 520 || r.list[0].weather[0].id == 521 || r.list[0].weather[0].id == 522 || r.list[0].weather[0].id == 531)
                    {
                        Iconpath.Add(st + "Weather-Rain.png");
                    }
                    else if (r.list[0].weather[0].id == 900 || r.list[0].weather[0].id == 902 || r.list[0].weather[0].id == 960 || r.list[0].weather[0].id == 962)
                    {
                        Iconpath.Add(st + "Hurricane.png");
                    }
                    else if (r.list[0].weather[0].id == 901 || r.list[0].weather[0].id == 905 || r.list[0].weather[0].id == 961 || r.list[0].weather[0].id == 959)
                    {
                        Iconpath.Add(st + "Cloud-Cyclone.png");
                    }
                    else if (r.list[0].weather[0].id == 201 || r.list[0].weather[0].id == 200 || r.list[0].weather[0].id == 210 || r.list[0].weather[0].id == 211 || r.list[0].weather[0].id == 212 || r.list[0].weather[0].id == 221 || r.list[0].weather[0].id == 230 || r.list[0].weather[0].id == 231 || r.list[0].weather[0].id == 232)
                    {
                        Iconpath.Add(st + "Cloud-Cyclone.png");
                    }
                    else
                    {
                        Iconpath.Add(st + "Weather.png");
                    }
                    getBackground(r);
                }
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
            if (settingpage.Contains("Units"))
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

        private void getBackground(SearchWeather.RootObject r)
        {
           // throw new NotImplementedException();
            if (r.list[0].weather[0].id == 804)
            {
                Background.Add("Assets/Background/overcast.jpg");
            }
            else if (r.list[0].weather[0].id == 802)
            {
                Background.Add("Assets/Background/scatteredclouds.jpg");
            }
            else if (r.list[0].weather[0].id == 801)
            {
                if (r.list[0].weather[0].icon.Contains("n"))
                {
                    Background.Add("Assets/Background/nightfewclouds.jpg");
                }
                else
                    Background.Add("Assets/Background/fewclouds.jpg");


            }
            else if (r.list[0].weather[0].id == 600 || r.list[0].weather[0].id == 601 || r.list[0].weather[0].id == 602 || r.list[0].weather[0].id == 611 || r.list[0].weather[0].id == 612 || r.list[0].weather[0].id == 615 || r.list[0].weather[0].id == 616 || r.list[0].weather[0].id == 620 || r.list[0].weather[0].id == 621 || r.list[0].weather[0].id == 622)
            {
                if (r.list[0].weather[0].icon.Contains("n"))
                {
                    Background.Add("Assets/Background/nightsnow.jpg");
                }
                else
                    Background.Add("Assets/Background/snow.jpg");

            }
            else if (r.list[0].weather[0].id == 301 || r.list[0].weather[0].id == 500 || r.list[0].weather[0].id == 302 || r.list[0].weather[0].id == 310 || r.list[0].weather[0].id == 311 || r.list[0].weather[0].id == 312 || r.list[0].weather[0].id == 313 || r.list[0].weather[0].id == 314)
            {
                Background.Add("Assets/Background/lightrain.jpg");
            }
            else if (r.list[0].weather[0].id == 501 || r.list[0].weather[0].id == 502 || r.list[0].weather[0].id == 503 || r.list[0].weather[0].id == 504 || r.list[0].weather[0].id == 511 || r.list[0].weather[0].id == 520 || r.list[0].weather[0].id == 521 || r.list[0].weather[0].id == 522 || r.list[0].weather[0].id == 531)
            {
                Background.Add("Assets/Background/heavyrain.jpg");
            }
            else if (r.list[0].weather[0].id == 300 || r.list[0].weather[0].id == 321)
            {
                Background.Add("Assets/Background/rain.jpg");
            }
            else
            {
                if (r.list[0].weather[0].icon.Contains("n"))
                {
                    Background.Add("Assets/Background/nightclear.jpg");
                }
                else
                    Background.Add("Assets/Background/clearsky.jpg");

            }
        }

        void wbc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (e.Result.ToString().Contains("404"))
                {
                    MessageBox.Show("Seems like you have entered a wrong city,Please search again!");
                    return;
                    
                }
                else
                {
                    DayWiseWeather.RootObject d = null;
                    d = JsonConvert.DeserializeObject<DayWiseWeather.RootObject>(e.Result);
                    //Test = new ObservableCollection<string>();
                    //Test.Add(d.city.name);
                    ///MessageBox.Show(Test[0]);
                    ///
                    //cname.Add(d.city.name);]

                    //Daylist.Add(d.list[0]);
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
                    CurrentDay = Daylist[0];


                    

                    this.IsBusy = false;
                }

            }
            else
            {
                MessageBox.Show("Something went wrong. Please try again after sometime!");

            }
        }
    }
}
