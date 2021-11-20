using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class UpdateWareHouseItemCategoryCommandHandler: IRequestHandler<UpdateWareHouseItemCategoryCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.WareHouseItemCategory> _repository;
        private readonly IMapper _mapper;

        public UpdateWareHouseItemCategoryCommandHandler(IRepositoryEF<Domain.Entity.WareHouseItemCategory> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateWareHouseItemCategoryCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var result = _mapper.Map<Domain.Entity.WareHouseItemCategory>(request.WareHouseItemCategoryCommands);
            await _repository.AddAsync(result);
            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}