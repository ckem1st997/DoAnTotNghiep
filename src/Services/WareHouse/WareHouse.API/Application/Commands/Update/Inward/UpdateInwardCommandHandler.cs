using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Commands.Update
{
    public partial class UpdateInwardCommandHandler : IRequestHandler<UpdateInwardCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.Inward> _repository;
        private readonly IMapper _mapper;

        public UpdateInwardCommandHandler(IRepositoryEF<Domain.Entity.Inward> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateInwardCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var result = _mapper.Map<Domain.Entity.Inward>(request.InwardCommands);
            _repository.Update(result);
            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}