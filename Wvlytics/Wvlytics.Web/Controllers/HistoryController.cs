using System.Web.Mvc;
using Wvlytics.Services;

namespace Wvlytics.Web.Controllers
{
    public class HistoryController : Controller
    {
        private readonly IQueryService _queryService;

        public HistoryController(IQueryService queryService)
        {
            _queryService = queryService;
        }

        public ActionResult Index()
        {
            return View(_queryService.GetMatchHistory());
        }

        public ActionResult Objectives(string id)
        {
            return View(_queryService.GetObjectiveHistory(id, "RedHome"));
        }

    }
}
