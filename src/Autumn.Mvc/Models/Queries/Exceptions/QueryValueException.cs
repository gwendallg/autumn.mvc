using System;

namespace Autumn.Mvc.Models.Queries.Exceptions
{
    public abstract class QueryValueException: QueryException<QueryParser.ValueContext>
    {
        protected QueryValueException(QueryParser.ValueContext origin, string message, Exception innerException = null) : base(origin, message, innerException)
        {
        }
    }
}