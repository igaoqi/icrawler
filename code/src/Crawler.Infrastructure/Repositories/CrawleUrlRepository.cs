using System.Linq.Expressions;
using Crawler.Domain.Entities;
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

        public async Task<CrawleUrl> FirstOrDefaultAsync(Expression<Func<CrawleUrl, bool>> predicate)
        {
            var cmd = new SqlCommand
            {
                Sql = "SELECT * FROM CrawleUrl" + predicate,
            };

            return await _repository.QueryFirstOrDefaultAsync<CrawleUrl>(cmd.Sql, cmd.Param);
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

        public async Task<IEnumerable<CrawleUrl>> GetListAsync()
        {
            var cmd = new SqlCommand
            {
                Sql = "SELECT * FROM CrawleUrl"
            };

            return await _repository.QueryAsync<CrawleUrl>(cmd.Sql);
        }

        public Task<int> InsertAsync(CrawleUrl entity)
        {
            var cmd = new SqlCommand
            {
                Sql = "INSERT INTO CrawleUrl (Id, Url, CrawledAt, Status, CreatedTime ,Remark ,Retry) VALUES (@Id, @Url, @CrawledAt, @Status, @CreatedTime,@Remark,@Retry)",
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