using PipServices.Commons.Refer;
using PipServices.Net.Rest;

namespace PipServices.Dummy.Rest
{
    public class DummyRestService : RestService<Startup>, IDescriptable
    {
        public static Descriptor Descriptor { get; } = new Descriptor("pip-services-dummies", "service", "rest", "default", "1.0");

        public Descriptor GetDescriptor()
        {
            return Descriptor;
        }
    }
}
