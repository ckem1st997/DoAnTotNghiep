using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouse.Domain.IRepositories
{
    public interface IDapper : IDisposable
    {
        DbConnection GetDbconnection();

        Task<T> GetAyncFirst<T>(string sp, DynamicParameters parms,
            CommandType commandType = CommandType.StoredProcedure);

        Task<IEnumerable<T>> GetList<T>(string sp, DynamicParameters parms,
            CommandType commandType = CommandType.StoredProcedure);

        Task<IEnumerable<T>> GetAllAync<T>(string sp, DynamicParameters parms,
            CommandType commandType = CommandType.StoredProcedure);

        Task<IEnumerable<T>> GetListByListId<T>(IEnumerable<string> listId, string nameEntity, CommandType commandType);

        Task<IEnumerable<T>> CheckCode<T>(string code, string nameEntity);
    }
}