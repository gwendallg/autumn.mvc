using System;

namespace Autumn.Mvc.Models.Paginations.Exceptions
{
    public class OutOfRangePageNumberException : PageableException
    {
        public OutOfRangePageNumberException(int pageNumber,
            Exception innerException = null) : base(
            string.Format("Out of range page number  : {0}", pageNumber), innerException)
        {
        }
    }
}
