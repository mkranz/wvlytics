using System.Collections.Generic;
using Wvlytics.Model;

namespace Wvlytics.Services
{
    public interface IStatsService
    {
        IEnumerable<StatSnapshot> GetStats(string matchHistoryId);
    }
}