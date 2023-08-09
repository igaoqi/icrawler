namespace Crawler.Domain.Repository
{
    public interface IEntityRepository<T, TKey>
    {
        Task<T> GetAsync(TKey id);

        Task<T> FirstOrDefaultAsync(Predicate<T> predicate);

        Task<IEnumerable<T>> GetListAsync();

        Task<int> InsertAsync(T entity);

        Task<int> UpdateAsync(T entity);

        Task<int> DeleteAsync(TKey id);
    }
}