using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using WareHouse.API.Application.Model;

namespace WareHouse.API.Application.Querie.CheckCode
{
    public class BeginningCheckItemAndWareHouseCommand : IRequest<bool>
    {
        public string ItemId { get; set; }
        public string WareHouseId { get; set; }
    }

    public class
        BeginningCheckItemAndWareHouseCommandHandler : IRequestHandler<BeginningCheckItemAndWareHouseCommand, bool>
    {
        private readonly IDapper _repository;

        public BeginningCheckItemAndWareHouseCommandHandler(IDapper repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<bool> Handle(BeginningCheckItemAndWareHouseCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            var sql = "select Id from BeginningWareHouse where ItemId=@key1 and WareHouseId=@key2";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@key1", request.ItemId);
            parameter.Add("@key2", request.WareHouseId);
            var res = await _repository.GetAyncFirst<BaseModel>(sql, parameter, CommandType.Text);
            return res != null;
        }
    }
}