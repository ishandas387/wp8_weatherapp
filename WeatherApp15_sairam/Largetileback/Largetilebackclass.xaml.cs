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

namespace Largetileback
{
    public partial class Largetilebackclass : UserControl
    {
        public Largetilebackclass()
        {
            InitializeComponent();
            updatetile();
        }

        private void updatetile()
        
            
        {
            //throw new NotImplementedException();
             timestp.Text = DateTime.Now.DayOfWeek + " " + DateTime.Now.ToString("HH:mm");
            IsolatedStorageSettings settingpage = IsolatedStorageSettings.ApplicationSettings;
            if(settingpage.Contains("d1temp"))
            {
                d1temp.Text = string.Format("{0:D}°", settingpage["d1temp"].ToString());
            }
            if (settingpage.Contains("d2temp"))
            {
                d2temp.Text = string.Format("{0:D}°", settingpage["d2temp"].ToString());
            }
            if (settingpage.Contains("d3temp"))
            {
                d3temp.Text = string.Format("{0:D}°", settingpage["d3temp"].ToString());
            }
            if (settingpage.Contains("d1mintemp"))
            {
                d1min.Text = string.Format("{0:D}°", settingpage["d1mintemp"].ToString());
            }
            if (settingpage.Contains("d2mintemp"))
            {
                d2min.Text = string.Format("{0:D}°", settingpage["d2mintemp"].ToString());
            }
            if (settingpage.Contains("d3mintemp"))
            {
                d3min.Text = string.Format("{0:D}°", settingpage["d3mintemp"].ToString());
            }


            if (settingpage.Contains("d1maxtemp"))
            {
                d1max.Text = string.Format("{0:D}°", settingpage["d1maxtemp"].ToString());
            }
            if (settingpage.Contains("d2maxtemp"))
            {
                d2max.Text = string.Format("{0:D}°", settingpage["d2maxtemp"].ToString());
            }
            if (settingpage.Contains("d3maxtemp"))
            {
                d3max.Text = string.Format("{0:D}°", settingpage["d3maxtemp"].ToString());
            }

            d1.Text = DateTime.Now.AddDays(1).DayOfWeek.ToString().Substring(0, 3);
            d2.Text = DateTime.Now.AddDays(2).DayOfWeek.ToString().Substring(0, 3);
            d3.Text = DateTime.Now.AddDays(3).DayOfWeek.ToString().Substring(0, 3);

        
        }
    }
}
