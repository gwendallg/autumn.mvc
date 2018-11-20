using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using Autumn.Mvc.Models.Paginations;
using Autumn.Mvc.Samples.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace Autumn.Mvc.Samples.Controllers
{
    [Route("customers")]
    public class CustomerController : Controller
    {

        private readonly IList<Customer> _customers;
        private readonly ILogger _logger;

        public CustomerController(IList<Customer> customers, ILoggerFactory loggerFactory)
        {
            _customers = customers;
            _logger = loggerFactory.CreateLogger("CustomerLog");
        }

        [HttpGet]
        public IActionResult Get(Expression<Func<Customer, bool>> filter,
            IPageable<Customer> pageable)
        {
            if (!ModelState.IsValid)
                return StatusCode((int) HttpStatusCode.InternalServerError, new ErrorModel(ModelState));
            _logger.LogDebug(filter?.ToString());
            
            var content = _customers
                .AsQueryable();

            if (filter != null)
            {
                content = content
                    .Where(filter);
            }

            if (pageable == null || content.Count() <= pageable.PageSize)
                return Ok(new Page<Customer>(content.ToList()));

            if (pageable.Sort?.OrderBy?.Count() > 0)
            {
                content = pageable.Sort.OrderBy.Aggregate(content, (current, order) => current.OrderBy(order));
            }

            if (pageable.Sort?.OrderDescendingBy?.Count() > 0)
            {
                content = pageable.Sort.OrderDescendingBy.Aggregate(content,
                    (current, order) => current.OrderByDescending(order));
            }

            var offset = pageable.PageNumber * pageable.PageSize;
            var limit = pageable.PageSize;
            var count = content.Count();
            content = content.Skip(offset)
                .Take(limit);

            return StatusCode((int) HttpStatusCode.PartialContent,
                new Page<Customer>(content.ToList(), pageable, count));
        }

    }
}