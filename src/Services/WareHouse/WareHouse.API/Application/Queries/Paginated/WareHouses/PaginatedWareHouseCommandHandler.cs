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
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Queries.Paginated.WareHouses
{
    public class
        PaginatedWareHouseCommandHandler : IRequestHandler<PaginatedWareHouseCommand, IPaginatedList<WareHouseDTO>>
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
            request.SearchModel.KeySearch = request.SearchModel.KeySearch?.Trim();
            if (request.SearchModel.KeySearch == null)
                request.SearchModel.KeySearch = "";
            StringBuilder sbCount = new StringBuilder();
            sbCount.Append("SELECT COUNT(*) FROM ( select * from WareHouse  ");
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from WareHouse ");
            sb.Append(" where Inactive =@active ");
            sbCount.Append(" where Inactive =@active ");
            if (!string.IsNullOrEmpty(request.SearchModel.KeySearch))
            {
                sb.Append(" and Code like @key or Name like @key ");
                sbCount.Append(" and Code like @key or Name like @key ");
            }
            sb.Append(" and OnDelete=0 ");
            sbCount.Append(" and OnDelete=0 ");
            sbCount.Append(" ) t   ");
            sb.Append(" order by Name OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY ");
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@key", '%' + request.SearchModel.KeySearch + '%');
            parameter.Add("@skip", (request.SearchModel.PageIndex - 1) * request.SearchModel.PageNumber);
            parameter.Add("@take", request.SearchModel.PageNumber);
            parameter.Add("@active", request.SearchModel.Active ? 1 : 0);
            _list.Result = await _repository.GetList<WareHouseDTO>(sb.ToString(), parameter, CommandType.Text);
            _list.totalCount = await _repository.GetAyncFirst<int>(sbCount.ToString(), parameter, CommandType.Text);
            return _list;
        }
    }
}