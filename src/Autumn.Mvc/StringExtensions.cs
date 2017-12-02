using System.Security.Cryptography;
using System.Text;

namespace Autumn.Mvc
{
    public static class StringExtensions
    {
        /// <summary>
        /// create hash 
        /// </summary>
        /// <returns></returns>
        public static string Hash(this string obj)
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
    }
}