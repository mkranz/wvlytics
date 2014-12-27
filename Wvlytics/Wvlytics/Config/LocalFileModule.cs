using Autofac;
using Wvlytics.Services;

namespace Wvlytics.Config
{
    public class LocalFileModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BinaryScoreWriter>().As<IScoreWriter>();
            builder.RegisterType<BinaryObjectiveSnapshotWriter>().As<IObjectiveSnapshotWriter>();
            builder.RegisterType<BinaryScoreReader>().As<IScoreReader>();
            builder.RegisterType<LocalFileAppenderService>().As<IAppenderService>();
        }
    }
}