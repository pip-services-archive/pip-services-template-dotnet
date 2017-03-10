using System;
using System.Diagnostics;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors.Runtime;
using PipServices.Dummy.Service.Run;
using PipServices.Runtime.Run;

namespace PipServices.Dummy
{
    internal static class Program
    {
        private static readonly Microservice Microservice = new DummyMicroservice();
        /// <summary>
        /// This is the entry point of the service host process.
        /// </summary>
        private static void Main()
        {
            try
            {
                // This line registers an Actor Service to host your actor class with the Service Fabric runtime.
                // The contents of your ServiceManifest.xml and ApplicationManifest.xml files
                // are automatically populated when you build this project.
                // For more information, see http://aka.ms/servicefabricactorsplatform

                ActorRuntime.RegisterActorAsync<DummyActor>(
                    (context, actorType) =>
                    {
                        var service = new ActorService(context, actorType,
                            (actorService, id) => new DummyActor(actorService, id, Microservice));
                        return service;
                    }).
                    GetAwaiter().GetResult();

                Thread.Sleep(Timeout.Infinite);
            }
            catch (Exception e)
            {
                ActorEventSource.Current.ActorHostInitializationFailed(e.ToString());
                throw;
            }
        }
    }
}
