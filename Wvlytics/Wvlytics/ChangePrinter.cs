using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using RestSharp.Serializers;
using Wvlytics.Api.WvW;
using Wvlytics.Models;

namespace Wvlytics
{
    public static class ChangePrinter
    {
        private static void ReadLoop(WvWApi api)
        {
            var matches = GetMatches(api);
            var details = GetMatchDetails(api, matches);
            foreach (var match in matches)
            {
                WriteState(match, details[match.wvw_match_id]);
            }

            while (true)
            {
                var updatedMatches = GetMatches(api);
                var updatedDetails = GetMatchDetails(api, matches);

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(DateTime.UtcNow.ToString("yyyy-MM-dd HHmmss"));
                foreach (var match in updatedMatches)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" ");
                    Console.Write(match.wvw_match_id);
                    Console.Write(":");
                    if (IsNewMatch(matches, match))
                    {
                        WriteState(match, updatedDetails[match.wvw_match_id]);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("N");
                    }
                    else if (MatchStateChanged(details, updatedDetails, match))
                    {
                        WriteState(match, updatedDetails[match.wvw_match_id]);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("C");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(".");
                    }
                    
                }

                Console.WriteLine();

                matches = updatedMatches;
                details = updatedDetails;
                Thread.Sleep(5000);
            }
        }

        private static void WriteState(Match match, MatchDetails updatedDetail)
        {
            var contents = new JsonSerializer().Serialize(updatedDetail);
            var time = DateTime.UtcNow;
            // Matches are labelled x-y, where x is region (1 or 2, US/EUR), and y is the match numbre in the region
            var path = GetPath(match.wvw_match_id, match.start_time, time);
            File.WriteAllText(path,contents);
        }

        private static string GetPath(string wvwMatchId, DateTime startTime, DateTime time)
        {
            const string basePath = @"C:\gw2data";
            var zone = wvwMatchId.Split('-')[0];
            var match = wvwMatchId.Split('-')[1];
            var directory = Path.Combine(basePath, zone, startTime.ToString("yyyy-MM-dd"), match);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            return Path.Combine(directory, time.ToString("yyyy-MM-dd HHmmss") + ".json");
        }

        private static bool MatchStateChanged(Dictionary<string, MatchDetails> previousMatchDetails, Dictionary<string, MatchDetails> updatedDetails, Match match)
        {
            var previousMatch = previousMatchDetails[match.wvw_match_id];
            var updatedMatch = updatedDetails[match.wvw_match_id];
            return MatchStateChanged(previousMatch, updatedMatch);
        }

        private static bool MatchStateChanged(MatchDetails previousMatchDetails, MatchDetails updatedDetails)
        {
            for (var i = 0; i < previousMatchDetails.scores.Count; i++)
            {
                if (previousMatchDetails.scores[i] != updatedDetails.scores[i])
                    return true;
            }

            for (var i = 0; i < previousMatchDetails.maps.Count; i++)
            {
                if (HasMapChanges(previousMatchDetails.maps[i],updatedDetails.maps[i]))
                    return true;
            }
            return false;
        }

        private static bool HasMapChanges(MapState previousMapState, MapState updatedMapState)
        {
            for (var i = 0; i < previousMapState.scores.Count; i++)
            {
                if (previousMapState.scores[i] != updatedMapState.scores[i])
                    return true;
            }
            for (var i = 0; i < previousMapState.objectives.Count; i++)
            {
                if (HasObjectiveChanges(previousMapState.objectives[i], updatedMapState.objectives[i]))
                    return true;
            }
            if (previousMapState.bonuses.Count != updatedMapState.bonuses.Count)
                return true;
            for (var i = 0; i < previousMapState.bonuses.Count; i++)
            {
                if (HasBonusChanges(previousMapState.bonuses[i], updatedMapState.bonuses[i]))
                    return true;
            }
            return false;
        }

        private static bool HasBonusChanges(BonusState previousBonusState, BonusState updatedBonusState)
        {
            if (previousBonusState.type != updatedBonusState.type)
                return true;
            if (previousBonusState.owner != updatedBonusState.owner)
                return true;
            return false;
        }

        private static bool HasObjectiveChanges(ObjectiveState previousObjectiveState, ObjectiveState updatedObjectiveState)
        {
            if (previousObjectiveState.id != updatedObjectiveState.id)
                throw new ArgumentException("Objective state mismatch, old id "+previousObjectiveState.id+" new id "+updatedObjectiveState.id);
            if (previousObjectiveState.owner != updatedObjectiveState.owner)
                return true;
            if (previousObjectiveState.owner_guild != updatedObjectiveState.owner_guild)
                return true;

            return false;
        }


        private static bool IsNewMatch(List<Match> previousMatches, Match match)
        {
            var previousMatch = previousMatches.FirstOrDefault(x => x.wvw_match_id == match.wvw_match_id);
            if (previousMatch == null)
                return true;
            if (previousMatch.start_time != match.start_time)
                return true;
            return false;
        }

        private static List<Match> GetMatches(WvWApi api)
        {
            return api.GetMatches().ToEnumerable().ToList();
        }

        private static Dictionary<string, MatchDetails> GetMatchDetails(WvWApi api, List<Match> matches)
        {
            return matches.Select(match => api.GetMatchDetails(match.wvw_match_id).ToEnumerable().Single()).ToDictionary(match => match.match_id);
        }

    }
}
