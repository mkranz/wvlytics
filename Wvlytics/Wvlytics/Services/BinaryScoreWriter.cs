using System;
using System.IO;
using Wvlytics.Model;

namespace Wvlytics.Services
{
    public class BinaryScoreWriter : IScoreWriter
    {
        public void WriteScore(Stream stream, ScoreSnapshot score)
        {
            Write(stream, score.Timestamp.ToBinary());
            Write(stream,score.Scores.RedPotentialPoints);
            Write(stream, score.Scores.BluePotentialPoints);
            Write(stream, score.Scores.GreenPotentialPoints);
            
            Write(stream, score.Scores.RedScore);
            Write(stream, score.Scores.BlueScore);
            Write(stream, score.Scores.GreenScore);

            stream.Flush();
        }

        protected void Write(Stream stream, long num)
        {
            var bytes = BitConverter.GetBytes(num);
            stream.Write(bytes, 0, bytes.Length);
        }
        protected void Write(Stream stream, int num)
        {
            var bytes = BitConverter.GetBytes(num);
            stream.Write(bytes, 0, bytes.Length);
        }
    }
}