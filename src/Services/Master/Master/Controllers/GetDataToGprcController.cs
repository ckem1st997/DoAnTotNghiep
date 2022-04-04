using Grpc.Core;
using GrpcGetDataToMaster;
using Infrastructure;
using Master.Controllers.BaseController;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Master.Controllers
{
    public class GetDataToGprcController : BaseControllerMaster
    {
        private readonly GrpcGetDataToMasterService _grpcGetDataToMaster;
        private readonly MasterdataContext masterdataContext;

        public GetDataToGprcController(MasterdataContext _masterdataContext, GrpcGetDataToMasterService grpcGetDataToMaster)
        {
            masterdataContext = _masterdataContext;
            _grpcGetDataToMaster = grpcGetDataToMaster;
        }

        [HttpGet]
        public async Task<IActionResult> GetAction()
        {
            for (int i = 0; i < 10; i++)
            {
                var tem = new FakeDataMaster()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Customer " + i,
                    OnDelete = false,
                    Type = 5
                };
                await masterdataContext.FakeDataMasters.AddAsync(tem);
            }
            var res = await masterdataContext.SaveChangesAsync();
            //  var res = await _grpcGetDataToMaster.GetCreateBy(new Params());
            return Ok(res);
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var res= masterdataContext.FakeDataMasters.ToList();
            return Ok(res);
        }
    }
}
