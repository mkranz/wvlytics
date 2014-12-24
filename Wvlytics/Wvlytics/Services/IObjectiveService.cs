using Wvlytics.Api.WvW.Model;

namespace Wvlytics.Services
{
    public interface IObjectiveService
    {
        Objective GetObjective(int objectiveId);
    }
}