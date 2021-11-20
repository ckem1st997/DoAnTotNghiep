using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class CreateInwardDetailCommandHandler: IRequestHandler<CreateInwardDetailCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.InwardDetail> _repository;
        private readonly IMapper _mapper;

        public CreateInwardDetailCommandHandler(IRepositoryEF<Domain.Entity.InwardDetail> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateInwardDetailCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var result = _mapper.Map<Domain.Entity.InwardDetail>(request.InwardDetailCommands);
            await _repository.AddAsync(result);
            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}