using System;
using System.Collections.Generic;
using System.Text;

namespace Autumn.Mvc.Models.Queries.Exceptions
{
    public class QueryComparisonInvalidMatchTypeException : QueryComparisonException
    {
        public QueryComparisonInvalidMatchTypeException(QueryParser.ComparisonContext origin,
            Exception innerException = null) : base(origin,
            string.Format("Invalid comparison match type : {0} and {1}", origin.selector().GetText(), origin.arguments().GetText()), innerException)
        {
        }
    }
}
