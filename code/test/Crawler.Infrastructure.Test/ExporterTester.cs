using AutoFixture.Xunit2;
using Crawler.Infrastructure.Services.Exporter;
using Crawler.Infrastructure.Test.Entities;

namespace Crawler.Infrastructure.Test
{
    public class ExporterTester
    {
        [Theory, AutoData]
        public void ExportToCsvTest(List<Users> users)
        {
            CsvHelper.ExportToCsv(users, "users.csv");
        }
    }
}