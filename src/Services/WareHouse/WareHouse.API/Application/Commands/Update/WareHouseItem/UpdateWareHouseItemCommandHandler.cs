using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Commands.Update
{
    public partial class UpdateWareHouseItemCommandHandler : IRequestHandler<UpdateWareHouseItemCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.WareHouseItem> _repository;
        private readonly IMapper _mapper;

        public UpdateWareHouseItemCommandHandler(IRepositoryEF<Domain.Entity.WareHouseItem> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateWareHouseItemCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var result = _mapper.Map<Domain.Entity.WareHouseItem>(request.WareHouseItemCommands);
            _repository.Update(result);
            return await _repository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0;

        }
    }
}