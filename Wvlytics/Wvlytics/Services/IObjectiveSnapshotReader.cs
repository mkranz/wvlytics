using System.Collections.Generic;
using System.IO;
using Wvlytics.Model;

namespace Wvlytics.Services
{
    public interface IObjectiveSnapshotReader
    {
        IEnumerable<ObjectiveHistory> ReadSnapshots(Stream stream, IEnumerable<int> objectiveIds);
    }
}