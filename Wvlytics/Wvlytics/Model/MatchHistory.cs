using System;
using Amazon.DynamoDBv2.DataModel;

namespace Wvlytics.Model
{
    [DynamoDBTable("MatchHistory")]
    public class MatchHistory
    {
        // use MatchId + StartTime
        [DynamoDBHashKey]
        public string MatchHistoryId { get; set; }

        public string MatchId { get; set; }

        public int RedWorldId { get; set; }
        public int GreenWorldId { get; set; }
        public int BlueWorldId { get; set; }

        public string RedWorldName { get; set; }
        public string GreenWorldName { get; set; }
        public string BlueWorldName { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int? GetWorldId(string ownerColor)
        {
            switch (ownerColor)
            {
                case "Red":
                    return RedWorldId;
                case "Green":
                    return GreenWorldId;
                case "Blue":
                    return BlueWorldId;
                case "Neutral":
                    return null;
                default:
                    throw new ArgumentException(ownerColor);
            }
        }
        public bool HasParticipant(string world)
        {
            return RedWorldName == world || GreenWorldName == world || BlueWorldName == world;
        }

        public bool HasParticipant(int worldId)
        {
            return RedWorldId == worldId || GreenWorldId == worldId || BlueWorldId == worldId;
        }
    }
}