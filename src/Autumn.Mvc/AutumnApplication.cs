using System.Reflection;
using Autumn.Mvc.Configurations;
using Newtonsoft.Json.Serialization;

namespace Autumn.Mvc
{
    public class AutumnApplication
    {
        
        private static readonly NamingStrategy CDefaultNamingStrategy = new DefaultNamingStrategy();
        private const string CDefaultPageSizeFieldName = "PageSize";
        private const string CDefaultSortFieldName = "Sort";
        private const string CDefaultPageNumberFieldName = "PageNumber";
        private const string CDefaultQueryFieldName = "Query";
        private const int CDefaultPageSize = 100;

        static AutumnApplication()
        {
            Current = new AutumnApplication();
        }

        private AutumnApplication()
        {
        }

        /// <summary>
        /// naming strategy of Autumn Application
        /// </summary>
        public NamingStrategy NamingStrategy { get; private set; }
        public int DefaultPageSize { get; private set; }
        public string PageSizeFieldName { get; private set; }
        public string SortFieldName { get; private set; }
        public string PageNumberFieldName { get; private set; }
        public string QueryFieldName { get; private set; }
        public static AutumnApplication Current { get; }
 
        public static void Initialize(AutumnOptions autumnOptions)
        {
            lock (Current)
            {
                Current.NamingStrategy = autumnOptions.NamingStrategy ?? CDefaultNamingStrategy;
                Current.DefaultPageSize = autumnOptions.DefaultPageSize <= 0
                    ? CDefaultPageSize
                    : autumnOptions.DefaultPageSize;
                Current.PageSizeFieldName =
                    (autumnOptions.PageSizeFieldName ?? CDefaultPageSizeFieldName).ToCase(Current.NamingStrategy);
                Current.PageNumberFieldName =
                    (autumnOptions.PageNumberFieldName ?? CDefaultPageNumberFieldName).ToCase(Current.NamingStrategy);
                Current.SortFieldName =
                    (autumnOptions.SortFieldName ?? CDefaultSortFieldName).ToCase(Current.NamingStrategy);
                Current.QueryFieldName =
                    (autumnOptions.QueryFieldName ?? CDefaultQueryFieldName).ToCase(Current.NamingStrategy);
            }
        }
    }
}