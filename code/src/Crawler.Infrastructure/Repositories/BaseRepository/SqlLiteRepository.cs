using System.Data;
using Crawler.Domain.Options;
using Crawler.Domain.Repository;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace Crawler.Infrastructure.Repositories.BaseRepository
{
    public class SqlLiteRepository : ICmdRepository
    {
        private readonly SqliteConnection _connection;

        public SqlLiteRepository(IOptionsSnapshot<MysqlConfig> optionsSnapshot)
        {
            _connection = new SqliteConnection(optionsSnapshot.Value.ConnectionString);
        }

        public Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
        }

        public Task<T> ExecuteScalarAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.ExecuteScalarAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public async Task ExecuteTransactionAsync(List<SqlCommand> commands, int? commandTimeout = null, CommandType? commandType = null)
        {
            using var transaction = await _connection.BeginTransactionAsync();
            try
            {
                foreach (var command in commands)
                {
                    await _connection.ExecuteAsync(command.Sql, command.Param, transaction, commandTimeout, commandType);
                }

                transaction.Commit();
            }
            catch (MySqlException)
            {
                transaction.Rollback();
                throw;
            }
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public async Task SetupAsync()
        {
            List<string> cmds = new List<string>();
            cmds.Add(@"CREATE TABLE IF NOT EXISTS Url (Id INTEGER,Url TEXT,CrawledAt TEXT,Status INTEGER,CreatedTime TEXT,Remark TEXT, Retry INTEGER DEFAULT (0))");

            foreach (var item in cmds)
            {
                await ExecuteAsync(item);
            }
        }
    }
}