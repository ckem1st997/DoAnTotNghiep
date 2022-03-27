using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using WareHouse.API.Application.Commands.Models;
using WareHouse.API.Application.Extensions;
using WareHouse.API.Application.Interface;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.BaseModel;
using WareHouse.Domain.IRepositories;


namespace WareHouse.API.Application.Queries.DashBoard
{
    public class DashBoardSelectTopTotalOutAndInCommand : BaseDashboardCommands, IRequest<IPaginatedList<DashBoardSelectTopInAndOut>>
    {
    }

    public class DashBoardSelectTopTotalOutAndInCommandHandler : IRequestHandler<DashBoardSelectTopTotalOutAndInCommand,
        IPaginatedList<DashBoardSelectTopInAndOut>>
    {
        private readonly IDapper _repository;
        private readonly IPaginatedList<DashBoardSelectTopInAndOut> _list;

        public DashBoardSelectTopTotalOutAndInCommandHandler(IDapper repository, IPaginatedList<DashBoardSelectTopInAndOut> list)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _list = list ?? throw new ArgumentNullException(nameof(list));
        }

        public async Task<IPaginatedList<DashBoardSelectTopInAndOut>> Handle(DashBoardSelectTopTotalOutAndInCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                return null;
            // BuildMyString.com generated code. Please enjoy your string responsibly.


            StringBuilder sb = new StringBuilder();

            sb.Append("select d1.Name,d1.Code,d1.Id,d1.UnitName,sum(d1.Count) as Count,sum(d1.SumPrice) as SumPrice,sum(d1.SumQuantity) as SumQuantity from ");
            sb.Append("(select top 10 count(InwardDetail.ItemId) as Count,WareHouseItem.Name,WareHouseItem.Code,WareHouseItem.Id,sum(InwardDetail.Quantity) as SumQuantity,Unit.UnitName,SUM(Price)as SumPrice ");
            sb.Append("from Inward inner join InwardDetail on Inward.Id=InwardDetail.InwardId  ");
            sb.Append("inner join WareHouseItem on InwardDetail.ItemId=WareHouseItem.Id ");
            sb.Append("inner join Unit on WareHouseItem.UnitId=Unit.Id ");
            sb.Append("where Inward.OnDelete=0 and InwardDetail.OnDelete=0 and WareHouseItem.OnDelete=0 and Unit.OnDelete=0 ");
            if (request.searchByDay.HasValue)
                sb.Append("and Inward.VoucherDate=cast(@searchByDay as date) ");
            if (request.searchByMounth > 0 && request.searchByMounth <= 12)
                sb.Append("and MONTH(Inward.VoucherDate)=@searchByMounth ");
            if (request.searchByYear > 1999 && request.searchByYear <= 2050)
                sb.Append("and YEAR(Inward.VoucherDate)=@searchByYear ");
            sb.Append("group by WareHouseItem.Name,WareHouseItem.Code,WareHouseItem.Id,Unit.UnitName ");
            sb.Append("union all ");
            sb.Append("select top 10 count(OutwardDetail.ItemId) as Count,WareHouseItem.Name,WareHouseItem.Code,WareHouseItem.Id,sum(OutwardDetail.Quantity) as SumQuantity,Unit.UnitName, SUM(Price) as SumPrice ");
            sb.Append("from Outward inner join OutwardDetail on Outward.Id=OutwardDetail.OutwardId  ");
            sb.Append("inner join WareHouseItem on OutwardDetail.ItemId=WareHouseItem.Id ");
            sb.Append("inner join Unit on WareHouseItem.UnitId=Unit.Id ");
            sb.Append("where Outward.OnDelete=0 and OutwardDetail.OnDelete=0 and WareHouseItem.OnDelete=0 and Unit.OnDelete=0 ");
            if (request.searchByDay.HasValue)
                sb.Append("and Outward.VoucherDate=cast(@searchByDay as date) ");
            if (request.searchByMounth > 0 && request.searchByMounth <= 12)
                sb.Append("and MONTH(Outward.VoucherDate)=@searchByMounth ");
            if (request.searchByYear > 1999 && request.searchByYear <= 2050)
                sb.Append("and YEAR(Outward.VoucherDate)=@searchByYear ");
            sb.Append("group by WareHouseItem.Name,WareHouseItem.Code,WareHouseItem.Id,Unit.UnitName) d1 ");
            sb.Append("group by d1.Name,d1.Code,d1.Id,d1.UnitName ");
            if (request.selectTopWareHouseBook.Equals(SelectTopWareHouseBook.Count))
                sb.Append("order by sum(d1.Count)  ");
            else if (request.selectTopWareHouseBook.Equals(SelectTopWareHouseBook.SumQuantity))
                sb.Append("order by sum(d1.SumQuantity)  ");
            else if (request.selectTopWareHouseBook.Equals(SelectTopWareHouseBook.SumPrice))
                sb.Append("order by sum(d1.SumPrice)  ");
            if (request.order == "desc")
                sb.Append("desc ");
            else if (request.order == "asc")
                sb.Append("asc ");

            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@searchByDay", request.searchByDay);
            parameter.Add("@searchByMounth", request.searchByMounth);
            parameter.Add("@searchByYear", request.searchByYear);
            _list.Result = await _repository.GetList<DashBoardSelectTopInAndOut>(sb.ToString(), parameter, CommandType.Text);
            _list.totalCount = _list.Result.Count();
            return _list;
        }
    }
}