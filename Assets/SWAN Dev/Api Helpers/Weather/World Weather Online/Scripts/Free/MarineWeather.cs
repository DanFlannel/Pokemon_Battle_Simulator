using System;
using System.Collections.Generic;
//using System.Linq;

namespace free.marineweather
{
    public class MarineWeatherInput
    {
        public string query { get; set; }
        public string format { get; set; }
        public string fx { get; set; }
        public string callback { get; set; }
    }

    public class MarineWeather
    {
        public Data data;
    }

    public class Data
    {
        public List<Nearest_Area> nearest_area;
        public List<Request> request;
        public List<Weather> weather;
    }

    public class Nearest_Area
    {
        public float distance_miles { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
    }

    public class Weather
    {
        public DateTime date { get; set; }
        public List<Hourly> hourly { get; set; }
        public int maxtempC { get; set; }
        public int mintempC { get; set; }
    }

    public class Hourly
    {
        public int cloudcover { get; set; }
        public float humidity { get; set; }
        public int pressure { get; set; }
        public float sigHeight_m { get; set; }
        public float swellDir { get; set; }
        public float swellHeight_m { get; set; } 
        public float swellPeriod_secs { get; set; }
        public int tempC { get; set; }
        public int tempF { get; set; }
        public int time { get; set; }
        public int visibility { get; set; }
        public int waterTemp_C { get; set; }
        public int waterTemp_F { get; set; }
        public int windspeedMiles { get; set; }
        public int windspeedKmph { get; set; }
        public int winddirDegree { get; set; }
        public string winddir16Point { get; set; }
        public string weatherCode { get; set; }
        public List<WeatherIconUrl> weatherIconUrl { get; set; }
        public float precipMM { get; set; }
    }


    public class Request
    {
        public string query { get; set; }
        public string type { get; set; }
    }

    public class WeatherIconUrl
    {
        public string value { get; set; }
    }
}