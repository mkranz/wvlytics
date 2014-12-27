using System;
using System.Collections.Generic;
using System.IO;
using Wvlytics.Model;

namespace Wvlytics.Services
{
    public class BinaryScoreReader : IScoreReader
    {
        public IEnumerable<ScoreSnapshot> ReadScores(Stream stream)
        {
            while (stream.Position < stream.Length)
            {
                yield return new ScoreSnapshot()
                {
                    Timestamp = DateTime.FromBinary(ReadLong(stream)),
                    Scores = new Score()
                    {
                        RedPotentialPoints = ReadInt(stream),
                        BluePotentialPoints = ReadInt(stream),
                        GreenPotentialPoints = ReadInt(stream),

                        RedScore = ReadInt(stream),
                        BlueScore = ReadInt(stream),
                        GreenScore = ReadInt(stream),
                    }
                };
            }
        }

        protected long ReadLong(Stream stream)
        {
            var buffer = new byte[8];
            stream.Read(buffer, 0, 8);
            return BitConverter.ToInt64(buffer, 0);
        }
        protected int ReadInt(Stream stream)
        {
            var buffer = new byte[4];
            stream.Read(buffer, 0, 4);
            return BitConverter.ToInt32(buffer, 0);
        }
    }
}