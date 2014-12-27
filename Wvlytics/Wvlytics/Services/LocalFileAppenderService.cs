using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Wvlytics.Model;
using Wvlytics.Util;

namespace Wvlytics.Services
{
    public class LocalFileConfig
    {
        public static string BasePath = @"C:\gw2data";
        public static string LocalMatchFileName = "matchInfo.json";
        public static string LocalSnapshotFileName = "scores.dat";
        public static string LocalObjectivesFileName = "objectives.dat";

        public static string GetMatchPath(string wvwMatchHistoryId)
        {
            return GetMatchPath(wvwMatchHistoryId.ToMatchId(), wvwMatchHistoryId.ToMatchTimestamp());
        }

        public static string GetMatchPath(string wvwMatchId, string startTime)
        {
            var zone = wvwMatchId.Split('-')[0];
            var match = wvwMatchId.Split('-')[1];
            var directory = Path.Combine(BasePath, zone, startTime, match);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            return directory;
        }
    }

    public class LocalFileAppenderService : IAppenderService
    {
        private readonly IScoreWriter _scoreWriter;
        private readonly IObjectiveSnapshotWriter _objectiveSnapshotWriter;

        public LocalFileAppenderService(IScoreWriter scoreWriter, IObjectiveSnapshotWriter objectiveSnapshotWriter)
        {
            _scoreWriter = scoreWriter;
            _objectiveSnapshotWriter = objectiveSnapshotWriter;
        }

        public void SaveMatch(MatchHistory match)
        {
            var matchPath = LocalFileConfig.GetMatchPath(match.MatchHistoryId);
            var path = Path.Combine(matchPath, LocalFileConfig.LocalMatchFileName);
            var contents = JsonConvert.SerializeObject(match);
            File.WriteAllText(path, contents);
        }

        public void SaveMatchSnapshot(MatchHistorySnapshot snapshot)
        {
            var matchPath = LocalFileConfig.GetMatchPath(snapshot.MatchHistoryId);
            WriteScores(snapshot, matchPath);
            WriteObjectives(snapshot, matchPath);
        }

        private void WriteScores(MatchHistorySnapshot snapshot, string matchPath)
        {
            var path = Path.Combine(matchPath, LocalFileConfig.LocalSnapshotFileName);

            using (var stream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Read))
            {
                _scoreWriter.WriteScore(stream, new ScoreSnapshot()
                {
                    Timestamp = snapshot.Timestamp,
                    Scores = snapshot.Scores
                });
            }
        }
        private void WriteObjectives(MatchHistorySnapshot snapshot, string matchPath)
        {
            var path = Path.Combine(matchPath, LocalFileConfig.LocalObjectivesFileName);

            using (var stream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Read))
            {
                _objectiveSnapshotWriter.WriteSnapshots(stream, 
                    snapshot.Timestamp,
                    snapshot.RedHome.Objectives
                        .Concat(snapshot.GreenHome.Objectives)
                        .Concat(snapshot.BlueHome.Objectives)
                        .Concat(snapshot.Center.Objectives)
                        );
            }
        }
    }
}