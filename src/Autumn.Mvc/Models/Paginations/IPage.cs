using System.Collections.Generic;

namespace Autumn.Mvc.Models.Paginations
{
    public interface IPage<T> where T : class
    {
        IList<T> Content { get; }
        long TotalElements { get; }
        int Number { get; }
        int NumberOfElements { get; }
        int TotalPages { get; }
        bool HasContent { get; }
        bool HasNext { get; }
        bool HasPrevious { get; }
    }
}