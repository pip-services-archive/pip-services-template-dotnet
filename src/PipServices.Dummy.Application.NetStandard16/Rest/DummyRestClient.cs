using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PipServices.Commons.Data;
using PipServices.Commons.Refer;
using PipServices.Dummy.Models;
using PipServices.Net.Rest;

namespace PipServices.Dummy.Rest
{
    public sealed class DummyRestClient : RestClient, IDummyService, IDescriptable
    {
        public static Descriptor Descriptor { get; } = new Descriptor("pip-services-dummies", "client", "rest", "default", "1.0");

        public Descriptor GetDescriptor()
        {
            return Descriptor;
        }

        private string PrepareQueryString(string path, SortParams sort)
        {
            if (sort == null)
                return path;

            var param = string.Join(",", sort.Select(x => x.Name).ToArray());

            return path + (string.IsNullOrWhiteSpace(param) ? string.Empty : "&sort=" + param);
        }

        public Task<List<DummyObject>> GetListByQueryAsync(string correlationId, string query, SortParams sort)
        {
            using (var timing = Instrument(correlationId, "dummy.get_list_by_query"))
            {

                return ExecuteAsync<List<DummyObject>>(
                    correlationId,
                    HttpMethod.Get,
                    PrepareQueryString($"dummies?correlation_id={correlationId}&query={query}", sort)
                );
            }
        }

        public Task<DummyObject> GetOneByIdAsync(string correlationId, string id)
        {
            using (var timing = Instrument(correlationId, "dummy.get_one_by_id"))
            {

                return ExecuteAsync<DummyObject>(
                    correlationId,
                    HttpMethod.Get,
                    $"dummies/{id}?correlation_id={correlationId}"
                );
            }
        }

        public Task<DummyObject> CreateAsync(string correlationId, DummyObject entity)
        {
            using (var timing = Instrument(correlationId, "dummy.create"))
            {
                return ExecuteAsync<DummyObject>(
                    correlationId,
                    HttpMethod.Post,
                    $"dummies?correlation_id={correlationId}",
                    entity
                );
            }
        }

        public Task<DummyObject> UpdateAsync(string correlationId, DummyObject entity)
        {
            using (var timing = Instrument(correlationId, "dummy.update"))
            {
                return ExecuteAsync<DummyObject>(
                    correlationId,
                    HttpMethod.Put,
                    $"dummies?correlation_id={correlationId}",
                    entity
                );
            }
        }

        public Task<DummyObject> DeleteByIdAsync(string correlationId, string id)
        {
            using (var timing = Instrument(correlationId, "dummy.delete_by_id"))
            {
                return ExecuteAsync<DummyObject>(
                    correlationId,
                    HttpMethod.Delete,
                    $"dummies/{id}?correlation_id={correlationId}"
                );
            }
        }
    }
}
