using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WareHouse.API.Application.Commands.Models;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class CreateOrUpdateWareHouseItemUnitCommand : IRequest<bool>
    {
        [DataMember]
        public IEnumerable<WareHouseItemUnitCommands> wareHouseItemUnitCommands { get; set; }
    }
    public partial class CreateOrUpdateWareHouseItemUnitCommandHandler : IRequestHandler<CreateOrUpdateWareHouseItemUnitCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.WareHouseItemUnit> _repository;
        private readonly IMapper _mapper;

        public CreateOrUpdateWareHouseItemUnitCommandHandler(IRepositoryEF<Domain.Entity.WareHouseItemUnit> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateOrUpdateWareHouseItemUnitCommand request, CancellationToken cancellationToken)
        {

            if (request is null || request.wareHouseItemUnitCommands is null)
                return false;
            var list = new List<Domain.Entity.WareHouseItemUnit>();
            foreach (var item in request.wareHouseItemUnitCommands)
            {
                var result = _mapper.Map<Domain.Entity.WareHouseItemUnit>(item);
                list.Add(result);
            }
            await _repository.BulkInsertOrUpdateAsync(list);
            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}