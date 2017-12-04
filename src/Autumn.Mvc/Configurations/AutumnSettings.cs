using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Autumn.Mvc.Configurations
{
    public class AutumnSettings
    {
        public const string CDefaultPageSizeFieldName = "PageSize";
        public const int CDefaultPageSize = 100;
        public const string CDefaultPageNumberFieldName = "PageNumber";
        public const string CDefaultQueryFieldName = "Query";
        public const string CDefaultSortFieldName = "Sort";

        public string PageSizeField { get; set; }
        public string SortField { get; set; }
        public string PageNumberField { get; set; }
        public string QueryField { get; set; }
        public int PageSize { get; set; }
        public JsonSerializerSettings JsonSerializerSettings { get; set; }
        public NamingStrategy NamingStrategy { get; set; }
        public IHostingEnvironment HostingEnvironment { get; set; }

        public AutumnSettings()
        {
            PageSizeField = CDefaultPageSizeFieldName;
            PageNumberField = CDefaultPageNumberFieldName;
            PageSize = CDefaultPageSize;
            SortField = CDefaultSortFieldName;
            QueryField = CDefaultQueryFieldName;
            NamingStrategy = new DefaultNamingStrategy();
        }
    }
}