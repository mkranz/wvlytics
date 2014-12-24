using System;
using System.Collections.Generic;
using System.Net;
using System.Reactive.Linq;
using Common.Logging;
using Common.Logging.Simple;
using RestSharp;

namespace Wvlytics.Util
{
    public class LogHost
    {
        public static ILog Default = new ConsoleOutLogger("Default", LogLevel.All, false, true, false,
            "yyyy-MM-dd hh:mm:ss", true);
    }

    public static class RestSharpRxHelper
    {
        public static IObservable<T> Flatten<T>(this IObservable<List<T>> observable)
        {
            return observable.SelectMany(x => x);
        }

        public static IObservable<T> RequestAsync<T>(this IRestClient client, IRestRequest request) where T:new()
        {
            var ret = Observable.StartAsync(() => client.ExecuteTaskAsync<T>(request));
            return ret.ThrowOnRestResponseFailure().Select(x => x.Data);
        }
        
        static IObservable<T> ThrowOnRestResponseFailure<T>(this IObservable<T> observable)
            where T : IRestResponse
        {
            return observable.SelectMany(x =>
            {
                if (x == null)
                {
                    return Observable.Return(x);
                }

                if (x.ErrorException != null)
                {
                    return Observable.Throw<T>(x.ErrorException);
                }

                if (x.ResponseStatus == ResponseStatus.Error)
                {
                    LogHost.Default.WarnFormat("Response Status failed for {0}: {1}", x.ResponseUri, x.ResponseStatus);
                    return Observable.Throw<T>(new Exception("Request Error"));
                }

                if (x.ResponseStatus == ResponseStatus.TimedOut)
                {
                    LogHost.Default.WarnFormat("Response Status failed for {0}: {1}", x.ResponseUri, x.ResponseStatus);
                    return Observable.Throw<T>(new Exception("Request Timed Out"));
                }

                if ((int)x.StatusCode >= 400)
                {
                    LogHost.Default.WarnFormat("Response Status failed for {0}: {1}", x.ResponseUri, x.StatusCode);
                    return Observable.Throw<T>(new WebException(x.Content));
                }

                if (x.ResponseStatus == ResponseStatus.Aborted)
                {
                    LogHost.Default.WarnFormat("Response Status failed for {0}: {1}", x.ResponseUri, x.ResponseStatus);
                    return Observable.Throw<T>(new Exception("Request aborted"));
                }

                return Observable.Return(x);
            });
        }
    }
}