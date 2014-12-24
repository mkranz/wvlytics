using System.Collections.Generic;

namespace Wvlytics.Models
{
    public class GuildEmblem
    {
        // Need image API
        public string background_id { get; set; }
        public string foreground_id { get; set; }

        public List<string> flags { get; set; }

        // Need colour API
        public string background_color_id { get; set; }
        public string foreground_primary_color_id { get; set; }
        public string foreground_secondary_color_id { get; set; }
    }
}
