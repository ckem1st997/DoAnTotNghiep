using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.GetAll.WareHouses;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Querie.CheckCode
{
    public class GetConvertRateByIdItemCommand : IRequest<int>
    {
        public string IdItem { get; set; }
        public string IdUnit { get; set; }
    }
    public class GetConvertRateByIdItemCommandHandler : IRequestHandler<GetConvertRateByIdItemCommand, int>
    {
        private readonly IDapper _repository;

        public GetConvertRateByIdItemCommandHandler(IDapper repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
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
            var res = await _repository.GetAyncFirst<int>(sql, parameter, CommandType.Text);
            return res;
        }
    }
}