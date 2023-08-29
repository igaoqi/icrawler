using Crawler.Infrastructure.Services.Exporter;

namespace Crawler.Infrastructure.Test.Entities
{
    public class Orders
    {
        [ExportCsv(Name = "编号", Sort = 1)]
        public int Id { get; set; }

        [ExportCsv(Name = "订单号", Sort = 2)]
        public string OrderNo { get; set; }

        [ExportCsv(Name = "创建时间", Sort = 3, Format = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime CreateTime { get; set; }

        [ExportCsv(Name = "金额", Sort = 4, Format = "{0:C}")]
        public decimal Amount { get; set; }
    }
}