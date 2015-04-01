using System.Web.Mvc;
using Wvlytics.Services;
using System.Linq;

namespace Wvlytics.Web.Controllers
{
	public class HistoryController : Controller
	{
		private readonly IQueryService _queryService;

		public HistoryController (IQueryService queryService)
		{
			_queryService = queryService;
		}

		public ActionResult Index ()
		{
			var matches = _queryService.GetMatchHistory ()
				.ToList ()
				.OrderByDescending (x => x.StartTime);

			return View (matches);
		}

		public ActionResult Match(string matchId)
		{
			var match = _queryService.GetMatchHistory ().FirstOrDefault (x => x.MatchHistoryId == matchId);
			if (match == null) {
				return RedirectToAction("Index");
			}
			return View (
				new MatchHistoryViewModel () 
				{
					Match = match
				}
			);
		}

		public ActionResult Objectives (string id, string map)
		{
			return View (_queryService.GetObjectiveHistory (id, map));
		}

	}
}
