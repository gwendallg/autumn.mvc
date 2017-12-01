using System;
using System.Linq.Expressions;
using Antlr4.Runtime.Tree;
using Autumn.Mvc.Models.Queries.Exceptions;
using Newtonsoft.Json.Serialization;

namespace Autumn.Mvc.Models.Queries
{
    public class DefaultQueryVisitor<T> : QueryBaseVisitor<Expression<Func<T, bool>>>
    {
        private readonly NamingStrategy  _namingStrategy;
        private readonly ParameterExpression _parameter;
        
        /// <summary>
        /// create instance of object
        /// </summary>
        /// <param name="namingStrategy"></param>
        public DefaultQueryVisitor(NamingStrategy namingStrategy)
        {
            _namingStrategy = namingStrategy;
            _parameter = Expression.Parameter(typeof(T));
        }
        
        /// <summary>
        /// visit a or expression
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Expression<Func<T, bool>> VisitOr(QueryParser.OrContext context)
        {
            return QueryExpressionHelper.GetOrExpression(this, context);
        }

        /// <summary>
        /// visit a and expression
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Expression<Func<T, bool>> VisitAnd(QueryParser.AndContext context)
        {
            return QueryExpressionHelper.GetAndExpression(this, context);
        }

        /// <summary>
        /// visiti a constraint expression
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Expression<Func<T, bool>> VisitConstraint(QueryParser.ConstraintContext context)
        {
            return context.comparison() != null ? context.comparison().Accept(this) : null;
        }

        public override Expression<Func<T, bool>> VisitErrorNode(IErrorNode node)
        {
            throw new QueryErrorNodeException(node);
        }

        /// <summary>
        /// visit a comparison expression 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Expression<Func<T, bool>> VisitComparison(QueryParser.ComparisonContext context)
        {
            var comparator = context.comparator().GetText().ToLowerInvariant();
            switch (comparator)
            {
                case "=is-null=":
                case "=nil=":
                    return QueryExpressionHelper.GetIsNullExpression<T>(_parameter, context, _namingStrategy);
                case "==":
                case "=eq=":
                    return QueryExpressionHelper.GetEqExpression<T>(_parameter, context, _namingStrategy);
                case "!=":
                case "=neq=":
                    return QueryExpressionHelper.GetNeqExpression<T>(_parameter, context, _namingStrategy);
                case "<":
                case "=lt=":
                    return QueryExpressionHelper.GetLtExpression<T>(_parameter, context, _namingStrategy);
                case "<=":
                case "=le=":
                    return QueryExpressionHelper.GetLeExpression<T>(_parameter, context, _namingStrategy);
                case ">":
                case "=gt=":
                    return QueryExpressionHelper.GetGtExpression<T>(_parameter, context, _namingStrategy);
                case ">=":
                case "=ge=":
                    return QueryExpressionHelper.GetGeExpression<T>(_parameter, context, _namingStrategy);
                case "=in=":
                    return QueryExpressionHelper.GetInExpression<T>(_parameter, context, _namingStrategy);
                case "=out=":
                case "=nin=":
                    return QueryExpressionHelper.GetOutExpression<T>(_parameter, context, _namingStrategy);
                default:
                    throw new QueryComparisonUnknownComparatorException(context);
            }
        }
    }
}