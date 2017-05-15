using System;

namespace WaxWelio.Entities
{
    public static class DataHelper
    {
        public static DateTime FromTimeStamp(this long time, double gtm = 0)
        {
            try
            {
                double seconds = time;
                var localTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return localTime.AddMilliseconds(seconds).AddHours(gtm);
            }
            catch
            {
                return DateTime.Now.Date;
            }
        }

        public static long ToTimeStamp(this DateTime dt, double gtm = 0)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var diff = (long) (dt.AddHours(gtm) - origin).TotalMilliseconds;
            return diff;
        }
    }
}