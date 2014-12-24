using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Wvlytics.Api.WvW;
using Wvlytics.Model;

namespace Wvlytics.Services
{
    public class MatchService : IMatchService
    {
        private readonly IWvwApi _wvwApi;
        private readonly IWorldService _worldService;

        public MatchService(IWvwApi wvwApi, IWorldService worldService)
        {
            _wvwApi = wvwApi;
            _worldService = worldService;
        }

        public IEnumerable<MatchHistory> GetCurrentMatches()
        {
            return
                _wvwApi.GetMatches().ToEnumerable()
                    .Select(x => new MatchHistory()
                    {
                        MatchHistoryId = Util.MatchHistoryUtil.MatchHistoryId(x.wvw_match_id,x.start_time),
                        MatchId = x.wvw_match_id,
                        RedWorldId = x.red_world_id,
                        GreenWorldId = x.green_world_id,
                        BlueWorldId = x.blue_world_id,
                        RedWorldName = _worldService.GetWorldName(x.red_world_id),
                        GreenWorldName = _worldService.GetWorldName(x.green_world_id),
                        BlueWorldName = _worldService.GetWorldName(x.blue_world_id),
                        StartTime = x.start_time,
                        EndTime = x.end_time
                    }).ToList();
        }
    }
}