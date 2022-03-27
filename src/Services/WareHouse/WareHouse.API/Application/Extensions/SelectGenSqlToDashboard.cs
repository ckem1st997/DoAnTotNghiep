using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouse.API.Application.Queries.DashBoard;

namespace WareHouse.API.Application.Extensions
{
    public static class SoftStatic
    {
        public const string DESC = "desc";
        public const string ASC = "asc";
    }
    public static class SelectGenSqlToDashboard
    {


        public static StringBuilder SelectTopOutward(DashBoardSelectTopInwardCommand request)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("select top 5 count(OutwardDetail.ItemId) as Count,WareHouseItem.Name,WareHouseItem.Code,WareHouseItem.Id,sum(OutwardDetail.Quantity) as SumQuantity,Unit.UnitName, SUM(Price) as SumPrice ");
            sb.Append("from Outward inner join OutwardDetail on Outward.Id=OutwardDetail.OutwardId   ");
            sb.Append("inner join WareHouseItem on OutwardDetail.ItemId=WareHouseItem.Id ");
            sb.Append("inner join Unit on WareHouseItem.UnitId=Unit.Id ");
            sb.Append("where Outward.OnDelete=0 and OutwardDetail.OnDelete=0 and WareHouseItem.OnDelete=0 and Unit.OnDelete=0 ");
            if (request.searchByDay.HasValue)
                sb.Append("and Outward.VoucherDate=cast(@searchByDay as date) ");
            if (request.searchByMounth > 0 && request.searchByMounth <= 12)
                sb.Append("and MONTH(Outward.VoucherDate)=@searchByMounth ");
            if (request.searchByYear > 1999 && request.searchByYear <= 2050)
                sb.Append("and YEAR(Outward.VoucherDate)=@searchByYear ");
            sb.Append("group by WareHouseItem.Name,WareHouseItem.Code,WareHouseItem.Id,Unit.UnitName ");
            if (request.selectTopWareHouseBook.Equals(SelectTopWareHouseBook.Count))
                sb.Append("order by count(OutwardDetail.ItemId) @soft ");
            else if (request.selectTopWareHouseBook.Equals(SelectTopWareHouseBook.SumQuantity))
                sb.Append("order by sum(OutwardDetail.Quantity) @soft ");
            else if (request.selectTopWareHouseBook.Equals(SelectTopWareHouseBook.SumPrice))
                sb.Append("order by sum(Price) @soft ");
            return sb;
        }
    }
}
