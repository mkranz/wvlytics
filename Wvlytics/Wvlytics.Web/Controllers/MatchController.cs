using System.Collections.Generic;
using System.Web.Http;
using Wvlytics.Model;
using Wvlytics.Services;

namespace Wvlytics.Web.Controllers
{
    public class MatchController : ApiController
    {
        private readonly IQueryService _queryService;

        public MatchController(IQueryService queryService)
        {
            _queryService = queryService;
        }

        // GET api/match
        public IEnumerable<MatchHistory> Get()
        {
            return _queryService.GetMatchHistory();
        }
    }
}
