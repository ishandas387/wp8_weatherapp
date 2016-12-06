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

namespace Midsizeback
{
    public partial class Midsizetileback : UserControl
    {
        public Midsizetileback()
        {
            InitializeComponent();
             updatetile();
        }

        private void updatetile()
        {
            //throw new NotImplementedException();

            IsolatedStorageSettings settingpage = IsolatedStorageSettings.ApplicationSettings;
            if (settingpage.Contains("d1temp"))
            {
                d1temp.Text = string.Format("{0:D}°", settingpage["d1temp"].ToString());
            }
            if (settingpage.Contains("d2temp"))
            {
                d2temp.Text = string.Format("{0:D}°", settingpage["d2temp"].ToString());
            }

            if (settingpage.Contains("d1mintemp"))
            {
                d1min.Text = string.Format("{0:D}°", settingpage["d1mintemp"].ToString());
            }
            if (settingpage.Contains("d2mintemp"))
            {
                d2min.Text = string.Format("{0:D}°", settingpage["d2mintemp"].ToString());
            }



            if (settingpage.Contains("d1maxtemp"))
            {
                d1max.Text = string.Format("{0:D}°", settingpage["d1maxtemp"].ToString());
            }
            if (settingpage.Contains("d2maxtemp"))
            {
                d2max.Text = string.Format("{0:D}°", settingpage["d2maxtemp"].ToString());
            }
            d1.Text = DateTime.Now.AddDays(1).DayOfWeek.ToString().Substring(0, 3);
            d2.Text = DateTime.Now.AddDays(2).DayOfWeek.ToString().Substring(0, 3);
          
        }
    }
}
