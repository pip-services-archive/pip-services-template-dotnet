﻿using System;
using System.Threading;
using System.Threading.Tasks;
using PipServices.Net.Rest;
using PipServices.Commons.Config;
using PipServices.Commons.Refer;
using PipServices.Dummy.Logic;
using PipServices.Dummy.Memory;
using Xunit;

namespace PipServices.Dummy.Rest
{
    public sealed class DummyRestClientTest : IDisposable
    {
        private static readonly ConfigParams RestConfig = ConfigParams.FromTuples(
            "connection.protocol", "http",
            "connection.host", "localhost",
            "connection.port", 3000
            );

        private readonly DummyController _ctrl;
        private readonly DummyRestService _service;
        private readonly DummyRestClient _client;
        private readonly DummyClientFixture _fixture;
        private readonly CancellationTokenSource _source;
        private readonly DummyMemoryPersistence _pers;

        public DummyRestClientTest()
        {
            _ctrl = new DummyController();

            _pers = new DummyMemoryPersistence();

            _service = new DummyRestService();

            _service.Configure(RestConfig);

            _client = new DummyRestClient();
            _client.Configure(RestConfig);

            var references = References.FromList(_pers, _ctrl, _client, _service);

            _pers.SetReferences(references);
            _ctrl.SetReferences(references);
            _client.SetReferences(references);
            _service.SetReferences(references);

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            var serviceTask = Task.Run(async () => await _service.OpenAsync(null));
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

#if !CORE_NET
            serviceTask.Wait();
#endif

            _fixture = new DummyClientFixture(_client);

            _source = new CancellationTokenSource();

            var clientTask = _client.OpenAsync(null);
            clientTask.Wait();
        }

        [Fact]
        public void TestCrudOperations()
        {
            var task = _fixture.TestCrudOperations();
            task.Wait();
        }

        public void Dispose()
        {
            var task = _client.CloseAsync(null);
            task.Wait();

            task = _service.CloseAsync(null);
            task.Wait();
        }
    }
}
