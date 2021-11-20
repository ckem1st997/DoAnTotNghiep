using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Commands.Delete
{
    public partial class DeleteSerialWareHouseCommandHandler : IRequestHandler<DeleteSerialWareHouseCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.SerialWareHouse> _repository;
        private readonly IMapper _mapper;

        public DeleteSerialWareHouseCommandHandler(IRepositoryEF<Domain.Entity.SerialWareHouse> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteSerialWareHouseCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var mode = await _repository.GetFirstAsyncAsNoTracking(request.Id);
            if (mode is null)
                return false;
            _repository.Delete(mode);
            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}