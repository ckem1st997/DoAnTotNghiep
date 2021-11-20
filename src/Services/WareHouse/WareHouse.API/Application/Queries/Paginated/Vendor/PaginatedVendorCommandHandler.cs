using System;
using System.Data;
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
            string sql = @"select * from Vendor order by Name OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@key", '%' + request.KeySearch + '%');
            parameter.Add("@skip", (request.PageIndex - 1) * request.PageNumber);
            parameter.Add("@take", request.PageNumber);
            _list.Result = await _repository.GetList<VendorDTO>(sql, parameter, CommandType.Text);
            string count = @"select count(Id) from Vendor";
            _list.totalCount = await _repository.GetAyncFirst<int>(count, parameter, CommandType.Text);
            return _list;
        }
    }
}
