using Crawler.Infrastructure.Services.Exporter;

namespace Crawler.Infrastructure.Test.Entities
{
    public class Users
    {
        [ExportCsv(Name = "编号", Sort = 1)]
        public int Id { get; set; }

        [ExportCsv(Name = "姓名", Sort = 2)]
        public string Name { get; set; }

        [ExportCsv(Name = "邮箱", Sort = 3)]
        public string Email { get; set; }

        [ExportCsv(Name = "密码", Sort = 4)]
        public string Password { get; set; }

        [ExportCsv(Name = "用户名", Sort = 5)]
        public string UserName { get; set; }

        [ExportCsv(Name = "订单", Sort = 6, IsArray = true)]
        public List<Orders> Orders { get; set; }
    }
}