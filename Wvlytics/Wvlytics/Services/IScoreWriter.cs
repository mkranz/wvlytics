using System.IO;
using Wvlytics.Model;

namespace Wvlytics.Services
{
    public interface IScoreWriter
    {
        void WriteScore(Stream stream, ScoreSnapshot score);
    }
}