using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Wvlytics.Api.WvW;
using Wvlytics.Api.WvW.Model;

namespace Wvlytics.Services
{
    public class ObjectiveService : IObjectiveService
    {
        private readonly IWvwApi _wvwApi;
        protected Lazy<Dictionary<int, Objective>> Objectives;

        public ObjectiveService(IWvwApi wvwApi)
        {
            _wvwApi = wvwApi;
            Objectives = new Lazy<Dictionary<int, Objective>>(GetObjectives);
        }

        protected Dictionary<int, Objective> GetObjectives()
        {
            return _wvwApi.GetObjectives().ToEnumerable().ToDictionary(x => x.id);
        }

        public Objective GetObjective(int objectiveId)
        {
            return Objectives.Value[objectiveId];
        }

        public IEnumerable<Objective> GetAllObjectives()
        {
            return Objectives.Value.Values;
        }
    }
}