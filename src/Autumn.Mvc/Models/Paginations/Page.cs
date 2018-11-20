
using System.Collections.Generic;

namespace Autumn.Mvc.Models.Paginations
{
    public class Page<T> : IPage<T> where T : class
    {

        public IList<T> Content { get; }
        public long TotalElements { get; }
        public int Number { get; }
        public int NumberOfElements { get; }
        public int TotalPages { get; }
        public bool HasContent { get; }
        public bool HasNext { get; }
        public bool HasPrevious { get; }

        public Page():this(new List<T>())
        {
            
        }

        public Page(List<T> content, IPageable<T> pageable = null, long? total = null)
        {
            Content = content ?? new List<T>();
            if (total == null)
            {
                TotalElements = Content.Count;
            }
            else
            {
                TotalElements = total.Value < (long) Content.Count ? Content.Count : total.Value;
            }

            HasContent = Content.Count > 0;
            if (HasContent)
            {
                NumberOfElements = Content.Count;
            }

            if (pageable == null) return;
            Number = pageable.PageNumber;
            HasPrevious = pageable.PageNumber > 0;
            HasNext = TotalElements > NumberOfElements + Number * pageable.PageSize;
            if (TotalElements <= 0) return;
            var mod = (int) TotalElements % pageable.PageSize;
            var quo = ((int) TotalElements) - mod;
            TotalPages = (quo / pageable.PageSize) + (mod > 0 ? 1 : 0);
        }
    }
}