using System.Collections.Generic;
using System.IO;
using Wvlytics.Model;

namespace Wvlytics.Services
{
    public interface IScoreReader
    {
        IEnumerable<ScoreSnapshot> ReadScores(Stream stream);
    }
}