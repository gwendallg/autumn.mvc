using System.Reflection;
using Newtonsoft.Json.Serialization;

namespace Autumn.Mvc.Configurations
{
    public class AutumnOptions
    {
        public string PageSizeFieldName { get; set; }
        public string SortFieldName { get; set; }
        public string PageNumberFieldName { get; set; }
        public string QueryFieldName { get; set; }
        public int DefaultPageSize { get; set; }
        public NamingStrategy NamingStrategy { get; set; }
    }
}