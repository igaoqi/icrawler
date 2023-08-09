using System.Data;
using Crawler.Domain.Options;
using Crawler.Domain.Repository;
using Dapper;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace Crawler.Infrastructure.Repositories.BaseRepository
{
    public class MysqlRepository : ICmdRepository
    {
        private readonly MysqlConfig _options;

        public MysqlRepository(IOptionsSnapshot<MysqlConfig> optionsSnapshot)
        {
            _options = optionsSnapshot.Value;
        }

        private IDbConnection GetDbConnection(string connection = "")
        {
            if (string.IsNullOrEmpty(connection))
            {
                connection = _options.ConnectionString;
            }

            return new MySqlConnection(connection);
        }

        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using var conn = GetDbConnection();
            return await conn.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using var conn = GetDbConnection();

            return await conn.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using var conn = GetDbConnection();
            return await conn.QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public async Task<T> ExecuteScalarAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using var conn = GetDbConnection();
            return await conn.ExecuteScalarAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public async Task ExecuteTransactionAsync(List<SqlCommand> commands, int? commandTimeout = null, CommandType? commandType = null)
        {
            using var conn = GetDbConnection();
            using var transaction = conn.BeginTransaction();

            try
            {
                foreach (var command in commands)
                {
                    await conn.ExecuteAsync(command.Sql, command.Param, transaction, commandTimeout, commandType);
                }

                transaction.Commit();
            }
            catch (MySqlException)
            {
                transaction.Rollback();
                throw;
            }
        }

        public Task SetupAsync()
        {
            throw new NotImplementedException();
        }
    }
}