using AutoMapper;
using MediatR;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using WareHouse.API.Application.Model;
using WareHouse.Domain.IRepositories;
using Dapper;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace WareHouse.API.Application.Queries.GetFisrt
{
    public class InwardGetFirstCommand : Model.BaseModel, IRequest<InwardDTO>
    {
    }

    public class InwardGetFirstCommandHandler : IRequestHandler<InwardGetFirstCommand, InwardDTO>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryEF<Domain.Entity.Inward> _repository;
        private readonly IRepositoryEF<Domain.Entity.InwardDetail> _repositoryDetail;
        private readonly IRepositoryEF<Domain.Entity.SerialWareHouse> _repositorySeri;

        public InwardGetFirstCommandHandler(IRepositoryEF<Domain.Entity.SerialWareHouse> repositorySeri, IRepositoryEF<Domain.Entity.InwardDetail> repositoryDetail, IRepositoryEF<Domain.Entity.Inward> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _repositoryDetail = repositoryDetail ?? throw new ArgumentNullException(nameof(repositoryDetail));
            _repositorySeri = repositorySeri ?? throw new ArgumentNullException(nameof(repositorySeri));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<InwardDTO> Handle(InwardGetFirstCommand request, CancellationToken cancellationToken)
        {
            if (request?.Id is null)
                return null;
            var res = await _repository.GetFirstAsyncAsNoTracking(request.Id);
            if (res != null)
            {
                res.InwardDetails = (System.Collections.Generic.ICollection<Domain.Entity.InwardDetail>)await _repositoryDetail.GetAync(x => x.InwardId.Equals(request.Id));
                if (res.InwardDetails != null)
                {
                    foreach (var item in res.InwardDetails)
                    {
                        item.SerialWareHouses = (System.Collections.Generic.ICollection<Domain.Entity.SerialWareHouse>)await _repositorySeri.GetAync(x => x.InwardDetailId.Equals(item.Id));

                    }
                }
            }
            return _mapper.Map<InwardDTO>(res);

        }
    }
}
