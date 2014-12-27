using System;

namespace Wvlytics.Model
{
    public struct ScoreSnapshot
    {
        public DateTime Timestamp { get; set; }
        public Score Scores { get; set; }
    }
}