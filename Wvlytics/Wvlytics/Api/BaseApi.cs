using RestSharp;

namespace Wvlytics.Api
{
    public class BaseApi
    {
        public const string Base = "https://api.guildwars2.com/";
        protected readonly RestClient Client;

        public BaseApi()
        {
            Client = new RestClient(Base);
        }
    }
}