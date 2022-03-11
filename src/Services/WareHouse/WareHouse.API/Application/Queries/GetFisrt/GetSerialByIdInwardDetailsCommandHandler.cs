using AutoMapper;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WareHouse.API.Application.Model;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Queries.GetFisrt
{
    public class GetSerialByIdInwardDetailsCommand : Model.BaseModel, IRequest<IEnumerable<SerialWareHouseDTO>>
    {
    }

    public class GetSerialByIdInwardDetailsCommandHandler : IRequestHandler<GetSerialByIdInwardDetailsCommand, IEnumerable<SerialWareHouseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IDapper _dapper;

        public GetSerialByIdInwardDetailsCommandHandler(IDapper dapper, IRepositoryEF<Domain.Entity.SerialWareHouse> repositorySeri, IRepositoryEF<Domain.Entity.InwardDetail> repositoryDetail, IRepositoryEF<Domain.Entity.Inward> repository, IMapper mapper)
        {
            _dapper = dapper;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<SerialWareHouseDTO>> Handle(GetSerialByIdInwardDetailsCommand request, CancellationToken cancellationToken)
        {
            if (request?.Id is null)
                return null;
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM SerialWareHouse WHERE InwardDetailId = @Id and OnDelete=0 ");
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", request.Id);
            var res = await _dapper.GetList<SerialWareHouseDTO>(sb.ToString(), parameter, CommandType.Text);
            return res;

        }
    }
}
