using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace ClassLibrary1
{
    public sealed partial class Midsize : UserControl
    {
        public Midsize()
        {
            this.InitializeComponent();

        }
        private void updateTileWithDetails()
        {
            //throw new NotImplementedException();
            ViewModelLocator vm = new ViewModelLocator();
            MainViewModel m = vm.Main;
            int a = m.Temp[0].ToString().IndexOf(".");
            int b = m.Temp[0].ToString().Length;
            int c = b - (a);
            tbTileTemp.Text = string.Concat(m.Temp[0].ToString().Remove(a, c), "°");
            tbTileDesc.Text = m.description[0];
            tbTileHumid.Text = string.Concat(m.Humid[0].ToString(), "%");
            tbTilePressure.Text = string.Concat(m.Pressure[0].ToString(), "mb");
            tbTileWind.Text = string.Concat(m.Wind[0].ToString(), "kmph");
            tbTileCity.Text = m.cname[0];
        }
    }
}
