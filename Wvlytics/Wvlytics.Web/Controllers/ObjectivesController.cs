using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Wvlytics.Model;
using Wvlytics.Services;

namespace Wvlytics.Web.Controllers
{
    public class ObjectivesController : ApiController
    {
        private readonly IQueryService _queryService;

        public ObjectivesController(IQueryService queryService)
        {
            _queryService = queryService;
        }

        // GET api/objectives
        public IEnumerable<ObjectiveHistory> Get(string id)
        {
            return _queryService.GetObjectiveHistory(id);
        }

        [HttpGet]
        [Route("api/Objectives/{id}/{map}")]
        public IEnumerable<ObjectiveHistory> Get(string id, string map)
        {
            return _queryService.GetObjectiveHistory(id,map);
        }

    }
}