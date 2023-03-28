using System;

namespace N225.Domain
{
    public sealed class Shared
    {
        public static string Password { get; set; } = String.Empty;       //= 
                                                                          //ConfigurationManager.AppSettings["Password"];   // "takao102769";


        public static string APIPassword { get; set; } = String.Empty;             //= 
                                                                                   //ConfigurationManager.AppSettings["APIPassword"];       //"takao100460";
        public static string XAPIkey { get; set; } = "9876543210";

        public static string Domein { get; set; } = @"http://localhost:";

        public static string WsDomain { get; set; } = @"ws://localhost:";

        public static string Port { get; set; } = "18080";
        public static double BidPrice { get; set; }
        public static double AskPrice { get; set; }
        public static string PathCsv { get; set; } = "N225/Csvfile/";

        public static TimeSpan StartTime { get; } =
                            new DateTime(01, 01, 01, 8, 45, 00).TimeOfDay;
        public static TimeSpan EndTime { get; } =
                            new DateTime(01, 01, 01, 15, 15, 00).TimeOfDay;
        public static TimeSpan NightStart { get; } =
                            new DateTime(01, 01, 01, 16, 30, 00).TimeOfDay;
        public static TimeSpan NightEnd { get; } =
                            new DateTime(01, 01, 01, 6, 00, 00).TimeOfDay;
    }
}
