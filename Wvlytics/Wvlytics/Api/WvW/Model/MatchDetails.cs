using System.Collections.Generic;

namespace Wvlytics.Models
{
    public class MatchDetails
    {
        public string match_id { get; set; }
        // red, green, blue
        public List<int> scores { get; set; }
        public List<MapState> maps { get; set; } 
    }
}