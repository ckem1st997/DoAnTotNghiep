
using Dapper;
using MediatR;
using Share.Base.Core.Extensions;
using Share.Base.Service;
using Share.Base.Service.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WareHouse.API.Application.Interface;
using WareHouse.API.Application.Model;

namespace WareHouse.API.Application.Queries.Paginated.WareHouses
{
    /// <summary>
    /// Đánh index, nên order by theo thứ tự index hoặc order by theo cột đánh index đầu tiên
    /// nếu order by Address hoặc Description hoặc OnDelete thì sẽ rất chậm, nếu order by theo Name
    /// Thì sẽ rất nhanh vì đứng ở đầu index
    /// select * from WareHouse where  OnDelete=0 order by Name OFFSET 0 ROWS FETCH NEXT 15 ROWS ONLY
    ///--CREATE INDEX WareHouse_index_name
    ///--ON WareHouse(Name, Address, Description, OnDelete);
    /// </summary>
    public class PaginatedWareHouseCommandHandler : IRequestHandler<PaginatedWareHouseCommand, PaginatedList<WareHouseDTO>>
    {
        private readonly IRepositoryEF<Domain.Entity.WareHouse> _repository;

        public PaginatedWareHouseCommandHandler()
        {
            _repository = EngineContext.Current.Resolve<IRepositoryEF<Domain.Entity.WareHouse>>(DataConnectionHelper.ConnectionStringNames.Warehouse) ?? throw new ArgumentNullException(nameof(_repository));
        }

        public async Task<PaginatedList<WareHouseDTO>> Handle(PaginatedWareHouseCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                return null;
            request.KeySearch = request.KeySearch?.Trim().Replace("=", "");
            if (request.KeySearch == null)
                request.KeySearch = "";
            var _list = new PaginatedList<WareHouseDTO>();
            StringBuilder sbCount = new StringBuilder();
            sbCount.Append("SELECT COUNT(*) FROM ( select Id,Code,Name,Address,Description,Inactive from WareHouse where ");
            StringBuilder sb = new StringBuilder();
            sb.Append("select Id,Code,Name,Address,Description,Inactive from WareHouse where ");
            if (request.Active != null)
            {
                sb.Append("  Inactive =@active and ");
                sbCount.Append("  Inactive =@active and ");
            }
            if (!string.IsNullOrEmpty(request.KeySearch))
            {
                sb.Append("  ( Name like @key or Code like @key or Description like @key   )  and ");
                sbCount.Append("  ( Name like @key or Code like @key or Description like @key ) and ");
            }
            sb.Append("  OnDelete=0 ");
            sbCount.Append("  OnDelete=0 ");
            sbCount.Append(" ) t   ");
            sb.Append(" order by Name OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY ");
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@key", '%' + request.KeySearch + '%');
            parameter.Add("@skip", request.Skip);
            parameter.Add("@take", request.Take);
            parameter.Add("@active", request.Active == true ? 1 : 0);
            _list.Result = await _repository.QueryAsync<WareHouseDTO>(sb.ToString(), parameter, CommandType.Text);
            _list.totalCount = await _repository.QueryFirstOrDefaultAsync<int>(ValidatorString.GetSqlCount(sb.ToString(), SqlEnd: "order"), parameter, CommandType.Text);
            return _list;
        }
    }
}
