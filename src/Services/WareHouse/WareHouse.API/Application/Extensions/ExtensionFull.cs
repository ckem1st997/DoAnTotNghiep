

using System;

namespace WareHouse.API.Application.Extensions
{
    public static class ExtensionFull
    {
        public static string GetDateToSqlRaw(DateTime? date)
        {
            return "" + date.Value.Year + "-" + date.Value.Month + "-" + date.Value.Day + "  00:00:00  ";
        }
    }
}
