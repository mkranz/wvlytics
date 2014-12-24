using System;
using System.Diagnostics;
using System.Linq;
using System.Management.Instrumentation;
using System.Threading.Tasks;
using Wvlytics.Model;

namespace Wvlytics.Services
{
    public class SnapshottingService : ISnapshottingService
    {
        private readonly IMatchService _matchService;
        private readonly IMatchSnapshotService _snapshotService;
        private readonly IAppenderService _appenderService;
        private readonly IPrinterService _printerService;

        public SnapshottingService(IMatchService matchService, IMatchSnapshotService snapshotService, IAppenderService appenderService, IPrinterService printerService)
        {
            _matchService = matchService;
            _snapshotService = snapshotService;
            _appenderService = appenderService;
            _printerService = printerService;
        }

        public void SnapshotAll()
        {
            var matches = _matchService.GetCurrentMatches();
            Console.WriteLine("Snapshotting {0} matches",matches.Count());
            Parallel.ForEach(matches, Snapshot);
            //foreach (var match in matches)
            //{
            //    Snapshot(match);
            //}
        }

        private void Snapshot(MatchHistory match)
        {
            var sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("{0}: Snapshotting",match.MatchHistoryId);
            var snapshot =_snapshotService.GetSnapshotFor(match);

            _printerService.PrintScores(match, snapshot);
            Console.WriteLine("{0}: Saving", match.MatchHistoryId);
            _appenderService.SaveMatch(match);
            _appenderService.SaveMatchSnapshot(snapshot);
            Console.WriteLine("{0}: Complete, Elapsed {1}ms", match.MatchHistoryId,sw.ElapsedMilliseconds);
            
        }
    }
}