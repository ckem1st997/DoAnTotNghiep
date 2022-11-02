using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WareHouse.API.Application.Authentication;
using WareHouse.API.Application.Cache;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.BaseModel;

namespace WareHouse.API.Application.Queries.GetAll.WareHouses
{
    public class GetAllWareHouseCommand : IRequest<IEnumerable<WareHouseDTO>>, ICacheableMediatrQuery
    {
        public bool Active { get; set; }
        [BindNever]
        public bool BypassCache { get; set; }
        [BindNever]
        public string CacheKey { get; set; }
        [BindNever]
        public TimeSpan? SlidingExpiration { get; set; }
    }

    public class
        GetAllWareHouseCommandHandler : IRequestHandler<GetAllWareHouseCommand, IEnumerable<WareHouseDTO>>
    {
        private readonly IDapper _repository;

        public GetAllWareHouseCommandHandler(IDapper repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<WareHouseDTO>> Handle(GetAllWareHouseCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                return null;
            string sql = "select Id,ParentId,Code,Name,Path from WareHouse where Inactive =@active and OnDelete=0 ";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@active", request.Active ? 1 : 0);
            var models = await _repository.GetAllAync<WareHouseDTO>(sql, parameter, CommandType.Text);
            return models;
        }
    }
}
