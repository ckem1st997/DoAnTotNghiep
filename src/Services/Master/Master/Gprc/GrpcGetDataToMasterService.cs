using Grpc.Core;
using Infrastructure;
using Master.Extension;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GrpcGetDataToMaster
{
    public class GrpcGetDataToMasterService : GrpcGetData.GrpcGetDataBase
    {

        private readonly MasterdataContext _masterdataContext;
        private readonly IDapper _mediat;
        private readonly ILogger<GrpcGetDataToMasterService> _logger;

        public GrpcGetDataToMasterService(MasterdataContext masterdataContext, IDapper mediat, ILogger<GrpcGetDataToMasterService> logger)
        {
            _mediat = mediat;
            _logger = logger;
            _masterdataContext = masterdataContext;
        }

        public override async Task<ListCreateBy> GetCreateBy(Params request, ServerCallContext context)
        {
            //   var list = await _mediat.GetAllAync<ListCreateBy>("select * from FakeDataMaster where Type=1 order by Type ",null,System.Data.CommandType.Text)

            var list = new ListCreateBy();
            foreach (var item in FakeData.GetCreateBy())
            {
                var tem = new BaseSelectDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                };
                list.ListCreateBy_.Add(tem);
            }
            return await Task.FromResult(list);
        }

        public override Task<ListGetCustomer> GetCustomer(Params request, ServerCallContext context)
        {
            var list = new ListGetCustomer();
            foreach (var item in FakeData.GetCustomer())
            {
                var tem = new BaseSelectDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                };
                list.ListGetCustomer_.Add(tem);
            }
            return Task.FromResult(list);
        }


        public override Task<ListGetDepartment> GetDepartment(Params request, ServerCallContext context)
        {
            var list = new ListGetDepartment();
            foreach (var item in FakeData.GetDepartment())
            {
                var tem = new BaseSelectDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                };
                list.ListGetDepartment_.Add(tem);
            }
            return Task.FromResult(list);
        }



        public override Task<ListGetEmployee> GetEmployee(Params request, ServerCallContext context)
        {
            var list = new ListGetEmployee();
            foreach (var item in FakeData.GetEmployee())
            {
                var tem = new BaseSelectDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                };
                list.ListGetEmployee_.Add(tem);
            }
            return Task.FromResult(list);
        }

        public override Task<ListGetProject> GetProject(Params request, ServerCallContext context)
        {
            var list = new ListGetProject();
            foreach (var item in FakeData.GetProject())
            {
                var tem = new BaseSelectDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                };
                list.ListGetProject_.Add(tem);
            }
            return Task.FromResult(list);
        }
        public override Task<ListGetStation> GetStation(Params request, ServerCallContext context)
        {
            var list = new ListGetStation();
            foreach (var item in FakeData.GetStation())
            {
                var tem = new BaseSelectDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                };
                list.ListGetStation_.Add(tem);
            }
            return Task.FromResult(list);
        }

    }
}
