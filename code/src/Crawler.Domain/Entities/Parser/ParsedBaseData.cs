namespace Crawler.Domain.Entities.Parser
{
    public abstract class ParsedBaseData
    {
        public abstract string Url { get; set; }

        public abstract string Author { get; set; }

        public abstract DateTime PublishedTime { get; set; }

        public abstract string Title { get; set; }

        public abstract string Content { get; set; }

        public abstract string[] Images { get; set; }
    }
}