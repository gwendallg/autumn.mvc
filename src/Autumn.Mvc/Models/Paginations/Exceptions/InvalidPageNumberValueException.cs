using System;

namespace Autumn.Mvc.Models.Paginations.Exceptions
{
    public class InvalidPageNumberValueException : PageableException
    {
        public InvalidPageNumberValueException(object pageNumber,
            Exception innerException = null) : base(
            string.Format("Invalid page number value : {0}", pageNumber), innerException)
        {
        }
    }
}