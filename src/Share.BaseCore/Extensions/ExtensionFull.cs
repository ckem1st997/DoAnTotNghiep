using Share.BaseCore.IRepositories;
using System;

namespace Share.BaseCore.Extensions
{
    public static class ExtensionFull
    {
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


        public static IRepositoryEF<T> ResolveRepository<T>(string ConnectionStringNames) where T : BaseEntity
        {
            return EngineContext.Current.Resolve<IRepositoryEF<T>>(ConnectionStringNames);
        }
    }
}
