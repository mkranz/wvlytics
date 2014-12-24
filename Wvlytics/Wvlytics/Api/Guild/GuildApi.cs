using System;
using RestSharp;
using Wvlytics.Util;

namespace Wvlytics.Api.Guild
{
    public class GuildApi : BaseApi, IGuildApi
    {
        public const string GuildDetails = "v1/guild_details.json";

        public IObservable<GuildDetails> GetGuildDetails(string guildId)
        {
            var request = new RestRequest(GuildDetails);
            request.AddParameter("guild_id", guildId);
            return Client.RequestAsync<GuildDetails>(request);
        }
    }
}