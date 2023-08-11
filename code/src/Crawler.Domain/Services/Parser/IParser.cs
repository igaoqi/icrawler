using Crawler.Domain.Dependency;
using Crawler.Domain.Entities.Parser;

namespace Crawler.Domain.Services.Parser
{
    public interface IParser<T> : ITransientDependency where T : ParsedBaseData, new()
    {
        Task<T> Parse(string content);
    }
}