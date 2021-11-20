using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WareHouse.API.Application.Interface;
using WareHouse.API.Application.Model;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Queries.Paginated.WareHouses
{
    public class PaginatedWareHouseCommandHandler : IRequestHandler<PaginatedWareHouseCommand, IPaginatedList<WareHouseDTO>>
    {

        private readonly IDapper _repository;
        private readonly IPaginatedList<WareHouseDTO> _list;

        public PaginatedWareHouseCommandHandler(IDapper repository, IPaginatedList<WareHouseDTO> list)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _list = list ?? throw new ArgumentNullException(nameof(list));

        }
        public async Task<IPaginatedList<WareHouseDTO>> Handle(PaginatedWareHouseCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                return null;
            request.KeySearch = request.KeySearch?.Trim();
            if (request.KeySearch == null)
                request.KeySearch = "";
            string sql = @"select * from WareHouse order by Name OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@key", '%' + request.KeySearch + '%');
            parameter.Add("@skip", (request.PageIndex - 1) * request.PageNumber);
            parameter.Add("@take", request.PageNumber);
            _list.Result = await _repository.GetList<WareHouseDTO>(sql, parameter, CommandType.Text);
            string count = @"select count(Id) from WareHouse";
            _list.totalCount = await _repository.GetAyncFirst<int>(count, parameter, CommandType.Text);
            return _list;
        }
    }
}
