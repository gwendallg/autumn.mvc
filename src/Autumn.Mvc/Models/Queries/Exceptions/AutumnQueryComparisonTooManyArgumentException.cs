using System;

namespace Autumn.Mvc.Models.Queries.Exceptions
{
    public class AutumnQueryComparisonTooManyArgumentException : AutumnQueryComparisonException
    {
        public AutumnQueryComparisonTooManyArgumentException(AutumnQueryParser.ComparisonContext origin,
            Exception innerException = null) : base(origin,string.Format("Too many arguments : {0}",origin.selector().GetText()), innerException)
        {
        }
    }
}