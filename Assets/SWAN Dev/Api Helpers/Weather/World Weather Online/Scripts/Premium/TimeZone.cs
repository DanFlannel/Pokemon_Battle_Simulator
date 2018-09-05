using System;
using System.Collections.Generic;
//using System.Linq;

namespace premium.timezone
{
    /// <summary>
    /// Summary description for TimeZone
    /// </summary>
    public class TimeZoneInput
    {
        public string query { get; set; }
        public string format { get; set; }
        public string callback { get; set; }
    }

    public class Timezone
    {
        public Data data;
    }

    public class Data
    {
        public List<Request> request;
        public List<Time_Zone> time_zone;
    }

    public class Request
    {
        public string query { get; set; }
        public string type { get; set; }
    }

    public class Time_Zone
    {
        public DateTime localtime { get; set; }
        public float utcOffset { get; set; }
    }
}