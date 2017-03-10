using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PipServices.Commons.Data;
using PipServices.Dummy.Logic;
using PipServices.Dummy.Models;

namespace PipServices.Dummy.Controllers
{
    [Route("dummies")]
    public class DummyWebApiController : Controller
    {
        public DummyWebApiController(IDummyController controller)
        {
            if (controller == null)
                throw new ArgumentNullException(nameof(controller));

            Controller = controller;
        }

        public IDummyController Controller { get; set; }

        [HttpGet]
        public async Task<IEnumerable<DummyObject>> GetDummiesAsync(
            [FromQuery(Name = "correlation_id")] string correlationId = null, string key = null, string skip = null,
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

            return await Controller.GetListByQueryAsync(correlationId, "", sortParams);
        }

        [HttpGet("{dummyId}")]
        public async Task<DummyObject> GetDummyByIdAsync(string dummyId,
            [FromQuery(Name = "correlation_id")] string correlationId = null)
        {
            return await Controller.GetOneByIdAsync(correlationId, dummyId);
        }

        [HttpPost]
        public async Task<DummyObject> CreateDummyAsync([FromBody] DummyObject dummy,
            [FromQuery(Name = "correlation_id")] string correlationId = null)
        {
            return await Controller.CreateAsync(correlationId, dummy);
        }

        [HttpPut("{dummyId}")]
        public async Task<DummyObject> UpdateDummyAsync(string dummyId, [FromBody] DummyObject dummy,
            [FromQuery(Name = "correlation_id")] string correlationId = null)
        {
            return await Controller.UpdateAsync(correlationId, dummy);
        }

        [HttpDelete("{dummyId}")]
        public async Task DeleteDummyAsync(string dummyId,
            [FromQuery(Name = "correlation_id")] string correlationId = null)
        {
            await Controller.DeleteByIdAsync(correlationId, dummyId);
        }
    }
}
