using System.Linq.Expressions;
using System.Reflection;

namespace Autumn.Mvc.Models.Queries
{
    public class ExpressionValue
    {
        public PropertyInfo Property { get; set; }
        public Expression Expression { get; set; }
    }
}