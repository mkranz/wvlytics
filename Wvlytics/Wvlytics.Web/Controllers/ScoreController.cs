using System.Collections.Generic;
using System.Web.Http;
using Wvlytics.Services;

namespace Wvlytics.Web.Controllers
{
    public class ScoreController : ApiController
    {
        private readonly IStatsService _statsService;

        public ScoreController(IStatsService statsService)
        {
            _statsService = statsService;
        }

        // GET api/killstats
        public IEnumerable<Model.StatSnapshot> Get(string id)
        {
            return _statsService.GetStats(id);
        }
    }
}