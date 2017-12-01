using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Autumn.Mvc.Configurations.Exceptions;
using Newtonsoft.Json.Serialization;

namespace Autumn.Mvc.Configurations
{
    public class AutumnOptionsBuilder
    {

        private readonly AutumnOptions _autumnSettings;
        private readonly Dictionary<string, string> _fieldNames=new Dictionary<string, string>();

        private void CkeckAndRegisterFieldName(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));
            if (Regex.Match(value, @"(_)?([A-Za-z0-9]((_)?[A-Za-z0-9])*(_)?)").Value!=value)
                throw new AutumnInvalidFormatFieldNameException(fieldName,value);
            var check = value.Trim();
            foreach (var item in _fieldNames.Keys)
            {
                if (item == fieldName) continue;
                if (_fieldNames[item].ToLowerInvariant() == check)
                    throw new AutumnAlreadyFieldNameUsedException(item, value);
            }
            _fieldNames[fieldName] = value.Trim();
        }

        /// <summary>
        /// class initializer 
        /// </summary>
        public AutumnOptionsBuilder()
        {
            _autumnSettings = new AutumnOptions();
        }

        /// <summary>
        /// build result
        /// </summary>
        /// <returns></returns>
        public AutumnOptions Build()
        {
            if (_fieldNames.ContainsKey("PageNumberFieldName"))
            {
                _autumnSettings.PageNumberFieldName = _fieldNames["PageNumberFieldName"];
            }
            if (_fieldNames.ContainsKey("PageSizeFieldName"))
            {
                _autumnSettings.PageSizeFieldName = _fieldNames["PageSizeFieldName"];
            }
            if (_fieldNames.ContainsKey("SortFieldName"))
            {
                _autumnSettings.SortFieldName = _fieldNames["SortFieldName"];
            }
            if (_fieldNames.ContainsKey("QueryFieldName"))
            {
                _autumnSettings.QueryFieldName = _fieldNames["QueryFieldName"];
            }
            return _autumnSettings;
        }
       
        /// <summary>
        /// configuration of page size
        /// </summary>
        /// <param name="pageSizeFieldName">query parameter id for page size</param>
        /// <param name="pageSize">default page size</param>
        /// <returns></returns>
        public AutumnOptionsBuilder PageSizeFieldName(string pageSizeFieldName,
            int? pageSize = null)
        {
            
            CkeckAndRegisterFieldName(pageSizeFieldName,"PageSizeFieldName");
            return this;
        }

        /// <summary>
        /// configuration of page number
        /// </summary>
        /// <param name="pageNumberFieldName">query parameter id for page number</param>
        /// <returns></returns>
        public AutumnOptionsBuilder PageNumberFieldName(string pageNumberFieldName)
        {
            CkeckAndRegisterFieldName(pageNumberFieldName,"PageNumberFieldName");
            return this;
        }


        /// <summary>
        /// configuraion of sort field
        /// </summary>
        /// <param name="sortFieldName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public AutumnOptionsBuilder SortFieldName(string sortFieldName)
        {
            CkeckAndRegisterFieldName(sortFieldName,"SortFieldName");
            return this;
        }
        
        /// <summary>
        /// configuration of query field name
        /// </summary>
        /// <param name="queryFieldName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public AutumnOptionsBuilder QueryFieldName(string queryFieldName)
        {
            CkeckAndRegisterFieldName(queryFieldName,"QueryFieldName");
            return this;
        }

        /// <summary>
        /// configuration of naming strategy
        /// </summary>
        /// <param name="namingStrategy"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public AutumnOptionsBuilder NamingStrategy(NamingStrategy namingStrategy)
        {
            if (namingStrategy==null)
            {
                throw new ArgumentNullException(nameof(namingStrategy));
            }
            _autumnSettings.NamingStrategy = namingStrategy;
            return this;
        }
    }
}