using System;
namespace LazyFramework
{
    public static class TimeUtils
    {
        public const int SecPerHour = 3600;
        public const int SecPerMinute = 60;
        public static string SecToString(int sec)
        {
            int _sec = sec;

            //convert to hour
            int hour = _sec/SecPerHour;
            _sec-=hour*SecPerHour;
            string hourStr = (hour<10 ? "0" : "")+hour.ToString();

            //convert remaining time to min
            int min = _sec/SecPerMinute;
            _sec-=min*SecPerMinute;
            string minStr = (min<10 ? "0" : "")+min.ToString();

            //sec string
            string secStr = (_sec<10 ? "0" : "")+_sec.ToString();

            return (hour<=0 ? "" : hourStr+":")+(min<=0 ? "" : minStr+":")+secStr+"s";
        }
        public static DateTime GetCurrentDay()
        {
            return StringToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
        }
        public static string GetCurrentDayInString()
        {
            return DateTime.Now.ToString("dd/MM/yyyy");
        }
        public static DateTime StringToDateTime(string timeString)
        {
            return DateTime.ParseExact(timeString , "dd/MM/yyyy" , null);
        }


        public static int CalculateDayPast(DateTime start , DateTime end)
        {
            TimeSpan span = end.Subtract(start);
            return span.Days;
        }

        public static int CalculateDayPastFromLastLogin()
        {
            DateTime today = GetCurrentDay();
            DateTime lastLoginDay = StringToDateTime(PlayerService.LastLoginDay);
            return CalculateDayPast(lastLoginDay , today);
        }
        public static void SetLoginDayAsToday()
        {
            PlayerService.LastLoginDay=GetCurrentDayInString();
        }
    }
}

