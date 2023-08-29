namespace Crawler.Infrastructure.Services.Exporter
{
    public class ExportCsvAttribute : Attribute
    {
        public string Name { get; set; }

        /// <summary>
        /// 数字越小越靠前
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 忽略该字段
        /// </summary>
        public bool Ignore { get; set; } = false;

        /// <summary>
        /// 是否是数组
        /// </summary>
        public bool IsArray { get; set; } = false;

        /// <summary>
        /// ToString 格式化
        /// </summary>
        public string Format { get; set; } = "";

        /// <summary>
        /// 为空时默认值
        /// </summary>
        public string DefaultValue { get; set; } = "";
    }
}