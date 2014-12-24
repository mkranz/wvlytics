using System;
using Wvlytics.Api.WvW.Model;
using Wvlytics.Models;

namespace Wvlytics.Api.WvW
{
    public interface IWvwApi
    {
        IObservable<World> GetWorlds();
        IObservable<Match> GetMatches();
        IObservable<MatchDetails> GetMatchDetails(string match);
        IObservable<Objective> GetObjectives();
    }
}