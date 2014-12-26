using Autofac;

namespace Wvlytics.Config
{
    public class WvlyticsCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Wvlytics.AssemblyRef.Reference)
                .Where(t => t.Name.EndsWith("Service"))
                .SingleInstance()
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Wvlytics.AssemblyRef.Reference)
                .Where(t => t.Name.EndsWith("Api"))
                .SingleInstance()
                .AsImplementedInterfaces();
        }   
    }
}