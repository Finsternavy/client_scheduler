using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client_scheduler.Util
{
    public class TimeZoneHelper
    {
        private static readonly TimeZoneInfo EasternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

        public static DateTime ConvertToEastern(DateTime localTime)
        {
            DateTime unspecifiedTime = DateTime.SpecifyKind(localTime, DateTimeKind.Unspecified);
            return TimeZoneInfo.ConvertTime(unspecifiedTime, TimeZoneInfo.Local, EasternTimeZone);
        }

        public static DateTime ConvertFromEastern(DateTime easternTime)
        {
            DateTime unspecifiedTime = DateTime.SpecifyKind(easternTime, DateTimeKind.Unspecified);
            return TimeZoneInfo.ConvertTime(unspecifiedTime, EasternTimeZone, TimeZoneInfo.Local);
        }

        public static DateTime GetCurrentEasternTime()
        {
            return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, EasternTimeZone);
        }
    }
}
