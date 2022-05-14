using System;

namespace SiteActivityReporting.Common
{
    public class CommonHelper
    {
        public static int RoundToNearestNumber(double number)
        {
            return Convert.ToInt32(Math.Round(number));
        }

        public static DateTime PruneDateTime(int hour)
        {
            return DateTime.Now.AddHours(-hour);
        }
    }
}
