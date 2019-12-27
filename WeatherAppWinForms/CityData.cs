using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherAppWinForms
{
    public class CityData
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public List<string> RainTime { get; set; }
        public string Latitude { get; set; }
        public string Longtitude { get; set; }
        public List<string> RainIn3H { get; set; }
        public List<string> Temp {get;set;}
        public List<string> MaxTemp { get; set; }
        public List<string> MinTemp { get; set; }
        public string CountryName { get; set; }
        public List<string> Main { get; set; }
        public List<string> Description { get; set; }

        public CityData()
        {
            RainTime = new List<string>();
            RainIn3H = new List<string>();
            Temp = new List<string>();
            Temp = new List<string>();
            MaxTemp = new List<string>();
            MinTemp = new List<string>();
            Main = new List<string>();
            Description = new List<string>();
        }
    }
}
