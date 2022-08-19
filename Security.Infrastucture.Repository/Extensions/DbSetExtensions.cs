using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Security.Infrastucture.Repository.Extensions
{
    public static class DbSetExtensions
    {
        public static async Task<IEnumerable<T>> FromSqlQueryAsync<T>(this DbContext dbContext,
            string sqlQuery, object parameters = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null)
        {
            commandType = CommandType.StoredProcedure;
            var connection = dbContext.Database.GetDbConnection();
            return await connection.QueryAsync<T>(sqlQuery, parameters, transaction, commandTimeout, commandType);
        }

        public static async Task<string> StringResultProcedure(this DbContext dbContext,
            string sqlQuery, object parameters = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null)
        {
            commandType = CommandType.StoredProcedure;
            var connection = dbContext.Database.GetDbConnection();

            return await connection.QueryFirstAsync<string>(sqlQuery, parameters, transaction, commandTimeout,
                commandType);
        }

        public static async Task ExecuteNonQueryAsync(this DbContext dbContext, string sqlQuery,
            object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var connection = dbContext.Database.GetDbConnection();

            await connection.ExecuteAsync(sqlQuery, parameters, transaction, commandTimeout, CommandType.Text);
        }
    }
}
