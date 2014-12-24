using Wvlytics.Model;

namespace Wvlytics.Services
{
    public interface IPrinterService
    {
        void PrintScores(MatchHistory match, MatchHistorySnapshot snapshot);
    }
}