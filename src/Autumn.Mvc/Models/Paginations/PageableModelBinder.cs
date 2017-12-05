using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Autumn.Mvc.Configurations;
using Autumn.Mvc.Models.Paginations.Exceptions;
using Autumn.Mvc.Models.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Autumn.Mvc.Models.Paginations
{
    public class PageableModelBinder<T> : IModelBinder where T : class
    {
        private readonly AutumnSettings _autumnSettings;

        public PageableModelBinder(AutumnSettings autumnSettings)
        {
            _autumnSettings = autumnSettings ?? throw new ArgumentNullException(nameof(autumnSettings));
        }
        
        public IPageable<T> Build(IQueryCollection queryCollection)
        {
            if (queryCollection == null) throw new ArgumentNullException(nameof(queryCollection));
            
            var pageSize =_autumnSettings.PageSize;
            if (queryCollection.TryGetValue(_autumnSettings.PageSizeField, out var pageSizeString))
            {
                if (int.TryParse(pageSizeString[0], out pageSize))
                {
                    if (pageSize < 0)
                    {
                        throw new OutOfRangePageSizeException(pageSize);
                    }
                }
                else
                {
                    throw new InvalidPageSizeValueException(pageSizeString[0]);
                }
            }
            var pageNumber = 0;
            if (queryCollection.TryGetValue(_autumnSettings.PageNumberField, out var pageNumberString))
            {
                if (int.TryParse(pageNumberString[0], out pageNumber))
                {
                    if (pageNumber < 0)
                    {
                        throw new OutOfRangePageNumberException(pageNumber);
                    }
                }
                else
                {
                    throw new InvalidPageNumberValueException(pageNumberString[0]);
                }
            }

            Sort<T> sort = null;
            if (queryCollection.TryGetValue(_autumnSettings.SortField, out var sortStringValues))
            {
                var parameter = Expression.Parameter(typeof(T));

                var orderBy = new List<Expression<Func<T, object>>>();
                var orderDescendingBy = new List<Expression<Func<T, object>>>();
     
                foreach (var sortStringValue in sortStringValues)
                {
                    if (!ExpressionValue.TryParse<T>(parameter, sortStringValue, _autumnSettings.NamingStrategy,
                        out var exp))
                    {
                        throw new UnknownSortException(sortStringValue);
                    }
                    var expression = Expression.Convert(exp.Expression, typeof(object));
                    var orderExpression = Expression.Lambda<Func<T, object>>(expression, parameter);
                    var propertyKeyDirection = sortStringValue;
                    var direction = ".Dir";
                    direction = _autumnSettings.NamingStrategy?.GetPropertyName(direction, false);
                    propertyKeyDirection = propertyKeyDirection + direction;
                    var isDescending = false;
                    if (queryCollection.ContainsKey(propertyKeyDirection))
                    {
                        var sortDirection = queryCollection[propertyKeyDirection][0];
                        if (sortDirection.ToLowerInvariant() != "asc" && sortDirection.ToLowerInvariant() != "desc")
                        {
                            throw new InvalidSortDirectionException(sortDirection);
                        }
                        isDescending = sortDirection.ToLowerInvariant() == "desc";
                    }
                    if (isDescending)
                    {
                        orderDescendingBy.Add(orderExpression);
                    }
                    else
                    {
                        orderBy.Add(orderExpression);
                    }
                }
                sort = new Sort<T>(orderBy, orderDescendingBy);
            }
            return new Pageable<T>(pageNumber, pageSize, sort);
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            try
            {
                bindingContext.Result =
                    ModelBindingResult.Success(Build(bindingContext.ActionContext.HttpContext.Request.Query));
            }
            catch (Exception e)
            {
                bindingContext.ModelState.AddModelError(GetType().FullName, e.Message);
            }
            return Task.CompletedTask;
        }
    }
}