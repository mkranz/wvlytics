using System.IO;
using Newtonsoft.Json;
using RestSharp.Serializers;
using Wvlytics.Model;
using Wvlytics.Util;

namespace Wvlytics.Services
{
    public class LocalFileConfig
    {
        public static string BasePath = @"C:\gw2data";
        public static string LocalMatchFileName = "matchInfo.json";
        public static string LocalSnapshotFileName = "scores.dat";

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

        public LocalFileAppenderService(IScoreWriter scoreWriter)
        {
            _scoreWriter = scoreWriter;
        }

        public void SaveMatch(MatchHistory match)
        {
            var matchPath = LocalFileConfig.GetMatchPath(match.MatchId, match.StartTime.ToString("yy-MM-dd"));
            var path = Path.Combine(matchPath, LocalFileConfig.LocalMatchFileName);
            var contents = JsonConvert.SerializeObject(match);
            File.WriteAllText(path, contents);
        }

        public void SaveMatchSnapshot(MatchHistorySnapshot snapshot)
        {
            var matchPath = LocalFileConfig.GetMatchPath(snapshot.MatchHistoryId.ToMatchId(), snapshot.MatchHistoryId.ToMatchTimestamp());
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
    }
}