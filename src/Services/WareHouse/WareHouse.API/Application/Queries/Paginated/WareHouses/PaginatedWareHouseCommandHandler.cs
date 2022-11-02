
using Dapper;
using MediatR;
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
    public class PaginatedWareHouseCommandHandler : IRequestHandler<PaginatedWareHouseCommand, IPaginatedList<WareHouseDTO>>
    {
        private readonly IDapper _repository;
        private readonly IPaginatedList<WareHouseDTO> _list;

        public PaginatedWareHouseCommandHandler(IDapper repository, IPaginatedList<WareHouseDTO> list)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _list = list ?? throw new ArgumentNullException(nameof(list));
        }

        public async Task<IPaginatedList<WareHouseDTO>> Handle(PaginatedWareHouseCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                return null;
            request.KeySearch = request.KeySearch?.Trim().Replace("=", "");
            if (request.KeySearch == null)
                request.KeySearch = "";
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
                sb.Append("  ( Name like @key or Code like @key  )  and ");
                sbCount.Append("  ( Name like @key or Code like @key) and ");
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
            _list.Result = await _repository.GetList<WareHouseDTO>(sb.ToString(), parameter, CommandType.Text);
            _list.totalCount = await _repository.GetAyncFirst<int>(ValidatorString.GetSqlCount(sb.ToString(), SqlEnd: "order"), parameter, CommandType.Text);
            return _list;
        }
    }
}
