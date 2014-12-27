using Autofac;
using Wvlytics.Config;
using Wvlytics.Services;

namespace Wvlytics.Console
{
    internal class IoC
    {
        public static IContainer Init()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<WvlyticsCoreModule>();
            
            //builder.RegisterModule<DynamoModule>();
            builder.RegisterModule<LocalFileModule>();
            
            return builder.Build();
        }
    }
}