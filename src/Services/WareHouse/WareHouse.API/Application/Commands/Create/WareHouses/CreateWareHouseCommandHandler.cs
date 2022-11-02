using AutoMapper;
using Share.BaseCore.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class CreateWareHouseCommandHandler : IRequestHandler<CreateWareHouseCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.WareHouse> _repository;
        private readonly IMapper _mapper;

        public CreateWareHouseCommandHandler(IRepositoryEF<Domain.Entity.WareHouse> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateWareHouseCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var result = _mapper.Map<Domain.Entity.WareHouse>(request.WareHouseCommands);
            await _repository.AddAsync(result);
            return await _repository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0;



            // insert hàng loạt
            //if (request is null)
            //    return false;
            //var result = _mapper.Map<Domain.Entity.WareHouse>(request.WareHouseCommands);
            //var list = new List<Domain.Entity.WareHouse>();
            //list.Add(result);
            //await _repository.BulkInsertAsync(list);
            //return true;
        }
    }
}