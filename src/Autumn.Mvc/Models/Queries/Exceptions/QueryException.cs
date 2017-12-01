using System;

namespace Autumn.Mvc.Models.Queries.Exceptions
{
    public abstract class QueryException<T> : AutumnException
    {
        public T Origin { get; }

        protected QueryException(T origin, string message, Exception innerException = null) : base(message, innerException)
        {
            Origin = origin;
        }
    }

}