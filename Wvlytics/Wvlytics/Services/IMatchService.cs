using System.Collections.Generic;
using Wvlytics.Model;

namespace Wvlytics.Services
{
    public interface IMatchService
    {
        IEnumerable<MatchHistory> GetCurrentMatches();
    }
}