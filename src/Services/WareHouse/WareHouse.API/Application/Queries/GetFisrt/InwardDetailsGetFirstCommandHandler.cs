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

    public class InwardDetailsGetFirstCommand : Model.BaseModel, IRequest<InwardDetailDTO>
    {
    }

    public class InwardDetailsGetFirstCommandHandler : IRequestHandler<InwardDetailsGetFirstCommand, InwardDetailDTO>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryEF<Domain.Entity.InwardDetail> _repositoryDetail;
        private readonly IRepositoryEF<Domain.Entity.SerialWareHouse> _repositorySeri;

        public InwardDetailsGetFirstCommandHandler(IRepositoryEF<Domain.Entity.SerialWareHouse> repositorySeri, IRepositoryEF<Domain.Entity.InwardDetail> repositoryDetail, IMapper mapper)
        {
            _repositoryDetail = repositoryDetail ?? throw new ArgumentNullException(nameof(repositoryDetail));
            _repositorySeri = repositorySeri ?? throw new ArgumentNullException(nameof(repositorySeri));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<InwardDetailDTO> Handle(InwardDetailsGetFirstCommand request, CancellationToken cancellationToken)
        {
            if (request?.Id is null)
                return null;
            var res = await _repositoryDetail.GetFirstAsyncAsNoTracking(request.Id);
            if (res != null)
                res.SerialWareHouses = (ICollection<SerialWareHouse>)await _repositorySeri.GetAync(x => x.InwardDetailId.Equals(res.Id) && x.OnDelete==false);
            return _mapper.Map<InwardDetailDTO>(res);

        }
    }
}