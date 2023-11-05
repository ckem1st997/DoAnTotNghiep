using AutoMapper;
using MediatR;
using Share.Base.Service;
using Share.Base.Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Delete
{
    public partial class DeleteWareHouseCommandHandler : IRequestHandler<DeleteWareHouseCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.WareHouse> _repository;
        private readonly IMapper _mapper;

        public DeleteWareHouseCommandHandler(IRepositoryEF<Domain.Entity.WareHouse> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteWareHouseCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            await _repository.DeteleSoftDelete(request.Id, cancellationToken);
            var res = await _repository.SaveChangesConfigureAwaitAsync(cancellationToken: cancellationToken);
            return res > 0;
        }
    }



    //

    public partial class CreateWareHouseCommand : IRequest<bool>
    {
        [DataMember] public WareHouseCommands WareHouseCommands { get; set; }

    }


    public partial class CreateWareHouseCommandHandler : IRequestHandler<CreateWareHouseCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.WareHouse> _repository;
        private readonly IMapper _mapper;

        public CreateWareHouseCommandHandler(IMapper mapper)
        {
            _repository = EngineContext.Current.Resolve<IRepositoryEF<Domain.Entity.WareHouse>>(DataConnectionHelper.ConnectionStringNames.Warehouse) ?? throw new ArgumentNullException(nameof(_repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateWareHouseCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;


            var res = await _repository.SaveChangesConfigureAwaitAsync(async () =>
            {

                var entity = _mapper.Map<Domain.Entity.WareHouse>(request.WareHouseCommands);
                await _repository.AddAsync(entity, cancellationToken);
                var res = await _repository.SaveChangesConfigureAwaitAsync(cancellationToken: cancellationToken);
                return res;
            }, cancellationToken: cancellationToken);
            return res > 0;
        }
    }
}