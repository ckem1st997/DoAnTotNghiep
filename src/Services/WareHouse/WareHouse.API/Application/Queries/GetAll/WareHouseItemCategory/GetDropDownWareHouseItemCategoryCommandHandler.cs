using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using WareHouse.API.Application.Model;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Queries.GetAll.WareHouseItemCategory
{
    public class GetDropDownWareHouseItemCategoryCommandHandler : IRequestHandler<
        GetDropDownWareHouseItemCategoryCommand, IEnumerable<WareHouseItemCategoryDTO>>
    {
        private readonly IDapper _repository;

        public GetDropDownWareHouseItemCategoryCommandHandler(IDapper repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<WareHouseItemCategoryDTO>> Handle(GetDropDownWareHouseItemCategoryCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            const string sql = "select Id,CONCAT('[',Code,'] ',Name) as Name from WareHouseItemCategory where Inactive =@active and OnDelete=0 ";
            var parameter = new DynamicParameters();
            parameter.Add("@active", request.Ative ? 1 : 0);
            var getAll = await _repository.GetAllAync<WareHouseItemCategoryDTO>(sql, parameter, CommandType.Text);
            return getAll;
        }
    }
}