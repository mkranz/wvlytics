using Wvlytics.Model;

namespace Wvlytics.Services
{
    public interface IMatchSnapshotService
    {
        MatchHistorySnapshot GetSnapshotFor(MatchHistory match);
    }
}