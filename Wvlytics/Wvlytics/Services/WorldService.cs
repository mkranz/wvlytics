using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Wvlytics.Api.WvW;
using Wvlytics.Models;

namespace Wvlytics.Services
{
    public class WorldService : IWorldService
    {
        private readonly IWvwApi _wvwApi;
        protected static Lazy<Dictionary<int, World>> Worlds;

        public WorldService(IWvwApi wvwApi)
        {
            _wvwApi = wvwApi;
            Worlds = new Lazy<Dictionary<int, World>>(GetWorlds);
        }

        protected Dictionary<int, World> GetWorlds()
        {
            return _wvwApi.GetWorlds().ToEnumerable().ToDictionary(x => x.id);
        }

        public World GetWorld(int worldId)
        {
            return Worlds.Value[worldId];
        }

        public string GetWorldName(int worldId)
        {
            return GetWorld(worldId).name;
        }
    }
}