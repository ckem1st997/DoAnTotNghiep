using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WareHouse.API.Application.Model;
using WareHouse.Domain.IRepositories;

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

        public OutwardGetFirstCommandHandler(IRepositoryEF<Domain.Entity.SerialWareHouse> repositorySeri, IRepositoryEF<Domain.Entity.OutwardDetail> repositoryDetail, IRepositoryEF<Domain.Entity.Outward> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _repositoryDetail = repositoryDetail ?? throw new ArgumentNullException(nameof(repositoryDetail));
            _repositorySeri = repositorySeri ?? throw new ArgumentNullException(nameof(repositorySeri));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<OutwardDTO> Handle(OutwardGetFirstCommand request, CancellationToken cancellationToken)
        {
            if (request?.Id is null)
                return null;
            var res = await _repository.GetFirstAsyncAsNoTracking(request.Id);
            if (res != null)
            {
                res.OutwardDetails = (System.Collections.Generic.ICollection<Domain.Entity.OutwardDetail>)await _repositoryDetail.GetAync(x => x.OutwardId.Equals(request.Id));
                if (res.OutwardDetails != null)
                {
                    foreach (var item in res.OutwardDetails)
                    {
                        item.SerialWareHouses = (System.Collections.Generic.ICollection<Domain.Entity.SerialWareHouse>)await _repositorySeri.GetAync(x => x.OutwardDetailId.Equals(item.Id));

                    }
                }
            }
            return _mapper.Map<OutwardDTO>(res);
        }
    }
}
