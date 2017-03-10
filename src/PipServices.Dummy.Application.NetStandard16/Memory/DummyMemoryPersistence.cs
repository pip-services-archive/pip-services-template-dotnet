using PipServices.Commons.Refer;
using PipServices.Data.Memory;
using PipServices.Dummy.Models;

namespace PipServices.Dummy.Memory
{
    public class DummyMemoryPersistence : MemoryPersistence<DummyObject,string>, IDescriptable, IDummyPersistance
    {
        public static Descriptor Descriptor { get; } = new Descriptor("pip-services-dummies", "persistance", "default", "default", "1.0");

        public Descriptor GetDescriptor()
        {
            return Descriptor;
        }
    }
}
