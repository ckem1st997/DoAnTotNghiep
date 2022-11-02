using AutoMapper;
using Infrastructure;
using MediatR;
using Share.BaseCore.Extensions;
using Share.BaseCore;
using Share.BaseCore.IRepositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using WareHouse.API.Application.Model;
using WareHouse.Domain.Entity;

namespace WareHouse.API.Application.Queries.GetFisrt
{
    public class OutwardGetFirstCommand : Model.BaseModel, IRequest<OutwardDTO>
    {
    }
    public class OutwardGetFirstCommandHandler : IRequestHandler<OutwardGetFirstCommand, OutwardDTO>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryEF<Domain.Entity.Outward> _repository;
        private readonly IRepositoryEF<Domain.Entity.OutwardDetail> _repositoryDetail;
        private readonly IRepositoryEF<Domain.Entity.SerialWareHouse> _repositorySeri;

        public OutwardGetFirstCommandHandler(IMapper mapper)
        {
            _repository = EngineContext.Current.Resolve<IRepositoryEF<Outward>>(DataConnectionHelper.ConnectionStringNames.Warehouse);
            _repositoryDetail = EngineContext.Current.Resolve<IRepositoryEF<OutwardDetail>>(DataConnectionHelper.ConnectionStringNames.Warehouse);
            _repositorySeri = EngineContext.Current.Resolve<IRepositoryEF<SerialWareHouse>>(DataConnectionHelper.ConnectionStringNames.Warehouse);
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<OutwardDTO> Handle(OutwardGetFirstCommand request, CancellationToken cancellationToken)
        {
            if (request?.Id is null)
                return null;
            var res = await _repository.GetFirstAsyncAsNoTracking(request.Id);
            if (res != null)
            {
                res.OutwardDetails = (System.Collections.Generic.ICollection<Domain.Entity.OutwardDetail>)await _repositoryDetail.GetAync(x => x.OutwardId.Equals(request.Id) && x.OnDelete == false);
                if (res.OutwardDetails != null)
                {
                    foreach (var item in res.OutwardDetails)
                    {
                        item.SerialWareHouses = (System.Collections.Generic.ICollection<Domain.Entity.SerialWareHouse>)await _repositorySeri.GetAync(x => x.OutwardDetailId.Equals(item.Id) && x.OnDelete == false);

                    }
                }
            }
            return _mapper.Map<OutwardDTO>(res);
        }
    }
}
