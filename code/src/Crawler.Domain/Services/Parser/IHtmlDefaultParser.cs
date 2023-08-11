using Crawler.Domain.Entities.Parser;

namespace Crawler.Domain.Services.Parser
{
    public interface IHtmlDefaultParser<T> : IParser<T> where T : ParsedBaseData, new()
    {
    }
}