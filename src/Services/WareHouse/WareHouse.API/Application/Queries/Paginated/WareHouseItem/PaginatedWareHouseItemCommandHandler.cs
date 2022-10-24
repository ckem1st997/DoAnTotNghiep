using System;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using WareHouse.API.Application.Extensions;
using WareHouse.API.Application.Interface;
using WareHouse.API.Application.Model;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Queries.Paginated.WareHouseItem
{
    public class PaginatedWareHouseItemCommandHandler : IRequestHandler<PaginatedWareHouseItemCommand, IPaginatedList<WareHouseItemDTO>>
    {
        private readonly IDapper _repository;
        private readonly IPaginatedList<WareHouseItemDTO> _list;

        public PaginatedWareHouseItemCommandHandler(IDapper repository, IPaginatedList<WareHouseItemDTO> list)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _list = list ?? throw new ArgumentNullException(nameof(list));
        }

        public async Task<IPaginatedList<WareHouseItemDTO>> Handle(PaginatedWareHouseItemCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                return null;
            request.KeySearch = request.KeySearch?.Trim() ?? "";
            StringBuilder sbCount = new StringBuilder();
            sbCount.Append("SELECT COUNT(*) FROM (  ");

            sbCount.Append(" select WareHouseItem.Id ");
            sbCount.Append(" from WareHouseItem  ");
            sbCount.Append(" left join Vendor on WareHouseItem.VendorID=Vendor.Id and Vendor.OnDelete=0  ");
            sbCount.Append(" left join Unit on WareHouseItem.UnitId=Unit.Id and Unit.OnDelete=0 ");
            sbCount.Append(" left join WareHouseItemCategory on WareHouseItem.CategoryID=WareHouseItemCategory.Id  and WareHouseItemCategory.OnDelete=0 where ");



            StringBuilder sb = new StringBuilder();

            sb.Append(" select WareHouseItem.Id, WareHouseItem.Code,WareHouseItem.Name,WareHouseItem.Description, ");
            sb.Append(" WareHouseItem.Country,WareHouseItem.Inactive,  ");
            sb.Append(" WareHouseItemCategory.Name as CategoryID, Vendor.Name as VendorID, Unit.UnitName as UnitId from WareHouseItem  ");
            sb.Append(" left join Vendor on WareHouseItem.VendorID=Vendor.Id and Vendor.OnDelete=0  ");
            sb.Append(" left join Unit on WareHouseItem.UnitId=Unit.Id and Unit.OnDelete=0 ");
            sb.Append(" left join WareHouseItemCategory on WareHouseItem.CategoryID=WareHouseItemCategory.Id  and WareHouseItemCategory.OnDelete=0 where ");
            if (request.Active != null)
            {
                sb.Append("  WareHouseItem.Inactive =@active and ");
                sbCount.Append("  WareHouseItem.Inactive =@active and ");
            }
            if (!string.IsNullOrEmpty(request.KeySearch))
            {
                sb.Append("  (WareHouseItem.Code like @key or WareHouseItem.Name like @key) and ");
                sbCount.Append("  (WareHouseItem.Code like @key or WareHouseItem.Name like @key) and ");
            }
            sb.Append("  WareHouseItem.OnDelete=0  ");
            sbCount.Append("  WareHouseItem.OnDelete=0  ");
            //
            sbCount.Append(" ) t   ");
            sb.Append(" order by WareHouseItem.Name OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY ");
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@key", '%' + request.KeySearch + '%');
            parameter.Add("@skip", request.Skip);
            parameter.Add("@take", request.Take);
            parameter.Add("@active", request.Active == true ? 1 : 0);
            Console.WriteLine(sb.ToString());
            _list.Result = await _repository.GetList<WareHouseItemDTO>(sb.ToString(), parameter, CommandType.Text);
            _list.totalCount = await _repository.GetAyncFirst<int>(ValidatorString.GetSqlCount(sb.ToString(), SqlEnd: "order"), parameter, CommandType.Text);
            return _list;
        }
    }
}