using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkOrders_DAL
{
    public static class Cache
    {
        public static class Keys
        {
            public static string Districts => "Districts";
            public static string LaborRate => "LaborRates";
            public static string Leadtechs => "Leadtechs";
            public static string Payments => "Payments";
            public static string Services => "Services";
            public static string Weekdays => "Weekdays";
            public static string WorkOrders => "WorkOrders";
            public static string Pdf => "Pdf";
        }

        public static class Options
        {
            public static class TimeSpans
            {
                public static TimeSpan Minute => TimeSpan.FromMinutes(1);
                public static TimeSpan Hour => TimeSpan.FromHours(1);
                public static TimeSpan Day => TimeSpan.FromDays(1);
            }
        }
    }
}
