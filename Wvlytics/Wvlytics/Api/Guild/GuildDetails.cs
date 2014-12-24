using Wvlytics.Models;

namespace Wvlytics.Api.Guild
{
    public class GuildDetails
    {
        public string guild_id { get; set; }
        public string guild_name { get; set; }
        public string tag { get; set; }
        public GuildEmblem emblem { get; set; }
    }
}