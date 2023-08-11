using Crawler.Application.Dependency;
using Crawler.Domain.Entities.Parser;
using Crawler.Domain.Services.Parser;

namespace Crawler.Application.Services
{
    public class HtmlDefaultParseAppService<T> : ITransientSelfDependency where T : ParsedBaseData, new()
    {
        private readonly IHtmlDefaultParser<T> _htmlDefaultParser;

        public HtmlDefaultParseAppService(IHtmlDefaultParser<T> htmlDefaultParser)
        {
            _htmlDefaultParser = htmlDefaultParser;
        }

        public async Task<T> ParseAsync(string content)
        {
            return await _htmlDefaultParser.Parse(content);
        }
    }
}