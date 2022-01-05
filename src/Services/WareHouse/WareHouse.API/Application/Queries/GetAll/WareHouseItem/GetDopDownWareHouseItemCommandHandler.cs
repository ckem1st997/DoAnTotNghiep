using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using WareHouse.API.Application.Model;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Queries.GetAll.WareHouseItem
{
    public class GetDopDownWareHouseItemCommand: IRequest<IEnumerable<WareHouseItemDTO>>
    {
        public bool Ative { get; set; } = true;
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
            const string sql = "select Id,CONCAT('[',Code,'] ',Name) as Name from WareHouseItem where Inactive =@active ";
            var parameter = new DynamicParameters();
            parameter.Add("@active", request.Ative ? 1 : 0);
            var getAll = await _repository.GetAllAync<WareHouseItemDTO>(sql, parameter, CommandType.Text);
            return getAll;
        }
    }
}