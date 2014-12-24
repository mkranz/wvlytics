using System.Collections.Generic;

namespace Wvlytics.Model
{
    public class MapHistorySnapshot
    {
        public Score Scores { get; set; }
        public List<ObjectiveSnapshot> Objectives { get; set; }
        public List<BonusSnapshot> Bonuses { get; set; }
    }
}