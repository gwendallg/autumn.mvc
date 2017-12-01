using System;

namespace Autumn.Mvc.Models.Queries.Exceptions
{
    public abstract class AutumnQueryException<T> : AutumnException
    {
        public T Origin { get; }

        protected AutumnQueryException(T origin, string message, Exception innerException = null) : base(message, innerException)
        {
            Origin = origin;
        }
    }

}