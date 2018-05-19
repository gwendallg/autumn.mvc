using System;

namespace Autumn.Mvc.Models.Queries.Exceptions
{
    public class QueryValueInvalidConversionException : QueryValueException
    {
        public QueryValueInvalidConversionException(QueryParser.ValueContext origin, Type type,
            Exception innerException = null) : base(origin,
            string.Format("{0} is not convertible to {1}", origin.GetText(), type.Namespace + "." + type.Name,
                innerException))
        {
        }
    }
}