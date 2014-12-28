using System;

namespace Wvlytics.Model
{
    public class StatSnapshot
    {
        public DateTime Timestamp { get; set; }
        public int RedScore { get; set; }
        public int GreenScore { get; set; }
        public int BlueScore { get; set; }

        public int RedPotentialPoints { get; set; }
        public int GreenPotentialPoints { get; set; }
        public int BluePotentialPoints { get; set; }

        public bool PointsApplied { get; set; }
        public int RedKills { get; set; }
        public int GreenKills { get; set; }
        public int BlueKills { get; set; }
    }
}