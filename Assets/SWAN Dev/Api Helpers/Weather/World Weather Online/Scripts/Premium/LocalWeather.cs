using System;
using System.Collections.Generic;
//using System.Linq;

namespace premium.localweather
{
    public class LocalWeatherInput
    {
        public string query { get; set; }
        public string format { get; set; }
        public string extra { get; set; }
        public string num_of_days { get; set; }
        public string date { get; set; }
        public string fx { get; set; }
        public string cc { get; set; }
        public string includelocation { get; set; }
        public string show_comments { get; set; }
        public string tp { get; set; }
        public string callback { get; set; }
    }

    public class LocalWeather
    {
        public Data data;
    }

    public class Data
    {
        public List<ClimateAverages> climateAverages;
        public List<Current_Condition> current_Condition;
        public List<Request> request;
        public List<Weather> weather;
    }

    public class ClimateAverages
    {
        public List<Month> month;
    }

    public class Month
    {
        public int index { get; set; }
        public string name { get; set; }
        public float avgMinTemp { get; set; }
        public float avgMinTemp_F { get; set; }
        public float avgMaxTemp { get; set; }
        public float avgMaxTemp_F { get; set; }
        public float absMinTemp { get; set; }
        public float absMinTemp_F { get; set; }
        public float absMaxTemp { get; set; }
        public string avgDailyRainfall { get; set; }
        public string avgMonthlyRainfall { get; set; }
        public int avgDryDays { get; set; }
        public int avgSnowDays { get; set; }
        public int avgFogDays { get; set; }

    }

    public class Current_Condition
    {
        public DateTime observation_time { get; set; }
        public DateTime localObsDateTime { get; set; }
        public int temp_C { get; set; }
        public int windspeedMiles { get; set; }
        public int windspeedKmph { get; set; }
        public int winddirDegree { get; set; }
        public string winddir16Point { get; set; }
        public string weatherCode { get; set; }
        public List<WeatherDesc> weatherDesc { get; set; }
        public List<WeatherIconUrl> weatherIconUrl { get; set; }
        public float precipMM { get; set; }
        public float precipInches { get; set; }
        public float humidity { get; set; }
        public int visibility { get; set; }
        public int visibilityMiles { get; set; }
        public int pressure { get; set; }
        public float pressureInches { get; set; }
        public int cloudcover { get; set; }
        public int chanceofrain { get; set; }
        public int chanceofwindy { get; set; }
        public int chanceofovercast { get; set; }
        public int chanceofsunny { get; set; }
        public int chanceoffrost { get; set; }
        public int chanceoffog { get; set; }
        public int chanceofsnow { get; set; }
        public int chanceofthunder { get; set; }
    }

    public class Request
    {
        public string query { get; set; }
        public string type { get; set; }
    }

    public class Weather
    {
        public List<Astronomy> astronomy { get; set; }
        public List<Hourly> hourly { get; set; }
        public DateTime date { get; set; }
        public int maxtempC { get; set; }
        public int maxtempF { get; set; }
        public int mintempC { get; set; }
        public int mintempF { get; set; }
        public int windspeedKmph { get; set; }
        public int winddirDegree { get; set; }
        public string winddir16Point { get; set; }
        public int weatherCode { get; set; }
        public List<WeatherDesc> weatherDesc { get; set; }
        public List<WeatherIconUrl> weatherIconUrl { get; set; }
        public float precipMM { get; set; }
    }

    public class Astronomy
    {
        public DateTime sunrise { get; set; }
        public DateTime sunset { get; set; }
        public DateTime moonrise { get; set; }
        public DateTime moonset { get; set; }
    }

    public class Hourly
    {
        public string time { get; set; }
        public int tempC { get; set; }
        public int tempF { get; set; }
        public int windspeedMiles { get; set; }
        public int windspeedKmph { get; set; }
        public int windspeedKnots { get; set; }
        public int windspeedMeterSec { get; set; }
        public int winddirDegree { get; set; }
        public string winddir16Point { get; set; }
        public int weatherCode { get; set; }
        public List<WeatherDesc> weatherDesc { get; set; }
        public List<WeatherIconUrl> weatherIconUrl { get; set; }
        public float precipMM { get; set; }
        public float precipInches { get; set; }
        public float humidity { get; set; }
        public int visibility { get; set; }
        public int visibilityMiles { get; set; }
        public int pressure { get; set; }
        public float pressureInches { get; set; }
        public int cloudcover { get; set; }
        public int chanceofrain { get; set; }
        public int chanceofwindy { get; set; }
        public int chanceofovercast { get; set; }
        public int chanceofsunny { get; set; }
        public int chanceoffrost { get; set; }
        public int chanceoffog { get; set; }
        public int chanceofsnow { get; set; }
        public int chanceofthunder { get; set; }
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