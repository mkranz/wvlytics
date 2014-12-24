using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using RestSharp;
using Wvlytics.Api.WvW.Model;
using Wvlytics.Models;
using Wvlytics.Util;

namespace Wvlytics.Api.WvW
{
    public class WvWApi : BaseApi, IWvwApi
    {
        public const string Matches = "v1/wvw/matches.json";
        public const string MatchDetails = "v1/wvw/match_details.json";
        public const string Objectives = "v1/wvw/objective_names.json";
        public const string Worlds = "v2/worlds";
        
        public IObservable<World> GetWorlds()
        {
            var request = new RestRequest(Worlds);
            request.AddParameter("ids", "all");
            return Client.RequestAsync<List<World>>(request)
                .Flatten();
        }

        public IObservable<Match> GetMatches()
        {
            var request = new RestRequest(Matches);
            return Client.RequestAsync<Matches>(request)
                .SelectMany(x => x.wvw_matches);
        }

        public IObservable<MatchDetails> GetMatchDetails(string match)
        {
            var request = new RestRequest(MatchDetails);
            request.AddParameter("match_id ", match);
            return Client.RequestAsync<MatchDetails>(request);
        }

        public IObservable<Objective> GetObjectives()
        {
            var request = new RestRequest(Objectives);
            return Client.RequestAsync<List<Objective>>(request).Flatten();
        }
    }
}