using System;

namespace Autumn.Mvc.Models.Paginations.Exceptions
{
    public abstract class PageableException : AutumnException 
    {
        protected PageableException(string message, Exception innerException = null) : base(
            message,
            innerException)
        {

        }
    }

}