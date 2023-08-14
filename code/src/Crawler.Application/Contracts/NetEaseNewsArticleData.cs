using System.ComponentModel.DataAnnotations.Schema;
using Crawler.Domain.Entities.Parser;

namespace Crawler.Application.Contracts
{
    public class NetEaseNewsArticleData : ParsedBaseData
    {
        public override string Url { get; set; }

        public override string Author { get; set; }

        [Selector(Expression = "//*[@id=\"ne_wrap\"]", AttributeName = "data-publishtime", IsArray = false, SelectorType = SelectorType.XPath)]
        public override string PublisheTime { get; set; }

        [Selector(Expression = "//title", IsArray = false, SelectorType = SelectorType.XPath)]
        public override string Title { get; set; }

        [Selector(Expression = "//div[@class='post_body']", SelectorType = SelectorType.XPath, IsArray = false)]
        public override string Content { get; set; }

        public override string[] Images { get; set; }
    }
}