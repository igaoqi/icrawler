using Crawler.Domain.Dependency;

namespace Crawler.Domain.Repository
{
    public interface IEntityRepository<T, TKey> : ITransientDependency
    {
        Task<T> GetAsync(TKey id);

        Task<T> GetFirstOrDefaultAsync(string sql, object param);

        Task<IEnumerable<T>> GetListAsync(string sql, object param);

        Task<int> InsertAsync(T entity);

        Task<int> UpdateAsync(T entity);

        Task<int> DeleteAsync(TKey id);
    }
}