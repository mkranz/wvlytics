using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Wvlytics.Api.WvW;
using Wvlytics.Model;
using Wvlytics.Models;

namespace Wvlytics.Services
{
    public class MatchSnapshotService : IMatchSnapshotService
    {
        private readonly IWvwApi _wvwApi;
        private readonly IObjectiveService _objectiveService;

        public MatchSnapshotService(IWvwApi wvwApi, IObjectiveService objectiveService)
        {
            _wvwApi = wvwApi;
            _objectiveService = objectiveService;
        }

        public MatchHistorySnapshot GetSnapshotFor(MatchHistory match)
        {
            var details = _wvwApi.GetMatchDetails(match.MatchId).ToEnumerable().Single();
            var timestamp = DateTime.UtcNow;
            var snapshot = new MatchHistorySnapshot()
            {
                MatchHistoryId = match.MatchHistoryId,
                Timestamp = timestamp,
                RedHome = MapSnapshot(match,details.maps, "RedHome"),
                GreenHome = MapSnapshot(match, details.maps, "GreenHome"),
                BlueHome = MapSnapshot(match, details.maps, "BlueHome"),
                Center = MapSnapshot(match, details.maps, "Center"),
                Scores = new Score()
                {
                    RedScore = details.scores[0],
                    GreenScore = details.scores[1],
                    BlueScore = details.scores[2],
                }
            };
            ScoreSum(snapshot);
            return snapshot;
        }

        private MapHistorySnapshot MapSnapshot(MatchHistory match, IEnumerable<MapState> maps, string name)
        {
            var map = maps.Single(x => x.type == name);
            var mapSnapshot = new MapHistorySnapshot()
            {
                Objectives = map.objectives.Select(x => new ObjectiveSnapshot()
                {
                    Id = x.id,
                    OwnerColor = x.owner,
                    OwnerWorldId = match.GetWorldId(x.owner),
                    OwnerGuildId = x.owner_guild
                }).ToList(),
                Bonuses = map.bonuses.Select(x => new BonusSnapshot()
                {
                    Type = x.type,
                    OwnerColor = x.owner,
                    OwnerWorldId = match.GetWorldId(x.owner),
                }).ToList(),
                Scores = new Score()
                {
                    RedScore = map.scores[0],
                    GreenScore = map.scores[1],
                    BlueScore = map.scores[2],
                }
            };
            CalculateScoresFor(mapSnapshot);

            return mapSnapshot;
        }

        private void CalculateScoresFor(MapHistorySnapshot mapSnapshot)
        {

            mapSnapshot.Scores.RedPotentialPoints = PotentialPointsFor(mapSnapshot,"Red");
            mapSnapshot.Scores.GreenPotentialPoints = PotentialPointsFor(mapSnapshot, "Green");
            mapSnapshot.Scores.BluePotentialPoints = PotentialPointsFor(mapSnapshot, "Blue");
        }

        private int PotentialPointsFor(MapHistorySnapshot mapSnapshot, string color)
        {
            return mapSnapshot.Objectives.Where(x => x.OwnerColor == color)
                .Sum(x => _objectiveService.GetObjective(x.Id).GetPotentialPointsValue());
        }

        private void ScoreSum(MatchHistorySnapshot snapshot)
        {
            snapshot.Scores.RedPotentialPoints = snapshot.RedHome.Scores.RedPotentialPoints +
                                                 snapshot.GreenHome.Scores.RedPotentialPoints +
                                                 snapshot.BlueHome.Scores.RedPotentialPoints +
                                                 snapshot.Center.Scores.RedPotentialPoints;
            snapshot.Scores.GreenPotentialPoints = snapshot.RedHome.Scores.GreenPotentialPoints +
                                                   snapshot.GreenHome.Scores.GreenPotentialPoints +
                                                   snapshot.BlueHome.Scores.GreenPotentialPoints +
                                                   snapshot.Center.Scores.GreenPotentialPoints;
            snapshot.Scores.BluePotentialPoints = snapshot.RedHome.Scores.BluePotentialPoints +
                                                  snapshot.GreenHome.Scores.BluePotentialPoints +
                                                  snapshot.BlueHome.Scores.BluePotentialPoints +
                                                  snapshot.Center.Scores.BluePotentialPoints;
        }

    }
}