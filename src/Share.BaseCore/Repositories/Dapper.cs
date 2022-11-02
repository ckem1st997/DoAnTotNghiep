
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
using Share.BaseCore.IRepositories;
using Share.BaseCore;

namespace Share.BaseCore.Repositories
{
    public class Dapperr : IDapper
    {
        private readonly IConfiguration _config;
        private readonly string Connectionstring;

        public Dapperr(IConfiguration config, string connectionstring)
        {
            _config = config;
            Connectionstring = connectionstring;
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

        public async Task<IEnumerable<T>> GetListByListId<T>(IEnumerable<string> listId, string nameEntity, CommandType commandType)
        {

            using var connection = new SqlConnection(_config.GetConnectionString(Connectionstring));
            var sp = "select * from " + nameEntity + " where " + nameof(BaseEntity.Id) + " in @ids";
            DynamicParameters parameter = new();
            parameter.Add("@ids", listId);
            return await connection.QueryAsync<T>(sp, parameter, commandType: commandType);
        }
        public DbConnection GetDbconnection()
        {
            return new SqlConnection(_config.GetConnectionString(Connectionstring));
        }

        public async Task<int> CheckCode<T>(string code, string nameEntity)
        {
            using var connection = new SqlConnection(_config.GetConnectionString(Connectionstring));
            var sp = "select Id from " + nameEntity + " where Code = @code";
            DynamicParameters parameter = new();
            parameter.Add("@code", code);
            var res = await connection.QueryAsync<T>(sp, parameter, commandType: CommandType.Text);
            return res.Count();
        }

        /// <summary>
        /// Kiểm tra bộ khóa đã tồn tại trong bảng dữ liệu hay chưa
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="LstParams"></param>
        /// <param name="nameEntity"></param>
        /// <returns></returns>
        public async Task<int> CheckCode<T>(List<DapperParamsQueryCommand> LstParams, string nameEntity)
        {
            StringBuilder sBuider = new();
            if (LstParams.Count > 0)
            {
                sBuider.Append("WHERE ");
                foreach (var item in LstParams)
                {
                    if (string.IsNullOrEmpty(item.SqlOperator))
                    {
                        sBuider.Append(string.Format(" {0} [{1}] {2} {3}", item.SqlOperator, item.FieldName, item.Operator, item.ValueCompare));
                    }
                    else
                    {
                        sBuider.Append(string.Format(" [{0}] {1} {2}", item.FieldName, item.Operator, item.ValueCompare));
                    }
                }
            }
            using var connection = new SqlConnection(_config.GetConnectionString(Connectionstring));
            string sSQL = string.Format("SELECT TOP 1 * FROM {0} (NOLOCK) {1}", nameEntity, sBuider.ToString());
            var res = await connection.QueryAsync<T>(sSQL, commandType: CommandType.Text);
            return res.Count();
        }
    }
}