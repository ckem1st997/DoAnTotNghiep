﻿
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

namespace Master.Extension
{
    public class Dapperr : IDapper
    {
        private readonly IConfiguration _config;
        private string Connectionstring = "MasterdataContext";

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
        public DbConnection GetDbconnection()
        {
            return new SqlConnection(_config.GetConnectionString(Connectionstring));
        }

        public Task<IEnumerable<T>> GetListByListId<T>(IEnumerable<string> listId, string nameEntity, CommandType commandType)
        {
            throw new NotImplementedException();
        }

        public Task<int> CheckCode<T>(string code, string nameEntity)
        {
            throw new NotImplementedException();
        }
    }
}