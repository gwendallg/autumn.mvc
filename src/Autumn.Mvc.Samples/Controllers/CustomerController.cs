using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Autumn.Mvc.Models.Paginations;
using Autumn.Mvc.Samples.Models;
using Microsoft.AspNetCore.Mvc;

namespace Autumn.Mvc.Samples.Controllers
{
    [Route("customers")]
    public class CustomerController : Controller
    {

        private readonly IList<Customer> _customers;

        public CustomerController(IList<Customer> customers)
        {
            _customers = customers;
        }

        [HttpGet]
        public AutumnPage<Customer> Get(Expression<Func<Customer, bool>> filter,
            AutumnPageable<Customer> pageable)
        {

            var content = _customers
                .AsQueryable();

            if (filter != null)
            {
                content = content
                    .Where(filter);
            }

            if (pageable == null) return new AutumnPage<Customer>(content.ToList(), pageable, _customers.Count);
            
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
            content = content.Skip(offset)
                .Take(limit);
            return new AutumnPage<Customer>(content.ToList(), pageable, _customers.Count);
        }
    }
}