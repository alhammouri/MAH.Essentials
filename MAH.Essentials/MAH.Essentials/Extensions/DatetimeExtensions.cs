using System;
using System.Globalization;

namespace MAH.Essentials
{
    public static class DatetimeExtentions
    {
        public static string AsDateTimeStamp(this DateTime source)
        {
            return source.ToString("yyyyMMddHHmmssffff");
        }

        public static string AsGermanShortDate(this DateTime source)
        {
            return source.ToString("d", CultureInfo.CreateSpecificCulture("de-DE"));
        }
    }
}