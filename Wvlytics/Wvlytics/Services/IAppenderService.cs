using Wvlytics.Model;

namespace Wvlytics.Services
{
    public interface IAppenderService
    {
        void SaveMatch(MatchHistory match);
        void SaveMatchSnapshot(MatchHistorySnapshot snapshot);
    }
}