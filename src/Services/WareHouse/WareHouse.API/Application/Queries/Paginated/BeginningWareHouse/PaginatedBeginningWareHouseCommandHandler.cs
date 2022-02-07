using System;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using WareHouse.API.Application.Interface;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.BaseModel;
using WareHouse.Domain.IRepositories;


namespace WareHouse.API.Application.Queries.Paginated
{
    public class PaginatedBeginningWareHouseCommand : BaseSearchModel, IRequest<IPaginatedList<BeginningWareHouseDTO>>
    {

    }

    public class PaginatedBeginningWareHouseCommandHandler : IRequestHandler<PaginatedBeginningWareHouseCommand, IPaginatedList<BeginningWareHouseDTO>>
    {
        private readonly IDapper _repository;
        private readonly IPaginatedList<BeginningWareHouseDTO> _list;

        public PaginatedBeginningWareHouseCommandHandler(IDapper repository, IPaginatedList<BeginningWareHouseDTO> list)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _list = list ?? throw new ArgumentNullException(nameof(list));
        }

        public async Task<IPaginatedList<BeginningWareHouseDTO>> Handle(PaginatedBeginningWareHouseCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                return null;
            request.KeySearch = request.KeySearch?.Trim() ?? "";
            StringBuilder sbCount = new StringBuilder();
            sbCount.Append("SELECT COUNT(*) FROM ( ");
            sbCount.Append(" select BeginningWareHouse.Id from BeginningWareHouse ");
            sbCount.Append(" join WareHouse on BeginningWareHouse.WareHouseId=WareHouse.Id and WareHouse.OnDelete=0 ");
            sbCount.Append(" join WareHouseItem on BeginningWareHouse.ItemId=WareHouseItem.Id and WareHouseItem.OnDelete=0 ");
            sbCount.Append(" join Unit on BeginningWareHouse.UnitId=Unit.Id and Unit.OnDelete=0 ");
            sbCount.Append(" where ");
            StringBuilder sb = new StringBuilder();
            sb.Append("select BeginningWareHouse.Id,WareHouse.Name as WareHouseName,Unit.UnitName,WareHouseItem.Name as ItemName,BeginningWareHouse.Quantity from BeginningWareHouse ");
            sb.Append(" join WareHouse on BeginningWareHouse.WareHouseId=WareHouse.Id and WareHouse.OnDelete=0 ");
            sb.Append(" join WareHouseItem on BeginningWareHouse.ItemId=WareHouseItem.Id and WareHouseItem.OnDelete=0 ");
            sb.Append(" join Unit on BeginningWareHouse.UnitId=Unit.Id and Unit.OnDelete=0 ");
            sb.Append(" where ");
            if (!string.IsNullOrEmpty(request.KeySearch))
            {
                sb.Append("  (WareHouse.Name like @key or WareHouseItem.Name like @key) and ");
                sbCount.Append("  (WareHouse.Name like @key or WareHouseItem.Name like @key) and ");
            }
            sb.Append("  BeginningWareHouse.OnDelete=0 ");
            sbCount.Append("  BeginningWareHouse.OnDelete=0 ");
            //
            sbCount.Append(" ) t   ");
            sb.Append(" order by UnitName OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY ");
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@key", '%' + request.KeySearch + '%');
            parameter.Add("@skip", request.Skip);
            parameter.Add("@take", request.Take);
            _list.Result = await _repository.GetList<BeginningWareHouseDTO>(sb.ToString(), parameter, CommandType.Text);
            _list.totalCount = await _repository.GetAyncFirst<int>(sbCount.ToString(), parameter, CommandType.Text);
            return _list;
        }
    }
}