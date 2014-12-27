using System.Collections.Generic;
using System.Linq;
using Amazon.DynamoDBv2.DataModel;
using Wvlytics.Model;

namespace Wvlytics.Services
{
    public class DynamoQueryService : IQueryService
    {
        private readonly DynamoDBContext _context;

        public DynamoQueryService(DynamoDBContext context)
        {
            _context = context;
        }
        public IEnumerable<MatchHistory> GetMatchHistory()
        {
            var matches = _context.Scan<MatchHistory>().ToList();
            return matches;
        }

        public IEnumerable<ScoreSnapshot> GetScoreHistory(string matchHistoryId)
        {
            var snapshots = _context.Query<MatchHistorySnapshot>(matchHistoryId, new DynamoDBOperationConfig()
            {
                ConsistentRead = false
            })
                .Select(x => new ScoreSnapshot()
                {
                    Timestamp = x.Timestamp,
                    Scores = x.Scores
                })
                .Take(5000)
                .ToList();
            return snapshots;
        }
    }
}