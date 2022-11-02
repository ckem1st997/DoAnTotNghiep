using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Share.BaseCore.Cache;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.GetAll.WareHouses;

namespace WareHouse.API.Application.Queries.GetAll
{
    public class VendorDropDownCommand : IRequest<IEnumerable<VendorDTO>>, ICacheableMediatrQuery
    {
        public bool Active { get; set; } = true;
        [BindNever]
        public bool BypassCache { get; set; }
        [BindNever]
        public string CacheKey { get; set;}
        [BindNever]
        public TimeSpan? SlidingExpiration { get;set; }
    }
    public class VendorDropDownCommandHandler : IRequestHandler<VendorDropDownCommand, IEnumerable<VendorDTO>>
    {
        private readonly IDapper _repository;

        public VendorDropDownCommandHandler(IDapper repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<VendorDTO>> Handle(VendorDropDownCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            const string sql = "select Id,Name from Vendor where Inactive =@active and OnDelete=0 ";
            var parameter = new DynamicParameters();
            parameter.Add("@active", request.Active ? 1 : 0);
            var getAll = await _repository.GetAllAync<VendorDTO>(sql, parameter, CommandType.Text);
            return getAll;
        }
    }
}