using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.GetAll.WareHouses;

namespace WareHouse.API.Application.Querie.CheckCode
{
    public class GetConvertRateByIdItemCommand : IRequest<int>
    {
        public string IdItem { get; set; }
        public string IdUnit { get; set; }
    }
    public class GetConvertRateByIdItemCommandHandler : IRequestHandler<GetConvertRateByIdItemCommand, int>
    {
        private readonly IRepositoryEF<Domain.Entity.Audit> _repository;


        public GetConvertRateByIdItemCommandHandler(IDapperEF repository)
        {
        }

        public async Task<int> Handle(GetConvertRateByIdItemCommand request,
            CancellationToken cancellationToken)
        {
           if (request == null)
                throw new ArgumentNullException(nameof(request));
            var sql = "select ConvertRate from WareHouseItemUnit where ItemId=@key1 and UnitId=@key2";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@key1", request.IdItem);
            parameter.Add("@key2", request.IdUnit);
            var res = await _repository.QueryFirstOrDefaultAsync<int>(sql, parameter, CommandType.Text);
            return res;
        }
    }
}