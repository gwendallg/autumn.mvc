using System;

namespace Autumn.Mvc.Models.Paginations.Exceptions
{
    public class InvalidPageSizeValueException : PageableException
    {
        public InvalidPageSizeValueException(object pageSize,
            Exception innerException = null) : base(
            string.Format("Invalid page size value : {0}", pageSize), innerException)
        {
        }
    }
}