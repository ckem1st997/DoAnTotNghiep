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


namespace WareHouse.API.Application.Queries.Paginated.Unit
{
    public class PaginatedUnitCommandHandler : IRequestHandler<PaginatedUnitCommand, IPaginatedList<UnitDTO>>
    {
        private readonly IDapper _repository;
        private readonly IPaginatedList<UnitDTO> _list;

        public PaginatedUnitCommandHandler(IDapper repository, IPaginatedList<UnitDTO> list)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _list = list ?? throw new ArgumentNullException(nameof(list));
        }

        public async Task<IPaginatedList<UnitDTO>> Handle(PaginatedUnitCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                return null;
            request.SearchModel.KeySearch = request.SearchModel.KeySearch?.Trim() ?? "";
            StringBuilder sbCount = new StringBuilder();
            sbCount.Append("SELECT COUNT(*) FROM ( select * from Unit  ");
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from Unit ");
            sb.Append(" where Inactive =@active ");
            sbCount.Append(" where Inactive =@active ");
            if (!string.IsNullOrEmpty(request.SearchModel.KeySearch))
            {
                sb.Append(" and UnitName like @key ");
                sbCount.Append(" and UnitName like @key ");
            }
            sb.Append(" and OnDelete=0 ");
            sbCount.Append(" and OnDelete=0 ");
            //
            sbCount.Append(" ) t   ");
            sb.Append(" order by UnitName OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY ");
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@key", '%' + request.SearchModel.KeySearch + '%');
            parameter.Add("@skip", (request.SearchModel.PageIndex - 1) * request.SearchModel.PageNumber);
            parameter.Add("@take", request.SearchModel.PageNumber);
            parameter.Add("@active", request.SearchModel.Active ? 1 : 0);
            _list.Result = await _repository.GetList<UnitDTO>(sb.ToString(), parameter, CommandType.Text);
            _list.totalCount = await _repository.GetAyncFirst<int>(sbCount.ToString(), parameter, CommandType.Text);
            return _list;
        }
    }
}