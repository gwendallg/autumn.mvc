using System;

namespace Autumn.Mvc.Models.Paginations.Exceptions
{
    public class OutOfRangePageSizeException : PageableException
    {
        public OutOfRangePageSizeException(int pageSize,
            Exception innerException = null) : base(string.Format("out of range page size  : {0}", pageSize), innerException)
        {
        }
    }
}