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

namespace WareHouse.API.Application.Queries.Paginated.WareHouseItemCategory
{
    public class PaginatedWareHouseItemCategoryCommandHandler: IRequestHandler<PaginatedWareHouseItemCategoryCommand, IPaginatedList<WareHouseItemCategoryDTO>>
    {
        private readonly IDapper _repository;
        private readonly IPaginatedList<WareHouseItemCategoryDTO> _list;

        public PaginatedWareHouseItemCategoryCommandHandler(IDapper repository, IPaginatedList<WareHouseItemCategoryDTO> list)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _list = list ?? throw new ArgumentNullException(nameof(list));
        }

        public async Task<IPaginatedList<WareHouseItemCategoryDTO>> Handle(PaginatedWareHouseItemCategoryCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                return null;
            request.SearchModel.KeySearch = request.SearchModel.KeySearch?.Trim();
            if (request.SearchModel.KeySearch == null)
                request.SearchModel.KeySearch = "";
            StringBuilder sbCount = new StringBuilder();
            sbCount.Append("SELECT COUNT(*) FROM ( select * from WareHouseItemCategory  ");
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from WareHouseItemCategory ");
            sb.Append(" where Inactive =@active ");
            sbCount.Append(" where Inactive =@active ");
            if (!string.IsNullOrEmpty(request.SearchModel.KeySearch))
            {
                sb.Append(" and Code like @key or Name like @key ");
                sbCount.Append(" and Code like @key or Name like @key ");
            }
            sbCount.Append(" ) t   ");
            sb.Append(" order by Name OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY ");
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@key", '%' + request.SearchModel.KeySearch + '%');
            parameter.Add("@skip", (request.SearchModel.PageIndex - 1) * request.SearchModel.PageNumber);
            parameter.Add("@take", request.SearchModel.PageNumber);
            parameter.Add("@active", request.SearchModel.Active ? 1 : 0);
            _list.Result = await _repository.GetList<WareHouseItemCategoryDTO>(sb.ToString(), parameter, CommandType.Text);
            _list.totalCount = await _repository.GetAyncFirst<int>(sbCount.ToString(), parameter, CommandType.Text);
            return _list;
        }
    }
}