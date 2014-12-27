using System;

namespace Wvlytics.Util
{
    public static class MatchHistoryUtil
    {
        public static string MatchHistoryId(string matchId, DateTime startTime)
        {
            return string.Format("{0}_{1}", matchId, startTime.ToString("yy-MM-dd"));
        }
        
        public static string ToMatchId(this string matchHistoryId)
        {
            return matchHistoryId.Split('_')[0];
        }
        public static string ToMatchTimestamp(this string matchHistoryId)
        {
            return matchHistoryId.Split('_')[1];
        }
    }
}