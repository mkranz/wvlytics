using System;
using Amazon.DynamoDBv2.DataModel;

namespace Wvlytics.Model
{
    [DynamoDBTable("MatchHistorySnapshot")]
    public class MatchHistorySnapshot
    {
        [DynamoDBHashKey]
        public string MatchHistoryId { get; set; }
        [DynamoDBRangeKey]
        public DateTime Timestamp { get; set; }

        public Score Scores { get; set; }

        public MapHistorySnapshot RedHome { get; set; }
        public MapHistorySnapshot GreenHome { get; set; }
        public MapHistorySnapshot BlueHome { get; set; }
        public MapHistorySnapshot Center { get; set; } 
    }
}