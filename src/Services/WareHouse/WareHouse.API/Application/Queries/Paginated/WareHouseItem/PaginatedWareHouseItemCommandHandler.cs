using System;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using WareHouse.API.Application.Interface;
using WareHouse.API.Application.Model;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Queries.Paginated.WareHouseItem
{
    public class PaginatedWareHouseItemCommandHandler: IRequestHandler<PaginatedWareHouseItemCommand, IPaginatedList<WareHouseItemDTO>>
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
            sbCount.Append("SELECT COUNT(*) FROM ( select * from WareHouseItem where ");
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from WareHouseItem where ");
            if (request.Active != null)
            {
                sb.Append("  Inactive =@active and ");
                sbCount.Append("  Inactive =@active and ");
            }
            if (!string.IsNullOrEmpty(request.KeySearch))
            {
                sb.Append("  Code like @key or Name like @key and ");
                sbCount.Append("  Code like @key or Name like @key and ");
            }
            sb.Append("  OnDelete=0 ");
            sbCount.Append("  OnDelete=0 ");
            //
            sbCount.Append(" ) t   ");
            sb.Append(" order by Name OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY ");
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@key", '%' + request.KeySearch + '%');
            parameter.Add("@skip", request.Skip);
            parameter.Add("@take", request.Take);
            parameter.Add("@active", request.Active==true ? 1 : 0);
            _list.Result = await _repository.GetList<WareHouseItemDTO>(sb.ToString(), parameter, CommandType.Text);
            _list.totalCount = await _repository.GetAyncFirst<int>(sbCount.ToString(), parameter, CommandType.Text);
            return _list;
        }
    }
}