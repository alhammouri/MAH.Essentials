using System;

namespace MAH.Essentials
{
    public static class TimeSpanExtentions
    {
        public static string GetTimeSpanSummary(this TimeSpan source)
        {
            return source.ToString(@"hh\:mm\:ss\:fff");
        }
    }
}