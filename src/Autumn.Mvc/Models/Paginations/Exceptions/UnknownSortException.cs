using System;

namespace Autumn.Mvc.Models.Paginations.Exceptions
{
    public class UnknownSortException : PageableException
    {
        public UnknownSortException(object sort,
            Exception innerException = null) : base(string.Format("Unknown sort : {0}", sort), innerException)
        {
        }
    }
}