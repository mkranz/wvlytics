using System.Collections.Generic;
using Wvlytics.Model;

namespace Wvlytics.Services
{
    public class StatsService : IStatsService
    {
        private readonly IQueryService _queryService;

        public StatsService(IQueryService queryService)
        {
            _queryService = queryService;
        }

        public IEnumerable<StatSnapshot> GetStats(string matchHistoryId)
        {
            ScoreSnapshot lastApply = null;
            ScoreSnapshot previous = null;

            var stats = new List<StatSnapshot>();

            foreach (var score in _queryService.GetScoreHistory(matchHistoryId))
            {
                if (previous != null)
                {
                    var killStat = new StatSnapshot()
                    {
                        Timestamp = score.Timestamp,
                        RedPotentialPoints = score.Scores.RedPotentialPoints,
                        GreenPotentialPoints = score.Scores.GreenPotentialPoints,
                        BluePotentialPoints = score.Scores.BluePotentialPoints,
                        RedScore = score.Scores.RedScore,
                        GreenScore = score.Scores.GreenScore,
                        BlueScore = score.Scores.BlueScore,
                        RedKills = score.Scores.RedScore - previous.Scores.RedScore,
                        GreenKills = score.Scores.GreenScore - previous.Scores.GreenScore,
                        BlueKills = score.Scores.BlueScore - previous.Scores.BlueScore
                    };

                    if (PointApplyHasOccurred(previous, score, lastApply))
                    {
                        // Subtract those points gained from structures
                        killStat.RedKills -= previous.Scores.RedPotentialPoints;
                        killStat.GreenKills -= previous.Scores.GreenPotentialPoints;
                        killStat.BlueKills -= previous.Scores.BluePotentialPoints;
                        // it is only a best guess - things could have changed hands before the apply
                        // .. so just ignore negative numbers
                        if (killStat.RedKills < 0)
                            killStat.RedKills = 0;
                        if (killStat.GreenKills < 0)
                            killStat.GreenKills = 0;
                        if (killStat.BlueKills < 0)
                            killStat.BlueKills = 0;

                        killStat.PointsApplied = true;
                        lastApply = score;
                    }

                    stats.Add(killStat);
                }

                previous = score;
            }

            return stats;
        }

        private bool PointApplyHasOccurred(ScoreSnapshot previous, ScoreSnapshot current, ScoreSnapshot lastApply = null)
        {
            var redDelta = current.Scores.RedScore - previous.Scores.RedScore;
            var greenDelta = current.Scores.GreenScore - previous.Scores.GreenScore;
            var blueDelta = current.Scores.BlueScore - previous.Scores.BlueScore;
            var deltaSum = redDelta + greenDelta + blueDelta;

            var ppSum = previous.Scores.RedPotentialPoints + previous.Scores.GreenPotentialPoints +
                        previous.Scores.BluePotentialPoints;

            // Looks like since the last snapshot, many points added - consider this candidate for 'point apply
            if (deltaSum >= ppSum)
            {
                if (lastApply == null)
                    return true;

                // but if there was < 15 minutes between snapshots, perhaps this is a false alarm
                // (big battle, where kill points count?)
                if ((current.Timestamp - lastApply.Timestamp).TotalMinutes < 14)
                    return false;
                else
                    return true;
            }

            return false;
        }
    }
}