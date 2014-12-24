using Wvlytics.Models;

namespace Wvlytics.Services
{
    public interface IWorldService
    {
        World GetWorld(int worldId);
        string GetWorldName(int worldId);
    }
}