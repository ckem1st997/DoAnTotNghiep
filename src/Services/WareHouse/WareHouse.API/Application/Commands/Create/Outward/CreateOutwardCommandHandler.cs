using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class CreateOutwardCommandHandler: IRequestHandler<CreateOutwardCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.Outward> _repository;
        private readonly IMapper _mapper;

        public CreateOutwardCommandHandler(IRepositoryEF<Domain.Entity.Outward> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateOutwardCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var result = _mapper.Map<Domain.Entity.Outward>(request.OutwardCommands);
            await _repository.AddAsync(result);
            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}