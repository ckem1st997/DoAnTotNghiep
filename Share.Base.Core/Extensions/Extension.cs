
using System;

namespace Share.Base.Core.Extensions
{
    public static class Extension
    {
        public static bool IsNullOrEmpty(this string v)
        {
            return string.IsNullOrEmpty(v);
        }
        public static string GetDateToSqlRaw(DateTime? date)
        {
            return "" + date.Value.Year + "-" + date.Value.Month + "-" + date.Value.Day + "  00:00:00  ";
        }

        public static string GetDateToSqlRaw(int Year, int Mouth, int Day)
        {
            return "" + Year + "-" + Mouth + "-" + Day + "";
        }
        public static string GetVoucherCode(string name)
        {
            var date = DateTime.Now;
            return name + date.Year.ToString() + date.Month.ToString() + date.Day.ToString() + date.Hour.ToString() + date.Minute.ToString() + date.Second.ToString() + date.Millisecond.ToString();
        }
    }
}
