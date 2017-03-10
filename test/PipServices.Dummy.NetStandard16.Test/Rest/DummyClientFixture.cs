using System.Threading.Tasks;
using PipServices.Dummy.Models;
using Xunit;

namespace PipServices.Dummy.Rest
{
    public sealed class DummyClientFixture
    {
        private readonly DummyObject _dummy1 = new DummyObject { Id = "1", Key = "Key 1", Content = "Content 1" };
        private readonly DummyObject _dummy2 = new DummyObject { Id = "2", Key = "Key 2", Content = "Content 2" };

        private readonly IDummyService _client;

        public DummyClientFixture(IDummyService client)
        {
            Assert.NotNull(client);
            _client = client;
        }

        public async Task TestCrudOperations()
        {
            // Create one dummy
            var dummy1 = await _client.CreateAsync(null, _dummy1);

            Assert.NotNull(dummy1);
            Assert.NotNull(dummy1.Id);
            Assert.Equal(_dummy1.Key, dummy1.Key);
            Assert.Equal(_dummy1.Content, dummy1.Content);

            // Create another dummy
            var dummy2 = await _client.CreateAsync(null, _dummy2);

            Assert.NotNull(dummy2);
            Assert.NotNull(dummy2.Id);
            Assert.Equal(_dummy2.Key, dummy2.Key);
            Assert.Equal(_dummy2.Content, dummy2.Content);

            // Get all dummies
            var dummies = await _client.GetListByQueryAsync(null, null, null);
            Assert.NotNull(dummies);
            Assert.Equal(2, dummies.Count);

            // Update the dummy
            dummy1.Content = "Updated Content 1";
            var dummy = await _client.UpdateAsync(null, dummy1);

            Assert.NotNull(dummy);
            Assert.Equal(dummy1.Id, dummy.Id);
            Assert.Equal(dummy1.Key, dummy.Key);
            Assert.Equal("Updated Content 1", dummy.Content);

            // Delete the dummy
            await _client.DeleteByIdAsync(null, dummy1.Id);

            // Try to get deleted dummy
            dummy = await _client.GetOneByIdAsync(null, dummy1.Id);
            Assert.Null(dummy);
        }
    }
}
