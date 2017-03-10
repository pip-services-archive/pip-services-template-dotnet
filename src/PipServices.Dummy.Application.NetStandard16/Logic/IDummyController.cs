using PipServices.Data;
using PipServices.Dummy.Models;

namespace PipServices.Dummy.Logic
{
    public interface IDummyController : IWriter<DummyObject, string>, IGetter<DummyObject, string>, IQuerableReader<DummyObject>
    {
    }
}
