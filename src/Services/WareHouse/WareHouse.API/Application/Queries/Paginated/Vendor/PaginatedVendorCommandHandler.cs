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

namespace WareHouse.API.Application.Queries.Paginated.Vendor
{
    public class PaginatedVendorCommandHandler: IRequestHandler<PaginatedVendorCommand, IPaginatedList<VendorDTO>>
    {

        private readonly IDapper _repository;
        private readonly IPaginatedList<VendorDTO> _list;

        public PaginatedVendorCommandHandler(IDapper repository, IPaginatedList<VendorDTO> list)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _list = list ?? throw new ArgumentNullException(nameof(list));

        }
        public async Task<IPaginatedList<VendorDTO>> Handle(PaginatedVendorCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                return null;
            request.KeySearch = request.KeySearch?.Trim();
            if (request.KeySearch == null)
                request.KeySearch = "";
            StringBuilder sbCount = new StringBuilder();
            sbCount.Append("SELECT COUNT(*) FROM ( select * from Vendor  ");
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from Vendor ");
            sb.Append(" where Inactive =@active ");
            sbCount.Append(" where Inactive =@active ");
            if (!string.IsNullOrEmpty(request.KeySearch))
            {
                sb.Append(" and Code like @key or Name like @key or Phone like @key or Email like @key ");
                sbCount.Append(" and Code like @key or Name like @key or Phone like @key or Email like @key ");
            }
            sb.Append(" and OnDelete=0 ");
            sbCount.Append(" and OnDelete=0 ");
            sbCount.Append(" ) t   ");
            sb.Append(" order by Name OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY ");
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@key", '%' + request.KeySearch + '%');
            parameter.Add("@skip", request.Skip);
            parameter.Add("@take", request.Take);
            parameter.Add("@active", request.Active ? 1 : 0);
            _list.Result = await _repository.GetList<VendorDTO>(sb.ToString(), parameter, CommandType.Text);
            _list.totalCount = await _repository.GetAyncFirst<int>(sbCount.ToString(), parameter, CommandType.Text);
            return _list;
        }
    }
}
