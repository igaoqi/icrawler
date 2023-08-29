using System.Globalization;
using System.Reflection;
using System.Text;

namespace Crawler.Infrastructure.Services.Exporter
{
    public class CsvHelper
    {
        public static void ExportToCsv<T>(IEnumerable<T> data, string filePath)
        {
            var properties = typeof(T).GetProperties()
                                      .Where(prop => Attribute.IsDefined(prop, typeof(ExportCsvAttribute)))
                                      .OrderBy(prop => ((ExportCsvAttribute)prop.GetCustomAttribute(typeof(ExportCsvAttribute))).Sort);

            var nestedProperties = properties.Where(prop => prop.PropertyType.IsClass && prop.PropertyType != typeof(string));

            var nestedColumns = new List<string>();
            foreach (var nestedProp in nestedProperties)
            {
                var nestedType = nestedProp.PropertyType;
                var nestedProps = nestedType.GetProperties()
                                            .Where(prop => Attribute.IsDefined(prop, typeof(ExportCsvAttribute)))
                                            .OrderBy(prop => ((ExportCsvAttribute)prop.GetCustomAttribute(typeof(ExportCsvAttribute))).Sort);

                foreach (var prop in nestedProps)
                {
                    var nestedAttr = (ExportCsvAttribute)prop.GetCustomAttribute(typeof(ExportCsvAttribute));
                    nestedColumns.Add($"{nestedProp.Name}_{prop.Name}");
                }
            }

            var headerColumns = properties.Select(prop => ((ExportCsvAttribute)prop.GetCustomAttribute(typeof(ExportCsvAttribute))).Name)
                                          .Concat(nestedColumns);

            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8, 10240))
            {
                writer.WriteLine(string.Join(",", headerColumns));

                foreach (var item in data)
                {
                    var values = new List<string>();
                    foreach (var prop in properties)
                    {
                        var attr = (ExportCsvAttribute)prop.GetCustomAttribute(typeof(ExportCsvAttribute));
                        var value = prop.GetValue(item);

                        if (attr.IsArray && value is IEnumerable<object> arrayValue)
                        {
                            var arrayValues = arrayValue.Select(v => FormatValue(v, attr));
                            values.Add(string.Join(",", arrayValues));
                        }
                        else
                        {
                            values.Add(FormatValue(value, attr));
                        }
                    }

                    foreach (var nestedProp in nestedProperties)
                    {
                        var nestedValue = nestedProp.GetValue(item);

                        if (nestedValue == null)
                        {
                            for (int i = 0; i < nestedColumns.Count(); i++)
                            {
                                values.Add(FormatValue(null, new ExportCsvAttribute()));  // Fill null values for nested properties
                            }
                        }
                        else
                        {
                            var nestedProps = nestedValue.GetType().GetProperties()
                                                        .Where(prop => Attribute.IsDefined(prop, typeof(ExportCsvAttribute)))
                                                        .OrderBy(prop => ((ExportCsvAttribute)prop.GetCustomAttribute(typeof(ExportCsvAttribute))).Sort);

                            foreach (var prop in nestedProps)
                            {
                                var nestedAttr = (ExportCsvAttribute)prop.GetCustomAttribute(typeof(ExportCsvAttribute));
                                var nestedPropValue = prop.GetValue(nestedValue);

                                values.Add(FormatValue(nestedPropValue, nestedAttr));
                            }
                        }
                    }

                    writer.WriteLine(string.Join(",", values));
                }
            }
        }

        private static string FormatValue(object value, ExportCsvAttribute attribute)
        {
            if (value == null)
            {
                return attribute.DefaultValue;
            }

            if (!string.IsNullOrEmpty(attribute.Format))
            {
                return string.Format(CultureInfo.InvariantCulture, attribute.Format, value);
            }

            return value.ToString();
        }
    }
}