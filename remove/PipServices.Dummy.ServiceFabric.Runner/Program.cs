using PipServices.Dummy.Service.Run;

namespace PipServices.Dummy.Service.Runner
{
    internal static class Program
    {
        /// <summary>
        /// This is the entry point of the service host process.
        /// </summary>
        private static void Main()
        {
            var runner = new DummyServiceRunner();
            runner.RunWithConfigFile("config.json");
        }
    }
}
