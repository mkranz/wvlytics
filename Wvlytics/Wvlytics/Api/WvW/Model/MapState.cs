using System.Collections.Generic;

namespace Wvlytics.Models
{
    public class MapState
    {
        public string type { get; set; }
        // map scores: red, green, blue
        public List<int> scores { get; set; }
        public List<ObjectiveState> objectives { get; set; }
        public List<BonusState> bonuses { get; set; }
    }
}