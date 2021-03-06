﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClassLibrary.Model
{
    public class SearchWeather
    {
        public class Coord
        {
            public double lon { get; set; }
            public double lat { get; set; }
        }

        public class Main
        {
            public double temp { get; set; }
            public double pressure { get; set; }
            public double humidity { get; set; }
            public double temp_min { get; set; }
            public double temp_max { get; set; }
        }

        public class Wind
        {
            public double speed { get; set; }
            public double deg { get; set; }
        }

        public class Sys
        {
            public string country { get; set; }
        }

        public class Clouds
        {
            public int all { get; set; }
        }

        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

        public class List
        {
            public int id { get; set; }
            public string name { get; set; }
            public Coord coord { get; set; }
            public Main main { get; set; }
            public int dt { get; set; }
            public Wind wind { get; set; }
            public Sys sys { get; set; }
            public Clouds clouds { get; set; }
            public List<Weather> weather { get; set; }
        }

        public class RootObject
        {
            public string message { get; set; }
            public string cod { get; set; }
            public int count { get; set; }
            public List<List> list { get; set; }
        }
    }
}
