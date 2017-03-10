using PipServices.Dummy.Service.Run;

namespace PipServices.Dummy.Actor.Runner
{
    internal static class Program
    {
        /// <summary>
        /// This is the entry point of the service host process.
        /// </summary>
        private static void Main()
        {
            var runner = new DummyActorRunner();
            runner.RunWithConfigFile("config.json");
        }
    }
}
