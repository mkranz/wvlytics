using System.Collections.Generic;
using Wvlytics.Model;

namespace Wvlytics.Services
{
    public interface IQueryService
    {
        IEnumerable<MatchHistory> GetMatchHistory();
        IEnumerable<ScoreSnapshot> GetScoreHistory(string matchHistoryId);
        IEnumerable<ObjectiveHistory> GetObjectiveHistory(string matchHistoryId, IEnumerable<int> objectives = null);
        IEnumerable<ObjectiveHistory> GetObjectiveHistory(string matchHistoryId, string map);
    }
}