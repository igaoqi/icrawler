namespace Crawler.Domain.Entities.Parser
{
    public class Selector : Attribute
    {
        public SelectorType SelectorType { get; set; }

        public string Expression { get; set; }

        public bool IsArray { get; set; } = false;
    }
}