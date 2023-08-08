namespace Crawler.Domain.Options
{
    public class QuartzConfig
    {
        public bool Enabled { get; set; }

        public string Type { get; set; }

        public string Cron { get; set; }
    }
}