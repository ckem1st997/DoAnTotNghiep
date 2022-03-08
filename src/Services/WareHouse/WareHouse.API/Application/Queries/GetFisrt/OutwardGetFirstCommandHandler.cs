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

        public OutwardGetFirstCommandHandler(IRepositoryEF<Domain.Entity.Outward> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<OutwardDTO> Handle(OutwardGetFirstCommand request, CancellationToken cancellationToken)
        {
            if (request?.Id is null)
                return null;
            var res = await _repository.GetFirstAsyncAsNoTracking(request.Id);
            return _mapper.Map<OutwardDTO>(res);
        }
    }
}
