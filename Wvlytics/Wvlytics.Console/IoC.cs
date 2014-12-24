using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Autofac;

namespace Wvlytics.Console
{
    public class IoC
    {
        public static IContainer Init()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Wvlytics.Assembly.Reference)
               .Where(t => t.Name.EndsWith("Service"))
               .SingleInstance()
               .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Wvlytics.Assembly.Reference)
               .Where(t => t.Name.EndsWith("Api"))
               .SingleInstance()
               .AsImplementedInterfaces();

            SetupDynamo(builder);

            return builder.Build();
        }

        private static void SetupDynamo(ContainerBuilder builder)
        {
            var client = new AmazonDynamoDBClient();
            var tables = client.ListTables();
            builder.RegisterInstance(client).AsSelf().AsImplementedInterfaces();
            builder.RegisterType<DynamoDBContext>().UsingConstructor(typeof(IAmazonDynamoDB));
        }

        private static void SetupDynamoLocal(ContainerBuilder builder)
        {
            var client = new AmazonDynamoDBClient(LocalPersistenceConfig.DynamoConfig);
            var tables = client.ListTables();
            builder.RegisterInstance(client).AsSelf().AsImplementedInterfaces();
            builder.RegisterType<DynamoDBContext>().UsingConstructor(typeof(IAmazonDynamoDB));
        }
    }

    public class LocalPersistenceConfig
    {
        public static readonly AmazonDynamoDBConfig DynamoConfig = new AmazonDynamoDBConfig()
        {
            ServiceURL = "http://localhost:9085/",
        };
    }
}