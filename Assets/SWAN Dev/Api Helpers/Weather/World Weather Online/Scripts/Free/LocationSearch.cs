using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;

namespace free.locationsearch
{
    public class LocationSearchInput
    {
        public string query { get; set; }
        public string timezone { get; set; }
        public string popular { get; set; }
        public string num_of_results { get; set; }
        public string format { get; set; }
        public string callback { get; set; }
    }

    public class LocationSearch
    {
        public Search_API search_API;
    }

    public class Search_API
    {
        public List<Result> result;
    }

    public class Result
    {
        public List<AreaName> areaName { get; set; }
        public List<Country> country { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public double population { get; set; }
        public List<Region> region { get; set; }
        public List<WeatherUrl> weatherUrl { get; set; }
        public TimeZone timezone { get; set; }
    }

    public class TimeZone
    {
        public float offset { get; set; }
    }

    public class AreaName
    {
        public string value { get; set; }
    }

    public class Country
    {
        public string value { get; set; }
    }

    public class Region
    {
        public string value { get; set; }
    }

    public class WeatherUrl
    {
        public string value { get; set; }
    }
}