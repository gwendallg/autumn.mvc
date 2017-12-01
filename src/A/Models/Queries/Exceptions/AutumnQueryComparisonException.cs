using System;

namespace Autumn.Mvc.Models.Queries.Exceptions
{
    public abstract class AutumnQueryComparisonException : AutumnQueryException<AutumnQueryParser.ComparisonContext>
    {
        protected AutumnQueryComparisonException(AutumnQueryParser.ComparisonContext origin, string message, Exception innerException = null) : base(origin, message, innerException)
        {
        }
    }
}