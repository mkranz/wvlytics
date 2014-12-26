using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Autofac;

namespace Wvlytics.Config
{
    public class DynamoModule : Module
    {
        public bool Local { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            if (Local)
                SetupDynamoLocal(builder);
            else
                SetupDynamo(builder);
        }

        private static void SetupDynamo(ContainerBuilder builder)
        {
            var client = new AmazonDynamoDBClient();
            builder.RegisterInstance(client).AsSelf().AsImplementedInterfaces();
            builder.RegisterType<DynamoDBContext>().UsingConstructor(typeof(IAmazonDynamoDB));
        }

        private static void SetupDynamoLocal(ContainerBuilder builder)
        {
            var client = new AmazonDynamoDBClient(LocalPersistenceConfig.DynamoConfig);
            builder.RegisterInstance(client).AsSelf().AsImplementedInterfaces();
            builder.RegisterType<DynamoDBContext>().UsingConstructor(typeof(IAmazonDynamoDB));
        }
    }
}