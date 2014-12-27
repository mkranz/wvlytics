using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Wvlytics.Model;
using Wvlytics.Services;

namespace Wvlytics.Web.Controllers
{
    public class ScoreController : ApiController
    {
        private readonly IQueryService _queryService;

        public ScoreController(IQueryService queryService)
        {
            _queryService = queryService;
        }

        // GET api/score/2-2_14-12-19
        public IEnumerable<ScoreSnapshot> Get(string id)
        {
            return _queryService.GetScoreHistory(id)
                .ToList();
        }
    }
}