using Amazon.DynamoDBv2;

namespace Wvlytics.Config
{
    public class LocalPersistenceConfig
    {
        public static readonly AmazonDynamoDBConfig DynamoConfig = new AmazonDynamoDBConfig()
        {
            ServiceURL = "http://localhost:9085/",
        };
    }
}