using System;
using System.Collections.Generic;
using System.Linq;

namespace Wvlytics.Model
{
    public class ObjectiveHistory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ObjectiveOwner> Owners { get; set; }

        public double GetLength()
        {
            return (Owners.Last().Timestamp - Owners.First().Timestamp).TotalSeconds;
        }

        public IEnumerable<ObjectiveBar> GetObjectiveBars()
        {
            var totalLength = GetLength();
            if (Owners.Count == 0)
            {
                yield return new ObjectiveBar()
                {
                    Owner = Owners.First().OwnerColor,
                    Percent = 100.0
                };
                yield break;
            }
            ObjectiveOwner previous = null;
            foreach (var owner in Owners)
            {
                if (previous == null)
                {
                    previous = owner;
                    continue;
                }
                var lengthDiff = (owner.Timestamp - previous.Timestamp).TotalSeconds;
                yield return new ObjectiveBar()
                {
                    Owner = previous.OwnerColor,
                    Percent = lengthDiff/totalLength
                };
                previous = owner;
            }
        } 
    }
    public class ObjectiveOwner
    {
        public DateTime Timestamp { get; set; }
        public string OwnerColor { get; set; }
    }

    public class ObjectiveBar
    {
        public double Percent { get; set; }
        public string Owner { get; set; }
    }
}