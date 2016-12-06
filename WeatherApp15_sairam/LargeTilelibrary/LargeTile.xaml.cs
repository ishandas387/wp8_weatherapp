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

namespace LargeTilelibrary
{
    public partial class LargeTile : UserControl
    {
        public LargeTile()
        {
            InitializeComponent();
            updateTileWithDetails();
        }
        private void updateTileWithDetails()
        {
            tbtimestamp.Text = DateTime.Now.DayOfWeek + " " + DateTime.Now.ToString("HH:mm");
            IsolatedStorageSettings settingpage = IsolatedStorageSettings.ApplicationSettings;
            if (settingpage.Contains("Temp"))
            {
                if (settingpage.Contains("Units"))
                {
                    if (settingpage["Units"].Equals("Metric"))
                    {
                        tbTileTemp.Text = string.Format("{0:D}°C", settingpage["Temp"].ToString());
                    }
                    else
                    {
                        tbTileTemp.Text = string.Format("{0:D}°F", settingpage["Temp"].ToString());
                    }
                }

            }
            if (settingpage.Contains("Maxtemp"))
            {
                if (settingpage.Contains("Units"))
                {
                    if (settingpage["Units"].Equals("Metric"))
                    {
                        tbTileMaxTemp.Text = string.Format("{0:D}°", settingpage["Maxtemp"].ToString());
                    }
                    else
                    {
                        tbTileMaxTemp.Text = string.Format("{0:D}°", settingpage["Maxtemp"].ToString());
                    }
                }

            }
            if (settingpage.Contains("Mintemp"))
            {
                if (settingpage.Contains("Units"))
                {
                    if (settingpage["Units"].Equals("Metric"))
                    {
                        tbTileMinTemp.Text = string.Format("{0:D}°", settingpage["Mintemp"].ToString());
                    }
                    else
                    {
                        tbTileMinTemp.Text = string.Format("{0:D}°", settingpage["Mintemp"].ToString());
                    }
                }

            }
            if (settingpage.Contains("Humidity"))
            {

                tbTileHumid.Text = string.Format("{0:D}%", settingpage["Humidity"].ToString());



            }
            if (settingpage.Contains("cityname"))
            {

                tbTileCity.Text = settingpage["cityname"].ToString();



            }
            if (settingpage.Contains("description1"))
            {

                tbTileDesc.Text = settingpage["description1"].ToString();



            }


        }
    }
}
