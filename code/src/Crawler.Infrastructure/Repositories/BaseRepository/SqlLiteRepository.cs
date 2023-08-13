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
        private readonly string _connection;

        public SqlLiteRepository(IOptionsSnapshot<SqlLiteConfig> optionsSnapshot)
        {
            _connection = optionsSnapshot.Value.ConnectionString;
        }

        public Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var conn = GetSqliteConnection(_connection))
            {
                return conn.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
            }
        }

        public Task<T> ExecuteScalarAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using var conn = GetSqliteConnection(_connection);
            return conn.ExecuteScalarAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public async Task ExecuteTransactionAsync(List<SqlCommand> commands, int? commandTimeout = null, CommandType? commandType = null)
        {
            using var conn = GetSqliteConnection(_connection);
            using var transaction = await conn.BeginTransactionAsync();
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

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using var conn = GetSqliteConnection(_connection);
            return conn.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using var conn = GetSqliteConnection(_connection);
            return conn.QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        private SqliteConnection GetSqliteConnection(string conn)
        {
            if (string.IsNullOrEmpty(conn))
            {
                conn = _connection;
            }

            return new SqliteConnection(conn);
        }

        public async Task SetupAsync()
        {
            List<string> cmds = new List<string>();
            cmds.Add(@"CREATE TABLE IF NOT EXISTS CrawleUrl (Id INTEGER,Url TEXT,CrawledAt TEXT,Status INTEGER,CreatedTime TEXT,Remark TEXT, Retry INTEGER DEFAULT (0))");

            cmds.Add(@"CREATE TABLE NetEaseNewsArticle (Id INTEGER,Url TEXT,Author TEXT,PublishedTime TEXT,Title TEXT,Content TEXT,Images TEXT,CONSTRAINT NewTable_PK PRIMARY KEY (Id))");

            foreach (var item in cmds)
            {
                await ExecuteAsync(item);
            }
        }
    }
}