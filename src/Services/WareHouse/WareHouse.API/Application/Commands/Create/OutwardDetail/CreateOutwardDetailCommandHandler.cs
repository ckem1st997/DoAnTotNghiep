using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class CreateOutwardDetailCommandHandler: IRequestHandler<CreateOutwardDetailCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.OutwardDetail> _repository;
        private readonly IMapper _mapper;

        public CreateOutwardDetailCommandHandler(IRepositoryEF<Domain.Entity.OutwardDetail> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateOutwardDetailCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var result = _mapper.Map<Domain.Entity.OutwardDetail>(request.OutwardDetailCommands);
            await _repository.AddAsync(result);
            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}