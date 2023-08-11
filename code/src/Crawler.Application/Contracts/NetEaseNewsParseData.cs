using Crawler.Domain.Entities.Parser;

namespace Crawler.Application.Contracts
{
    public class NetEaseNewsParseData : ParsedBaseData
    {
        public override string Url { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override string Author { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override DateTime PublishedTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        [Selector(Expression = "//title", IsArray = false, SelectorType = SelectorType.XPath)]
        public override string Title { get; set; }

        public override string Content { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string[] Images { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}