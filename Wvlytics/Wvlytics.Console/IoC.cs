using Autofac;
using Wvlytics.Config;

namespace Wvlytics.Console
{
    internal class IoC
    {
        public static IContainer Init()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<WvlyticsCoreModule>();
            builder.RegisterModule<DynamoModule>();
            return builder.Build();
        }
    }
}