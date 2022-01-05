
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouse.Domain.Entity;
using WareHouse.Domain.IRepositories;

namespace WareHouse.Infrastructure.Repositories
{
    public class Dapperr : IDapper
    {
        private readonly IConfiguration _config;
        private string Connectionstring = "WarehouseManagementContext";

        public Dapperr(IConfiguration config)
        {
            _config = config;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


        public async Task<T> GetAyncFirst<T>(string sp, DynamicParameters parms, CommandType commandType)
        {
            using var connection = new SqlConnection(_config.GetConnectionString(Connectionstring));
            return await connection.QueryFirstOrDefaultAsync<T>(sp, parms, commandType: commandType);
        }


        public async Task<IEnumerable<T>> GetAllAync<T>(string sp, DynamicParameters parms, CommandType commandType)
        {
            using var connection = new SqlConnection(_config.GetConnectionString(Connectionstring));
            return await connection.QueryAsync<T>(sp, parms, commandType: commandType);
        }
        public async Task<IEnumerable<T>> GetList<T>(string sp, DynamicParameters parms, CommandType commandType)
        {
            using var connection = new SqlConnection(_config.GetConnectionString(Connectionstring));
            return await connection.QueryAsync<T>(sp, parms, commandType: commandType);
        }
        
        public async Task<IEnumerable<T>> GetListByListId<T>(IEnumerable<string> listId,string nameEntity, CommandType commandType)
        {
            using var connection = new SqlConnection(_config.GetConnectionString(Connectionstring));
            var sp = "select * from " + nameEntity + " where " + nameof(BaseEntity.Id) + " in @ids";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@ids", listId);
            return await connection.QueryAsync<T>(sp, parameter, commandType: commandType);
        }
        public DbConnection GetDbconnection()
        {
            return new SqlConnection(_config.GetConnectionString(Connectionstring));
        }


    }
}