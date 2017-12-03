using System;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Autumn.Mvc.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Caching.Memory;

namespace Autumn.Mvc.Models.Queries
{
    public class QueryModelBinder<T> : IModelBinder
    {
        private readonly AutumnSettings _autumnSettings;

        public QueryModelBinder(AutumnSettings autumnSettings)
        {
            _autumnSettings = autumnSettings ?? throw new ArgumentNullException(nameof(autumnSettings));
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var queryCollection = bindingContext.ActionContext.HttpContext.Request.Query;
            bindingContext.Result = ModelBindingResult.Success(Build(queryCollection));
            return Task.CompletedTask;
        }

        private static Expression<Func<T, bool>> GetOrRegistryQuery(string query,AutumnSettings autumnSettings)
        {
            lock (QueryExpressionHelper.QueriesCache)
            {
                if (string.IsNullOrWhiteSpace(query)) return QueryExpressionHelper.True<T>();
                var hash = Hash(string.Format("{0}?{1}", typeof(T).FullName, query));
                if (QueryExpressionHelper.QueriesCache.TryGetValue(hash, out Expression<Func<T, bool>> result))
                    return result;
                var antlrInputStream = new AntlrInputStream(query);
                var lexer = new QueryLexer(antlrInputStream);
                var commonTokenStream = new CommonTokenStream(lexer);
                var parser = new QueryParser(commonTokenStream);
                var or = parser.or();
                var visitor = new DefaultQueryVisitor<T>(autumnSettings.NamingStrategy);
                result = visitor.VisitOr(or);
                QueryExpressionHelper.QueriesCache.Set(hash, result);
                return result;
            }
        }
        
        /// <summary>
        /// create hash 
        /// </summary>
        /// <returns></returns>
        private static string Hash(string obj)
        {
            if (obj == null) return null;
            using (var md5 = MD5.Create())
            {
                md5.Initialize();
                md5.ComputeHash(Encoding.UTF8.GetBytes(obj));
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
        /// <param name="queryCollection"></param>
        /// <returns></returns>
        private Expression<Func<T, bool>> Build(IQueryCollection queryCollection)
        {
            var result = QueryExpressionHelper.True<T>();
            if (queryCollection.TryGetValue(_autumnSettings.QueryField, out var query))
            {
                result = GetOrRegistryQuery(query,_autumnSettings);
            }
            return result;
        }
    }
}