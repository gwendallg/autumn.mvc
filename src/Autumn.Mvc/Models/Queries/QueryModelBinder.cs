using System;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Caching.Memory;

namespace Autumn.Mvc.Models.Queries
{
    public class QueryModelBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var queryCollection = bindingContext.ActionContext.HttpContext.Request.Query;
            var eval = QueryExpressionHelper.True<T>();
            if (queryCollection.TryGetValue(AutumnApplication.Current.QueryFieldName, out var query))
            {
                eval = GetOrRegistryQuery(query);
            }
            bindingContext.Result = ModelBindingResult.Success(eval);
            return Task.CompletedTask;
        }

        private static Expression<Func<T, bool>> GetOrRegistryQuery(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return QueryExpressionHelper.True<T>();
            var hash = Hash(query);
            if (QueryExpressionHelper.QueriesCache.TryGetValue(hash, out Expression<Func<T, bool>> result)) return result;
            result = Build(query);
            QueryExpressionHelper.QueriesCache.Set(hash, result);
            return result;
        }

        /// <summary>
        /// create hash 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private static string Hash(string query)
        {
            using (var md5 = MD5.Create())
            {
                md5.Initialize();
                md5.ComputeHash(Encoding.UTF8.GetBytes(string.Format("{0}?{1}", typeof(T).FullName, query)));
                var hash = md5.Hash;
                var builder = new StringBuilder();
                foreach (var t in hash)
                {
                    builder.Append(t.ToString("x2"));
                }
                return builder.ToString();
            }    
        }

        /// <summary>
        /// build specification from rsql query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private static Expression<Func<T, bool>> Build(string query)
        {
            var antlrInputStream = new AntlrInputStream(query);
            var lexer = new QueryLexer(antlrInputStream);
            var commonTokenStream = new CommonTokenStream(lexer);
            var parser = new QueryParser(commonTokenStream);
            var eval = parser.or();
            var visitor = new DefaultQueryVisitor<T>(AutumnApplication.Current.NamingStrategy);
            return visitor.VisitOr(eval);
        }
    }
}