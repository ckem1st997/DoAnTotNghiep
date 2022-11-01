using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WareHouse.Domain.Entity;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class CreateSerialWareHouseCommandHandler : IRequestHandler<CreateSerialWareHouseCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.SerialWareHouse> _repository;
        private readonly IMapper _mapper;

        public CreateSerialWareHouseCommandHandler(IRepositoryEF<Domain.Entity.SerialWareHouse> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateSerialWareHouseCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var list = new List<SerialWareHouse>();
            foreach (var item in request.SerialWareHouseCommands)
            {
                var serialWareHouse = _mapper.Map<SerialWareHouse>(item);
                await _repository.AddAsync(serialWareHouse);
            }
            return await _repository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}