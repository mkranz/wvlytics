using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Wvlytics.Api.WvW.Model;
using Wvlytics.Model;

namespace Wvlytics.Services
{
    public class BinaryObjectiveSnapshotReader : IObjectiveSnapshotReader
    {
        private readonly IObjectiveService _objectiveService;

        public BinaryObjectiveSnapshotReader(IObjectiveService objectiveService)
        {
            _objectiveService = objectiveService;
        }

        public IEnumerable<ObjectiveHistory> ReadSnapshots(Stream stream, IEnumerable<int> objectiveIds)
        {
            var objectiveHistory = GetObjectives(objectiveIds).Select(x => new ObjectiveHistory()
            {
                Id = x.id,
                Name = x.name,
                Owners = new List<ObjectiveOwner>()
            }).ToList();

            while (stream.Position < stream.Length)
            {
                var timestamp = DateTime.FromBinary(ReadLong(stream));

                var redObjectives = GetBitArray(stream);
                var greenObjectives = GetBitArray(stream);
                var blueObjectives = GetBitArray(stream);

                var isLast = stream.Position == stream.Length;

                foreach (var objective in objectiveHistory)
                {
                    if (redObjectives.Get(objective.Id))
                        Add(objective, timestamp, "Red", isLast);
                    else if (greenObjectives.Get(objective.Id))
                        Add(objective, timestamp, "Green", isLast);
                    else if (blueObjectives.Get(objective.Id))
                        Add(objective, timestamp, "Blue", isLast);
                    else
                        Add(objective, timestamp, "Neutral", isLast);
                }
            }
            return objectiveHistory;

            
        }

        private static void Add(ObjectiveHistory objective, DateTime timestamp, string color, bool isLast)
        {
            // only store changes in ownership
            var previous = objective.Owners.LastOrDefault();
            if (previous == null || isLast || previous.OwnerColor != color)
            {
                objective.Owners.Add(new ObjectiveOwner()
                {
                    Timestamp = timestamp,
                    OwnerColor = color
                });
            }
        }

        private List<Objective> GetObjectives(IEnumerable<int> objectiveIds)
        {
            return objectiveIds == null
                ? _objectiveService.GetAllObjectives().ToList()
                : objectiveIds.Select(x => _objectiveService.GetObjective(x)).ToList();
        }

        private static BitArray GetBitArray(Stream stream)
        {
            var buffer = new Byte[80];
            stream.Read(buffer, 0, buffer.Length);
            return new BitArray(buffer);
        }

        protected long ReadLong(Stream stream)
        {
            var buffer = new byte[8];
            stream.Read(buffer, 0, 8);
            return BitConverter.ToInt64(buffer, 0);
        }
    }
}