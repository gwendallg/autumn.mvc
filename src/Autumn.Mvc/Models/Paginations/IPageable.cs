namespace Autumn.Mvc.Models.Paginations
{
    public interface IPageable<T> where T : class
    {
        /// <summary>
        /// page number 
        /// </summary>
        int PageNumber { get; }

        /// <summary>
        /// page size
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// sort expression
        /// </summary>
        Sort<T> Sort { get; }
    }
}