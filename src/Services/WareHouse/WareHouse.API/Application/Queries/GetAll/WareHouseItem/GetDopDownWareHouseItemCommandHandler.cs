using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WareHouse.API.Application.Cache;
using WareHouse.API.Application.Model;

namespace WareHouse.API.Application.Queries.GetAll.WareHouseItem
{
    public class GetDopDownWareHouseItemCommand: IRequest<IEnumerable<WareHouseItemDTO>>, ICacheableMediatrQuery
    {
        public bool Active { get; set; } = true;
        [BindNever]
        public bool BypassCache { get; set; }
        [BindNever]
        public string CacheKey { get; set;}
        [BindNever]
        public TimeSpan? SlidingExpiration { get;set; }
    }
    
    
    public class GetDopDownWareHouseItemCommandHandler: IRequestHandler<GetDopDownWareHouseItemCommand, IEnumerable<WareHouseItemDTO>>
    {
        private readonly IDapper _repository;

        public GetDopDownWareHouseItemCommandHandler(IDapper repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<WareHouseItemDTO>> Handle(GetDopDownWareHouseItemCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            const string sql = "select Id,CONCAT('[',Code,'] ',Name) as Name,UnitId from WareHouseItem where Inactive =@active and OnDelete=0 order by Name";
            var parameter = new DynamicParameters();
            parameter.Add("@active", request.Active ? 1 : 0);
            var getAll = await _repository.GetAllAync<WareHouseItemDTO>(sql, parameter, CommandType.Text);
            return getAll;
        }
    }
}