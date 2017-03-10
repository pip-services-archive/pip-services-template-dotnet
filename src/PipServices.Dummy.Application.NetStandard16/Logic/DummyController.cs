using System.Collections.Generic;
using System.Threading.Tasks;
using PipServices.Commons.Config;
using PipServices.Commons.Data;
using PipServices.Commons.Log;
using PipServices.Commons.Refer;
using PipServices.Commons.Run;
using PipServices.Dummy.Memory;
using PipServices.Dummy.Models;

namespace PipServices.Dummy.Logic
{
    public sealed class DummyController : IReferenceable, IReconfigurable, IOpenable, IClosable, INotifiable, IDummyController, IDescriptable
    {
        public static Descriptor Descriptor { get; } = new Descriptor("pip-services-dummies", "controller", "default", "default", "1.0");

        private readonly FixedRateTimer _timer;
        private readonly CompositeLogger _logger = new CompositeLogger();
        private IDummyPersistance _persistance;
        public string Message { get; private set; } = "Hello from controller!";
        public long Counter { get; private set; }

        public DummyController()
        {
            _timer = new FixedRateTimer(async () => { await NotifyAsync(null); }, 1000, 1000);
        }

        public void Configure(ConfigParams config)
        {
            Message = config.GetAsStringWithDefault("message", Message);
        }

        public void SetReferences(IReferences references)
        {
            _logger.SetReferences(references);

            _persistance = references.GetOneOptional<IDummyPersistance>(new Descriptor(null, "persistance", null, null, null));
        }

        public Task OpenAsync(string correlationId)
        {
            _timer.Start();
            _logger.Trace(correlationId, "Dummy controller opened");

            return Task.Delay(0);
        }

        public Task CloseAsync(string correlationId)
        {
            _timer.Stop();

            _logger.Trace(correlationId, "Dummy controller closed");

            return Task.Delay(0);
        }

        public Task NotifyAsync(string correlationId)
        {
            _logger.Info(correlationId, "{0} - {1}", Counter++, Message);

            return Task.Delay(0);
        }

        public Task<DummyObject> CreateAsync(string correlationId, DummyObject entity)
        {
            _logger.Trace(correlationId, "Dummy controller's CreateAsync was called");

            return _persistance.CreateAsync(correlationId, entity);
        }

        public async Task<DummyObject> UpdateAsync(string correlationId, DummyObject entity)
        {
            _logger.Trace(correlationId, "Dummy controller's UpdateAsync was called");

            var ent = await _persistance.UpdateAsync(correlationId, entity);

            return ent;
        }

        public Task<DummyObject> DeleteByIdAsync(string correlationId, string id)
        {
            _logger.Trace(correlationId, "Dummy controller's DeleteByIdAsync was called");

            return _persistance.DeleteByIdAsync(correlationId, id);
        }

        public Task<DummyObject> GetOneByIdAsync(string correlationId, string id)
        {
            _logger.Trace(correlationId, "Dummy controller's GetOneByIdAsync was called");

            return _persistance.GetOneByIdAsync(correlationId, id);
        }

        public Task<List<DummyObject>> GetListByQueryAsync(string correlationId, string query, SortParams sort)
        {
            _logger.Trace(correlationId, "Dummy controller's GetListByQueryAsync was called");

            return _persistance.GetListByQueryAsync(correlationId, query, sort);
        }

        public Descriptor GetDescriptor()
        {
            return Descriptor;
        }
    }
}
