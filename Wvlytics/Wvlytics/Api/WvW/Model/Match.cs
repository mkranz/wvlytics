using System;

namespace Wvlytics.Models
{
    public class Match
    {
        public string wvw_match_id { get; set; }
        public int red_world_id { get; set; }
        public int blue_world_id { get; set; }
        public int green_world_id { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }

        public override string ToString()
        {
            return string.Format("wvw_match_id: {0}, red_world_id: {1}, blue_world_id: {2}, green_world_id: {3}, start_time: {4}, end_time: {5}", wvw_match_id, red_world_id, blue_world_id, green_world_id, start_time, end_time);
        }

        public bool HasParticipant(World world)
        {
            return HasParticipant(world.id);
        }

        private bool HasParticipant(int worldId)
        {
            return red_world_id == worldId || blue_world_id == worldId || green_world_id == worldId;
        }
    }
}