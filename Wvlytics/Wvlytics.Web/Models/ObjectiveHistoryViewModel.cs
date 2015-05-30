using System;
using System.Collections.Generic;
using Wvlytics.Model;

namespace Wvlytics.Web
{
	public class ObjectiveHistoryViewModel
	{
		public MatchHistory Match {get;set;}
		public IEnumerable<ObjectiveHistory> Objectives {get;set;}

		public IEnumerable<TimeTick> GetTimes() {
			var now = DateTime.UtcNow;
			var spanMinutes = (now - Match.StartTime).TotalMinutes;
			var deltaMinutes = 60*6; // 3 hours
			var deltaPercent = ((double)deltaMinutes) / ((double)spanMinutes);
			var time = Match.StartTime;
			var times = new List<TimeTick> ();
			var totalPercent = 0.0;
			while (time < now) 
			{
				if (totalPercent + deltaPercent >= 1.0)
					break;
				times.Add (new TimeTick () {
					Percent = deltaPercent,
					DateTime = time
				});

				time = time.AddMinutes (deltaMinutes);
				totalPercent += deltaPercent;
			}
			return times;
		}
	}
	public class TimeTick
	{
		public DateTime DateTime { get; set; }
		public double Percent { get; set; }
	}
}

