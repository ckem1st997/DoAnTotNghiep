using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WareHouse.API.Application.Model;
using WareHouse.Domain.Entity;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Queries.GetFisrt
{

    public class OutwardDetailsGetFirstCommand : Model.BaseModel, IRequest<OutwardDetailDTO>
    {
    }

    public class OutwardDetailsGetFirstCommandHandler : IRequestHandler<OutwardDetailsGetFirstCommand, OutwardDetailDTO>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryEF<Domain.Entity.OutwardDetail> _repositoryDetail;
        private readonly IRepositoryEF<Domain.Entity.SerialWareHouse> _repositorySeri;

        public OutwardDetailsGetFirstCommandHandler(IRepositoryEF<Domain.Entity.SerialWareHouse> repositorySeri, IRepositoryEF<Domain.Entity.OutwardDetail> repositoryDetail, IMapper mapper)
        {
            _repositoryDetail = repositoryDetail ?? throw new ArgumentNullException(nameof(repositoryDetail));
            _repositorySeri = repositorySeri ?? throw new ArgumentNullException(nameof(repositorySeri));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<OutwardDetailDTO> Handle(OutwardDetailsGetFirstCommand request, CancellationToken cancellationToken)
        {
            if (request?.Id is null)
                return null;
            var res = await _repositoryDetail.GetFirstAsyncAsNoTracking(request.Id);
            if (res != null)
                res.SerialWareHouses = (ICollection<SerialWareHouse>)await _repositorySeri.GetAync(x => x.OutwardDetailId.Equals(res.Id) && x.OnDelete==false);
            return _mapper.Map<OutwardDetailDTO>(res);

        }
    }
}