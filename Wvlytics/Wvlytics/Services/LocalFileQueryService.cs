using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Wvlytics.Model;
using Wvlytics.Util;

namespace Wvlytics.Services
{
    public class LocalFileQueryService : IQueryService
    {
        private readonly IScoreReader _scoreReader;
        
        public LocalFileQueryService(IScoreReader scoreReader)
        {
            _scoreReader = scoreReader;
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
            var matchPath = LocalFileConfig.GetMatchPath(matchHistoryId.ToMatchId(), matchHistoryId.ToMatchTimestamp());
            var path = Path.Combine(matchPath, LocalFileConfig.LocalSnapshotFileName);

            using (var stream = new FileStream(path,FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return _scoreReader.ReadScores(stream).ToList();
            }
        }
    }
}