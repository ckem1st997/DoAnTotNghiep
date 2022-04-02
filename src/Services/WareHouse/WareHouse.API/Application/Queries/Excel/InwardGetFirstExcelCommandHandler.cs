using AutoMapper;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using WareHouse.API.Application.Model;
using WareHouse.Domain.Entity;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Queries.GetFisrt
{
    public class InwardGetFirstExcelCommand : Model.BaseModel, IRequest<InwardDTO>
    {
    }
    public class InwardGetFirstExcelCommandHandler : IRequestHandler<InwardGetFirstExcelCommand, InwardDTO>
    {
        private readonly IMapper _mapper;
        private readonly IDapper _dapper;
        private readonly IRepositoryEF<Domain.Entity.Inward> _repository;
        private readonly IRepositoryEF<Domain.Entity.WareHouse> _repositoryDetail;
        private readonly IRepositoryEF<Domain.Entity.InwardDetail> _repositoryDetaild;

        public InwardGetFirstExcelCommandHandler(IDapper dapper, IRepositoryEF<Domain.Entity.InwardDetail> repositoryDetaild, IRepositoryEF<Domain.Entity.WareHouse> repositoryDetail, IRepositoryEF<Domain.Entity.Inward> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repositoryDetaild = repositoryDetaild;
            _repositoryDetail = repositoryDetail;
            _dapper = dapper;
        }
        public async Task<InwardDTO> Handle(InwardGetFirstExcelCommand request, CancellationToken cancellationToken)
        {
            if (request?.Id is null)
                return null;
            var res = await _repository.GetFirstAsyncAsNoTracking(request.Id);
            if (res != null)
            {
                res.WareHouse = await _repositoryDetail.GetFirstAsyncAsNoTracking(res.WareHouseId);
            }
            var result = _mapper.Map<InwardDTO>(res);
            if (result != null)
            {
                var sql = "select InwardDetail.*,Unit.UnitName, WareHouseItem.Name as ItemName from InwardDetail inner join WareHouseItem on InwardDetail.ItemId=WareHouseItem.Id inner join Unit on Unit.Id=WareHouseItem.UnitId where InwardDetail.InwardId=@key and InwardDetail.OnDelete=0";
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@key", request.Id);
                result.InwardDetails = (ICollection<InwardDetailDTO>)await _dapper.GetAllAync<InwardDetailDTO>(sql, parameter, CommandType.Text);
            }

            return result;
        }
    }
}
