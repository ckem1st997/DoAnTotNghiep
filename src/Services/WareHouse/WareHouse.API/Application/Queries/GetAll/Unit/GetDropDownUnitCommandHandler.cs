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

namespace WareHouse.API.Application.Queries.GetAll.Unit
{
    public class GetDropDownUnitCommandHandler : IRequestHandler<GetDropDownUnitCommand, IEnumerable<UnitDTO>>
    {
        private readonly IDapper _repository;

        public GetDropDownUnitCommandHandler(IDapper repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<UnitDTO>> Handle(GetDropDownUnitCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            const string sql = "select Id,UnitName from Unit where Inactive =@active and OnDelete=0 ";
            var parameter = new DynamicParameters();
            parameter.Add("@active", request.Active ? 1 : 0);
            var getAll = await _repository.GetAllAync<UnitDTO>(sql, parameter, CommandType.Text);
            return getAll;
        }
    }
}