using System;

namespace Wvlytics.Api.Guild
{
    public interface IGuildApi
    {
        IObservable<GuildDetails> GetGuildDetails(string guildId);
    }
}