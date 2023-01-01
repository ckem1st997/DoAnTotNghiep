using AutoMapper;
using HotChocolate;
using HotChocolate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WareHouse.API.Application.Model;
using WareHouse.Domain.Entity;
using WareHouse.Infrastructure;

namespace WareHouse.API.Infrastructure.GraphQL
{
    public class Query
    {
        private readonly IRepositoryEF<Unit> _repositoryEF;
        private readonly IMapper _mapper;


        public Query(IRepositoryEF<Unit> repositoryEF, IMapper mapper)
        {
            _repositoryEF = repositoryEF;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Unit>> GetSuperheroes()
        {
            var list =await _repositoryEF.ListAllAsync();
            return list;
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Unit> GetUnits( WarehouseManagementContext context)
             => context.Units;
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IEnumerable<Unit> GetUnit()
        {
            var lisst=new List<Unit>();
            for (int i = 0; i < 100000; i++)
            {
                lisst.Add(new Unit()
                {
                    Id = i.ToString(),
                    UnitName = Guid.NewGuid().ToString(),
                });
            }
            return lisst;
        }
    }
}
