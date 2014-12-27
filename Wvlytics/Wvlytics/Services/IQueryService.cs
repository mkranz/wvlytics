using System.Collections.Generic;
using Wvlytics.Model;

namespace Wvlytics.Services
{
    public interface IQueryService
    {
        IEnumerable<MatchHistory> GetMatchHistory();
        IEnumerable<ScoreSnapshot> GetScoreHistory(string matchHistoryId);
    }
}