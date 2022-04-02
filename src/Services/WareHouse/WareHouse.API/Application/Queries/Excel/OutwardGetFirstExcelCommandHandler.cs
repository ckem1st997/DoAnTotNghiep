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
    public class OutwardGetFirstExcelCommand : Model.BaseModel, IRequest<OutwardDTO>
    {
    }
    public class OutwardGetFirstExcelCommandHandler : IRequestHandler<OutwardGetFirstExcelCommand, OutwardDTO>
    {
        private readonly IMapper _mapper;
        private readonly IDapper _dapper;
        private readonly IRepositoryEF<Domain.Entity.Outward> _repository;
        private readonly IRepositoryEF<Domain.Entity.WareHouse> _repositoryDetail;
        private readonly IRepositoryEF<Domain.Entity.OutwardDetail> _repositoryDetaild;

        public OutwardGetFirstExcelCommandHandler(IDapper dapper,IRepositoryEF<Domain.Entity.OutwardDetail> repositoryDetaild,IRepositoryEF<Domain.Entity.WareHouse> repositoryDetail, IRepositoryEF<Domain.Entity.Outward> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repositoryDetaild = repositoryDetaild;
            _repositoryDetail = repositoryDetail;
            _dapper = dapper;
        }
        public async Task<OutwardDTO> Handle(OutwardGetFirstExcelCommand request, CancellationToken cancellationToken)
        {
            if (request?.Id is null)
                return null;
            var res = await _repository.GetFirstAsyncAsNoTracking(request.Id);
            if (res != null)
            {
                res.WareHouse =await _repositoryDetail.GetFirstAsyncAsNoTracking(res.WareHouseId);
                res.WareHouse =await _repositoryDetail.GetFirstAsyncAsNoTracking(res.ToWareHouseId);
               
            }
            var result= _mapper.Map<OutwardDTO>(res);
            if(result !=null)
            {
                var sql = "select OutwardDetail.*,Unit.UnitName, WareHouseItem.Name as ItemName, WareHouseItem.Code from OutwardDetail inner join WareHouseItem on OutwardDetail.ItemId=WareHouseItem.Id inner join Unit on Unit.Id=WareHouseItem.UnitId where OutwardDetail.OutwardId=@key and OutwardDetail.OnDelete=0";
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@key", request.Id);
                result.OutwardDetails = (ICollection<OutwardDetailDTO>)await _dapper.GetAllAync<OutwardDetailDTO>(sql, parameter, CommandType.Text);
            }

            return result;
        }
    }
}
