using System;
using System.Linq.Expressions;
using Antlr4.Runtime.Tree;
using Autumn.Mvc.Models.Queries.Exceptions;
using Newtonsoft.Json.Serialization;

namespace Autumn.Mvc.Models.Queries
{
    public class AutumnDefaultQueryVisitor<T> : AutumnQueryBaseVisitor<Expression<Func<T, bool>>>
    {
        private readonly NamingStrategy  _namingStrategy;
        private readonly ParameterExpression _parameter;
        
        /// <summary>
        /// create instance of object
        /// </summary>
        /// <param name="namingStrategy"></param>
        public AutumnDefaultQueryVisitor(NamingStrategy namingStrategy)
        {
            _namingStrategy = namingStrategy;
            _parameter = Expression.Parameter(typeof(T));
        }
        
        /// <summary>
        /// visit a or expression
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Expression<Func<T, bool>> VisitOr(AutumnQueryParser.OrContext context)
        {
            return AutumnQueryExpressionHelper.GetOrExpression(this, context);
        }

        /// <summary>
        /// visit a and expression
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Expression<Func<T, bool>> VisitAnd(AutumnQueryParser.AndContext context)
        {
            return AutumnQueryExpressionHelper.GetAndExpression(this, context);
        }

        /// <summary>
        /// visiti a constraint expression
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Expression<Func<T, bool>> VisitConstraint(AutumnQueryParser.ConstraintContext context)
        {
            return context.comparison() != null ? context.comparison().Accept(this) : null;
        }

        public override Expression<Func<T, bool>> VisitErrorNode(IErrorNode node)
        {
            throw new AutumnQueryErrorNodeException(node);
        }

        /// <summary>
        /// visit a comparison expression 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Expression<Func<T, bool>> VisitComparison(AutumnQueryParser.ComparisonContext context)
        {
            var comparator = context.comparator().GetText().ToLowerInvariant();
            switch (comparator)
            {
                case "=is-null=":
                case "=nil=":
                    return AutumnQueryExpressionHelper.GetIsNullExpression<T>(_parameter, context, _namingStrategy);
                case "==":
                case "=eq=":
                    return AutumnQueryExpressionHelper.GetEqExpression<T>(_parameter, context, _namingStrategy);
                case "!=":
                case "=neq=":
                    return AutumnQueryExpressionHelper.GetNeqExpression<T>(_parameter, context, _namingStrategy);
                case "<":
                case "=lt=":
                    return AutumnQueryExpressionHelper.GetLtExpression<T>(_parameter, context, _namingStrategy);
                case "<=":
                case "=le=":
                    return AutumnQueryExpressionHelper.GetLeExpression<T>(_parameter, context, _namingStrategy);
                case ">":
                case "=gt=":
                    return AutumnQueryExpressionHelper.GetGtExpression<T>(_parameter, context, _namingStrategy);
                case ">=":
                case "=ge=":
                    return AutumnQueryExpressionHelper.GetGeExpression<T>(_parameter, context, _namingStrategy);
                case "=in=":
                    return AutumnQueryExpressionHelper.GetInExpression<T>(_parameter, context, _namingStrategy);
                case "=out=":
                case "=nin=":
                    return AutumnQueryExpressionHelper.GetOutExpression<T>(_parameter, context, _namingStrategy);
                default:
                    throw new AutumnQueryComparisonUnknownComparatorException(context);
            }
        }
    }
}