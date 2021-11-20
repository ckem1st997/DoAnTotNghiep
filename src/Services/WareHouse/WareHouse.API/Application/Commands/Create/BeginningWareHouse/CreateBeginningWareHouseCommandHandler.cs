﻿using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Commands.Create.Vendor
{
    public class CreateVendorCommandHandler: IRequestHandler<CreateVendorCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.Vendor> _repository;
        private readonly IMapper _mapper;

        public CreateVendorCommandHandler(IRepositoryEF<Domain.Entity.Vendor> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateVendorCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var result = _mapper.Map<Domain.Entity.Vendor>(request.VendorCommands);
            await _repository.AddAsync(result);
            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}