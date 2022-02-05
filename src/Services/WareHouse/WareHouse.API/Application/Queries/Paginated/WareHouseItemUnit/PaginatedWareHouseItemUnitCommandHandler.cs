using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WareHouse.API.Application.Interface;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.BaseModel;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Queries.Paginated.WareHouseItemUnit
{
    [DataContract]
    public class PaginatedWareHouseItemUnitCommand : BaseSearchModel, IRequest<IPaginatedList<WareHouseItemUnitDTO>>
    {
    }
    public class
        PaginatedWareHouseItemUnitCommandHandler : IRequestHandler<PaginatedWareHouseItemUnitCommand, IPaginatedList<WareHouseItemUnitDTO>>
    {
        private readonly IDapper _repository;
        private readonly IPaginatedList<WareHouseItemUnitDTO> _list;

        public PaginatedWareHouseItemUnitCommandHandler(IDapper repository, IPaginatedList<WareHouseItemUnitDTO> list)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _list = list ?? throw new ArgumentNullException(nameof(list));
        }

        public async Task<IPaginatedList<WareHouseItemUnitDTO>> Handle(PaginatedWareHouseItemUnitCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                return null;
            request.KeySearch = request.KeySearch?.Trim().Replace("=", "");
            if (request.KeySearch == null)
                request.KeySearch = "";
            StringBuilder sbCount = new StringBuilder();
            sbCount.Append("SELECT COUNT(*) FROM ( select * from WareHouseItemUnit where ");
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from WareHouseItemUnit  inner join Unit on WareHouseItemUnit.UnitId=Unit.Id where ");
            if (string.IsNullOrEmpty(request.KeySearch))
                return _list;
            sb.Append("  ItemId=@key  and ");
            sbCount.Append("  ItemId=@key and ");
            sb.Append("  WareHouseItemUnit.OnDelete=0 ");
            sbCount.Append("  WareHouseItemUnit.OnDelete=0 ");
            sbCount.Append(" ) t   ");
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@key", request.KeySearch);
            _list.Result = await _repository.GetList<WareHouseItemUnitDTO>(sb.ToString(), parameter, CommandType.Text);
            _list.totalCount = await _repository.GetAyncFirst<int>(sbCount.ToString(), parameter, CommandType.Text);
            return _list;
        }
    }
}