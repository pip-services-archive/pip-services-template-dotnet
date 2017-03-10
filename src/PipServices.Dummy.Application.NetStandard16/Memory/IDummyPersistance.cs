using PipServices.Data;
using PipServices.Dummy.Models;

namespace PipServices.Dummy.Memory
{
    interface IDummyPersistance : IWriter<DummyObject, string>, IGetter<DummyObject, string>, IQuerableReader<DummyObject>
    {
    }
}
