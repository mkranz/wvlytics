using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Wvlytics.Model;

namespace Wvlytics.Services
{
    public class BinaryObjectiveSnapshotWriter : IObjectiveSnapshotWriter
    {
        public void WriteSnapshots(Stream stream, DateTime timestamp, IEnumerable<ObjectiveSnapshot> objectives)
        {
            var redObjectives = new BitArray(80);
            var greenObjectives = new BitArray(80);
            var blueObjectives = new BitArray(80);
            foreach (var objective in objectives)
            {
                switch (objective.OwnerColor)
                {
                    case "Red":
                        redObjectives.Set(objective.Id,true);
                        break;
                    case "Green":
                        greenObjectives.Set(objective.Id, true);
                        break;
                    case "Blue":
                        blueObjectives.Set(objective.Id, true);
                        break;
                }
            }

            var dest = new Byte[240];

            redObjectives.CopyTo(dest, 0);
            greenObjectives.CopyTo(dest, 80);
            blueObjectives.CopyTo(dest, 160);

            Write(stream, timestamp.ToBinary());
            stream.Write(dest, 0, dest.Length);
        }

        protected void Write(Stream stream, long num)
        {
            var bytes = BitConverter.GetBytes(num);
            stream.Write(bytes, 0, bytes.Length);
        }
    }
}