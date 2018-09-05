using System;
using System.Collections.Generic;
//using System.Linq;

namespace premium.marineweather
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
        public List<Request> request;
        public List<Weather> weather;
    }

    public class Weather
    {
        public DateTime date { get; set; }
        public List<Astronomy> astronomy { get; set; }
        public List<Hourly> hourly { get; set; }
        public int maxtempC { get; set; }
        public int mintempC { get; set; }
        public int maxtempF { get; set; }
        public int mintempF { get; set; }
    }

    public class Astronomy
    {
        public string sunrise { get; set; }
        public string sunset { get; set; }
        public string moonrise { get; set; }
        public string moonset { get; set; }
    }

    public class Hourly
    {
        public int cloudcover { get; set; }
        public float humidity { get; set; }
        public int pressure { get; set; }
        public float sigHeight_m { get; set; }
        public float swellDir { get; set; }
        public string swellDir16Point { get; set; }
        public float swellHeight_m { get; set; }
        public float swell_Height_ft { get; set; } 
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
        public List<WeatherDesc> weatherDesc { get; set; }
        public List<WeatherIconUrl> weatherIconUrl { get; set; }
        public float precipMM { get; set; }
    }


    public class Request
    {
        public string query { get; set; }
        public string type { get; set; }
    }

    public class WeatherDesc
    {
        public string value { get; set; }
    }

    public class WeatherIconUrl
    {
        public string value { get; set; }
    }
}