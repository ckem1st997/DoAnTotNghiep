using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WareHouse.API.Application.Commands.Models.WareHouse;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Commands.Update.WareHouses
{
    public class UpdateWareHouseCommandHandler : IRequestHandler<UpdateWareHouseCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.WareHouse> _repository;
        private readonly IMapper _mapper;

        public UpdateWareHouseCommandHandler(IRepositoryEF<Domain.Entity.WareHouse> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateWareHouseCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var mode = await _repository.GetFirstAsync(request.WareHouseCommands.Id);
            if (mode is null)
                return false;
            var result = _mapper.Map<Domain.Entity.WareHouse>(request.WareHouseCommands);
            _repository.Update(result);
            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}