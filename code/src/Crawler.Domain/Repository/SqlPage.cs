namespace Crawler.Domain.Repository
{
    public abstract class SqlPage
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string OrderBy { get; set; } = "Id";

        public string OrderByDesc { get; set; } = "ASC";
    }
}