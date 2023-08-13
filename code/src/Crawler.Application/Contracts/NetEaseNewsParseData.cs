using Crawler.Domain.Entities.Parser;

namespace Crawler.Application.Contracts
{
    public class NetEaseNewsParseData : ParsedBaseData
    {
        public override string Url { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override string Author { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        [Selector(Expression = "//*[@id=\"ne_wrap\"]", AttributeName = "data-publishtime", IsArray = false, SelectorType = SelectorType.XPath)]
        public override string PublisheTime { get; set; }

        [Selector(Expression = "//title", IsArray = false, SelectorType = SelectorType.XPath)]
        public override string Title { get; set; }

        [Selector(Expression = "//div[@class='post_body']", SelectorType = SelectorType.XPath, IsArray = false)]
        public override string Content { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override string[] Images { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}