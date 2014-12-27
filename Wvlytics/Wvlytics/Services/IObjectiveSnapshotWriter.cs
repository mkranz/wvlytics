using System;
using System.Collections.Generic;
using System.IO;
using Wvlytics.Model;

namespace Wvlytics.Services
{
    public interface IObjectiveSnapshotWriter
    {
        void WriteSnapshots(Stream stream, DateTime timestamp, IEnumerable<ObjectiveSnapshot> objectives);
    }
}