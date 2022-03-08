using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WareHouse.API.Application.Model;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Queries.GetFisrt
{
    public class InwardGetFirstCommand : Model.BaseModel, IRequest<InwardDTO>
    {
    }

    public class InwardGetFirstCommandHandler : IRequestHandler<InwardGetFirstCommand, InwardDTO>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryEF<Domain.Entity.Inward> _repository;

        public InwardGetFirstCommandHandler(IRepositoryEF<Domain.Entity.Inward> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<InwardDTO> Handle(InwardGetFirstCommand request, CancellationToken cancellationToken)
        {
            if (request?.Id is null)
                return null;
            var res = await _repository.GetFirstAsyncAsNoTracking(request.Id);
            return _mapper.Map<InwardDTO>(res);
        }
    }
}
