using System;

namespace Autumn.Mvc.Models.Paginations.Exceptions
{
    public class InvalidSortDirectionException : PageableException
    {
        public InvalidSortDirectionException(object sortDirection,
            Exception innerException = null) : base(
            string.Format("Invalid sort direction : {0}", sortDirection), innerException)
        {
        }
    }
}