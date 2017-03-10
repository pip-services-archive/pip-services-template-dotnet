using PipServices.Data;
using PipServices.Dummy.Models;

namespace PipServices.Dummy.Rest
{
    public interface IDummyService : IWriter<DummyObject, string>, IGetter<DummyObject, string>, IQuerableReader<DummyObject>
    {
    }
}
