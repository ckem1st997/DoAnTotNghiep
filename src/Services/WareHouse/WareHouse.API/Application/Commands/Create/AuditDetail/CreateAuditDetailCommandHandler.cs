﻿using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class UpdateAuditDetailCommandHandler: IRequestHandler<UpdateAuditDetailCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.AuditDetail> _repository;
        private readonly IMapper _mapper;

        public UpdateAuditDetailCommandHandler(IRepositoryEF<Domain.Entity.AuditDetail> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateAuditDetailCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var result = _mapper.Map<Domain.Entity.AuditDetail>(request.AuditDetailCommands);
            await _repository.AddAsync(result);
            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}