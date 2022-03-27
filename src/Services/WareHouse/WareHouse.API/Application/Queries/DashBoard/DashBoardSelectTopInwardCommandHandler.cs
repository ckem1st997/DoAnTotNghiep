using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using WareHouse.API.Application.Extensions;
using WareHouse.API.Application.Interface;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.BaseModel;
using WareHouse.Domain.IRepositories;


namespace WareHouse.API.Application.Queries.DashBoard
{
    public class DashBoardSelectTopInwardCommand : IRequest<IPaginatedList<DashBoardSelectTopInAndOut>>
    {
        public DateTime? searchByDay { get; set; }
        public int searchByMounth { get; set; }
        public int searchByYear { get; set; }
        public string order { get; set; }

        public SelectTopWareHouseBook? selectTopWareHouseBook { get; set; }
    }

    public class DashBoardSelectTopInwardCommandHandler : IRequestHandler<DashBoardSelectTopInwardCommand,
        IPaginatedList<DashBoardSelectTopInAndOut>>
    {
        private readonly IDapper _repository;
        private readonly IPaginatedList<DashBoardSelectTopInAndOut> _list;

        public DashBoardSelectTopInwardCommandHandler(IDapper repository, IPaginatedList<DashBoardSelectTopInAndOut> list)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _list = list ?? throw new ArgumentNullException(nameof(list));
        }

        public async Task<IPaginatedList<DashBoardSelectTopInAndOut>> Handle(DashBoardSelectTopInwardCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                return null;
            // BuildMyString.com generated code. Please enjoy your string responsibly.

            StringBuilder sb = SelectGenSqlToDashboard.SelectTopInward(request);

            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@searchByDay", request.searchByDay);
            parameter.Add("@searchByMounth", request.searchByMounth);
            parameter.Add("@searchByYear", request.searchByYear);
            parameter.Add("@soft", request.order);
            _list.Result = await _repository.GetList<DashBoardSelectTopInAndOut>(sb.ToString(), parameter, CommandType.Text);
            _list.totalCount = _list.Result.Count();
            return _list;
        }


    }
}