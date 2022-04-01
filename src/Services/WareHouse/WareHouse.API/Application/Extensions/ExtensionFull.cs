

using System;

namespace WareHouse.API.Application.Extensions
{
    public static class ExtensionFull
    {
        public static string GetDateToSqlRaw(DateTime? date)
        {
            return "" + date.Value.Year + "-" + date.Value.Month + "-" + date.Value.Day + "  00:00:00  ";
        }

        public static string GetDateToSqlRaw(int Year, int Mouth, int Day)
        {
            return "" +Year+ "-" + Mouth + "-" + Day + "";
        }
    }
}
