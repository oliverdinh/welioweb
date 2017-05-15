using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WaxWelio.Common
{
    public static class Utils
    {
        public static string ConvertToUnSign(string text, string textBySpace = null)
        {
            for (var i = 33; i < 48; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (var i = 58; i < 65; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (var i = 91; i < 97; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (var i = 123; i < 127; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            if (!string.IsNullOrEmpty(textBySpace))
            {
                text = text.Replace(" ", textBySpace);
            }

            var regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");

            var strFormD = text.Normalize(NormalizationForm.FormD);

            return regex.Replace(strFormD, string.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp, double addTime = 0)
        {
            try
            {
                double seconds = unixTimeStamp;
                var localTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return localTime.AddMilliseconds(seconds).AddHours(addTime);
            }
            catch
            {
                return DateTime.Now.Date;
            }
        }

        public static long ConvertToTimestamp(DateTime value, double addTime = 0)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var diff = (long)(value.AddHours(-addTime) - origin).TotalMilliseconds;
            return diff;
        }

        public static string ConvertListToString(IEnumerable<object> list, string seperate)
        {
            return list.Aggregate("", (current, item) => current + (item.ToString() + seperate));
        }


        public static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public static string GetLast(this string source, int tailLength)
        {
            return tailLength >= source.Length ? source : source.Substring(source.Length - tailLength);
        }

        public static string GetFileImage(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            var lastPoint = str.LastIndexOf('.');
            if (lastPoint < 0)
                lastPoint = 0;
            var fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
            return fileName + str.Substring(lastPoint);
        }

        public static DateTime StringToDateTime(string str, string format = "dd MMM yyyy HH:mm", string cultureInfo = "en-US")
        {
            try
            {
                return DateTime.ParseExact(str, format,
                    null);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public static double TimeZoneToOffset(string timeZone)
        {
            var split = timeZone.Split(':');
            var hour = double.Parse(split[0]);
            var min = double.Parse(split[1]);
            return hour + min / 60;
        }

        public static int TimeZoneToHours(string timeZone)
        {
            var split = timeZone.Split(':');
            var hour = int.Parse(split[0]);
            return hour;
        }

        public static long ToUTCTimeSpan(DateTime date, string timeZone)
        {
            date = date.AddHours((0 - TimeZoneToHours(timeZone)));
            var timeSpan = (date - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }

        public static DateTime FromUnixTime(long unixTime, string timeZone)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var utcDate = epoch.AddSeconds(unixTime);
            var date = utcDate.AddHours(TimeZoneToHours(timeZone));
            return date;
        }

        public static long GetMidnightCurrentDateTimeSpan(string timeZone)
        {
            var now = DateTime.Now;
            return ToUTCTimeSpan(new DateTime(now.Year, now.Month, now.Day, 0, 0, 0), timeZone);
        }

        public static long GetMidnightBeginDateTimeSpan(DateTime date, string timeZone)
        {
            return ToUTCTimeSpan(new DateTime(date.Year, date.Month, date.Day, 0, 0, 0), timeZone);
        }

        public static long GetMidnightEndDateTimeSpan(DateTime date, string timeZone)
        {
            return ToUTCTimeSpan(new DateTime(date.Year, date.Month, date.Day, 23, 59, 59), timeZone);
        }

        public static string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}