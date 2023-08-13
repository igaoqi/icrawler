using Crawler.Domain.Entities.CrawleUrl;
using Crawler.Domain.Repository;

namespace Crawler.Infrastructure.Repositories
{
    public class CrawleUrlRepository : ICrawleUrlRepository
    {
        private readonly ICmdRepository _repository;

        public CrawleUrlRepository(ICmdRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> DeleteAsync(long id)
        {
            var cmd = new SqlCommand
            {
                Sql = "DELETE FROM CrawleUrl WHERE Id = @Id",
                Param = new { Id = id }
            };

            return await _repository.ExecuteAsync(cmd.Sql, cmd.Param);
        }

        public async Task<CrawleUrl> GetAsync(long id)
        {
            var cmd = new SqlCommand
            {
                Sql = "SELECT * FROM CrawleUrl WHERE Id = @Id LIMIT 1",
                Param = new { Id = id }
            };

            return await _repository.QueryFirstOrDefaultAsync<CrawleUrl>(cmd.Sql, cmd.Param);
        }

        public async Task<CrawleUrl> GetFirstOrDefaultAsync(string sql, object param)
        {
            return await _repository.QueryFirstOrDefaultAsync<CrawleUrl>(sql, param);
        }

        public async Task<IEnumerable<CrawleUrl>> GetListAsync(string sql, object param)
        {
            var cmd = new SqlCommand
            {
                Sql = sql,
                Param = param
            };

            return await _repository.QueryAsync<CrawleUrl>(cmd.Sql, cmd.Param);
        }

        public Task<int> InsertAsync(CrawleUrl entity)
        {
            var cmd = new SqlCommand
            {
                Sql = @"INSERT INTO CrawleUrl (Id, Url, CrawledAt, Status, CreatedTime, UpdatedTime, Remark ,Retry ,TenantId ,CreatedBy ,UpdatedBy)
                                       VALUES (@Id, @Url, @CrawledAt, @Status, @CreatedTime,@UpdatedTime,@Remark,@Retry,@TenantId,@CreatedBy,@UpdatedBy)",
                Param = entity
            };

            return _repository.ExecuteAsync(cmd.Sql, cmd.Param);
        }

        public Task<int> UpdateAsync(CrawleUrl entity)
        {
            var cmd = new SqlCommand
            {
                //Sql = "UPDATE CrawleUrl SET Url = @Url, CrawledAt = @CrawledAt, Status = @Status, CreatedTime = @CreatedTime WHERE Id = @Id",
                Sql = "UPDATE CrawleUrl SET Status = @Status,Retry=@Retry,Remark=@Remark WHERE Id = @Id",
                Param = entity
            };

            return _repository.ExecuteAsync(cmd.Sql, cmd.Param);
        }
    }
}