using System.Reflection;
using Crawler.Domain.Entities.Parser;
using Crawler.Domain.Services.Parser;
using HtmlAgilityPack;

namespace Crawler.Infrastructure.Services.Parser
{
    public class HtmlDefaultParser<T> : IHtmlDefaultParser<T> where T : ParsedBaseData, new()
    {
        public HtmlDefaultParser()
        {
        }

        public Task<T> Parse(string content)
        {
            var doc = GetHtmlDocument(content);

            var result = new T();

            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var selector = property.GetCustomAttribute<Selector>();
                if (selector != null)
                {
                    switch (selector.SelectorType)
                    {
                        case SelectorType.XPath:
                            SetXPathValue(result, doc, selector, property);
                            break;

                        default:
                            break;
                    }
                }
            }

            return Task.FromResult(result);
        }

        private HtmlDocument GetHtmlDocument(string content)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(content);

            return doc;
        }

        private void SetXPathValue(T obj, HtmlDocument doc, Selector selector, PropertyInfo property)
        {
            var node = doc.DocumentNode.SelectSingleNode(selector.Expression);
            property.SetValue(obj, node.InnerText);
        }
    }
}