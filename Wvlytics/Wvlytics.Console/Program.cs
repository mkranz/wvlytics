using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Wvlytics.Services;

namespace Wvlytics.Console
{
    class Program
    {
        private static bool _running = true;
        static void Main(string[] args)
        {
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            var container = IoC.Init();

            var snapshotter = container.Resolve<ISnapshottingService>();

            new Task(() => SnapshotLoop(snapshotter)).Start();
			while (_running) {}
			//System.Console.ReadKey ();
            _running = false;
        }

        private static void SnapshotLoop(ISnapshottingService snapshotter)
        {
            while (_running)
            {
                new Task(snapshotter.SnapshotAll).Start();
                Thread.Sleep(60000);
            }
        }
    }
}
