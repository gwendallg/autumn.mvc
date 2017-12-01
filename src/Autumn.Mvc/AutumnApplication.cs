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
                    Current.NamingStrategy.GetPropertyName(
                        (autumnOptions.PageSizeFieldName ?? CDefaultPageSizeFieldName), false);
                Current.PageNumberFieldName =
                    Current.NamingStrategy.GetPropertyName(
                        (autumnOptions.PageNumberFieldName ?? CDefaultPageNumberFieldName), false);
                Current.SortFieldName =
                    Current.NamingStrategy.GetPropertyName((autumnOptions.SortFieldName ?? CDefaultSortFieldName),
                        false);
                Current.QueryFieldName =
                    Current.NamingStrategy.GetPropertyName((autumnOptions.QueryFieldName ?? CDefaultQueryFieldName),
                        false);
            }
        }
    }
}