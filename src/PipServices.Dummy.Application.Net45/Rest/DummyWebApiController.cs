using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using PipServices.Commons.Data;
using PipServices.Dummy.Logic;
using PipServices.Dummy.Models;
using PipServices.Net.Rest;

namespace PipServices.Dummy.Rest
{
    [RoutePrefix("dummies")]
    public class DummyWebApiController : ApiController, IHttpLogicController<IDummyController>
    {
        public DummyWebApiController(IDummyController controller)
        {
            if (controller == null)
                throw new ArgumentNullException(nameof(controller));

            Logic = controller;
        }

        public DummyWebApiController()
        {
        }

        [Route("")]
        [HttpGet]
        public async Task<List<DummyObject>> GetDummiesAsync(
            [FromUri(Name = "correlation_id")] string correlationId = null, string key = null, string skip = null,
            string take = null, string total = null, string sort = null)
        {
            var sortParams = new SortParams();

            if (!string.IsNullOrWhiteSpace(sort))
            {
                var fieldArr = sort.Split(',');

                foreach (var field in fieldArr)
                {
                    sortParams.Add(new SortField(field));
                }
            }

            return await Logic.GetListByQueryAsync(correlationId, "", sortParams);
        }

        [Route("{dummyId}")]
        [HttpGet]
        public async Task<DummyObject> GetDummyById(string dummyId,
            [FromUri(Name = "correlation_id")] string correlationId = null)
        {
            return await Logic.GetOneByIdAsync(correlationId, dummyId);
        }

        [Route("")]
        [HttpPost]
        public async Task<DummyObject> CreateDummy([FromBody] DummyObject dummy,
            [FromUri(Name = "correlation_id")] string correlationId = null)
        {
            return await Logic.CreateAsync(correlationId, dummy);
        }

        [Route("")]
        [HttpPut]
        public async Task<DummyObject> UpdateDummyAsync([FromBody] DummyObject dummy,
            [FromUri(Name = "correlation_id")] string correlationId = null)
        {
            return await Logic.UpdateAsync(correlationId, dummy);
        }

        [Route("{dummyId}")]
        [HttpDelete]
        public async Task DeleteDummyAsync(string dummyId,
            [FromUri(Name = "correlation_id")] string correlationId = null)
        {
            await Logic.DeleteByIdAsync(correlationId, dummyId);
        }

        public IDummyController Logic { get; set; }
    }
}
