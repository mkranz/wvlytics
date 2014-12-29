using System.Collections.Generic;
using System.IO;
using System.Linq;
using Amazon.S3.Model;
using Newtonsoft.Json;
using Wvlytics.Model;

namespace Wvlytics.Services
{
    public class LocalFileQueryService : IQueryService
    {
        private readonly IScoreReader _scoreReader;
        private readonly IObjectiveSnapshotReader _objectiveSnapshotReader;

        public LocalFileQueryService(IScoreReader scoreReader, IObjectiveSnapshotReader objectiveSnapshotReader)
        {
            _scoreReader = scoreReader;
            _objectiveSnapshotReader = objectiveSnapshotReader;
        }

        public IEnumerable<MatchHistory> GetMatchHistory()
        {
            return Directory.EnumerateFiles(LocalFileConfig.BasePath, LocalFileConfig.LocalMatchFileName,
                SearchOption.AllDirectories).Select(ReadMatchFile);
        }

        private MatchHistory ReadMatchFile(string path)
        {
            return JsonConvert.DeserializeObject<MatchHistory>(File.ReadAllText(path));
        }

        public IEnumerable<ScoreSnapshot> GetScoreHistory(string matchHistoryId)
        {
            var matchPath = LocalFileConfig.GetMatchPath(matchHistoryId);
            var path = Path.Combine(matchPath, LocalFileConfig.LocalSnapshotFileName);

            using (var stream = new FileStream(path,FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return _scoreReader.ReadScores(stream).ToList();
            }
        }

        public IEnumerable<ObjectiveHistory> GetObjectiveHistory(string matchHistoryId, IEnumerable<int> objectiveIds)
        {
            var matchPath = LocalFileConfig.GetMatchPath(matchHistoryId);
            var path = Path.Combine(matchPath, LocalFileConfig.LocalObjectivesFileName);

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return _objectiveSnapshotReader.ReadSnapshots(stream,objectiveIds).ToList();
            }
        }

        public IEnumerable<ObjectiveHistory> GetObjectiveHistory(string matchHistoryId, string map)
        {
            List<int> objectiveIds = null;
            switch (map)
            {
                case "Center":
                    objectiveIds = new List<int>()
                    {
                        1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22
                    };
                    break;
                // ignore bonuses for now
                case "RedHome":
                    objectiveIds = new List<int>()
                    {
                        32,33,34,35,36,37,38,39,40,50,51,52,53 
                    };
                    break;
                case "GreenHome":
                    objectiveIds = new List<int>()
                    {
                        41,42,43,44,45,46,47,48,49,54,55,56,57
                    };
                    break;
                case "BlueHome":
                    objectiveIds = new List<int>()
                    {
                        23,24,25,26,27,28,29,30,31,58,59,60,61
                    };
                    break;
            }
            return GetObjectiveHistory(matchHistoryId, objectiveIds);
        } 
    }
}