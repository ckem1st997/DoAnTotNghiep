using AutoMapper;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.GetAll.WareHouseItemCategory;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Queries.GetFisrt.WareHouses
{
    public class SearchWareHouseItemByCategoryCommand : IRequest<IEnumerable<WareHouseItemCategoryDTO>>
    {
        public string Key { get; set; }
        public string Id { get; set; }
    }
    public class SearchWareHouseItemByCategoryCommandHandler : IRequestHandler<SearchWareHouseItemByCategoryCommand, IEnumerable<WareHouseItemCategoryDTO>>
    {
        private readonly IDapper _dapper;
        private readonly IRepositoryEF<Domain.Entity.WareHouse> _repository;
        private readonly IMediator _mediat;

        public SearchWareHouseItemByCategoryCommandHandler(IRepositoryEF<Domain.Entity.WareHouse> repository, IMediator mediat, IDapper dapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mediat = mediat;
            _dapper = dapper;
        }

        public async Task<IEnumerable<WareHouseItemCategoryDTO>> Handle(SearchWareHouseItemByCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request?.Id is null)
                return null;

            var sql = "select WareHouseItem.* from WareHouseItem inner join WareHouseItemCategory on WareHouseItem.CategoryID=WareHouseItemCategory.Id where WareHouseItem.Name like '%%' or WareHouseItemCategory.Id = ''";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@key", '%' + request.Key + '%');
            parameter.Add("@skip", request.Id);
            var list = await _dapper.GetList<WareHouseItemCategoryDTO>(sql, parameter, CommandType.Text);

            return list;
        }
    }
}