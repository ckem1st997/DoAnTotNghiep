using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using WareHouse.API.Application.Model;
using WareHouse.Domain.Entity;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Querie.CheckCode
{
    public class CheckItemAndWareHouseItemByOutWardCommand : IRequest<bool>
    {
        public string ItemId { get; set; }
        public string WareHouseId { get; set; }
    }

    public class
        CheckItemAndWareHouseItemByOutWardCommandHandler : IRequestHandler<CheckItemAndWareHouseItemByOutWardCommand, bool>
    {
        private readonly IRepositoryEF<BeginningWareHouse> _repository;
        private readonly IRepositoryEF<Inward> _repositoryInward;
        private readonly IRepositoryEF<InwardDetail> _repositoryInwardDetails;

        public CheckItemAndWareHouseItemByOutWardCommandHandler(IRepositoryEF<InwardDetail> repositoryInwardDetails,IRepositoryEF<BeginningWareHouse> repository, IRepositoryEF<Inward> repositoryInward)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _repositoryInward = repositoryInward;
            _repositoryInwardDetails = repositoryInwardDetails;
        }

        public async Task<bool> Handle(CheckItemAndWareHouseItemByOutWardCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrEmpty(request.ItemId) || string.IsNullOrEmpty(request.WareHouseId))
            {
                throw new ArgumentNullException(request.ItemId);
            }

            var beginning =await _repository.GetAync(x => x.WareHouseId.Equals(request.WareHouseId)&& x.ItemId.Equals(request.ItemId),records:1);

            if (beginning != null & beginning.AsList().Count>0)
            {
                return true;
            }
            else
            {
                var inward = await _repositoryInward.GetAync(x => x.WareHouseId.Equals(request.WareHouseId), records: 1);
                if (inward != null & inward.AsList().Count > 0)
                {
                    var inwardDetail =await _repositoryInwardDetails.GetAync(x => x.ItemId.Equals(request.ItemId), records: 1);
                    if (inwardDetail != null & inwardDetail.AsList().Count > 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}