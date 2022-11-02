using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

namespace WareHouse.API.Application.Commands.Update
{
    public partial class UpdateOutwardDetailCommandHandler : IRequestHandler<UpdateOutwardDetailCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.OutwardDetail> _repository;
        private readonly IMapper _mapper;

        public UpdateOutwardDetailCommandHandler(IRepositoryEF<Domain.Entity.OutwardDetail> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateOutwardDetailCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var result = _mapper.Map<Domain.Entity.OutwardDetail>(request.OutwardDetailCommands);
            _repository.Update(result);
            return await _repository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}