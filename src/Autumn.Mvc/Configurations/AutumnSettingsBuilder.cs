using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Autumn.Mvc.Configurations.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Autumn.Mvc.Configurations
{
    public class AutumnSettingsBuilder
    {
        private readonly AutumnSettings _autumnSettings;
        private readonly Dictionary<string, string> _fieldNames = new Dictionary<string, string>();

        private void CkeckAndRegisterFieldName(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));
            if (Regex.Match(value, @"(_)?([A-Za-z0-9]((_)?[A-Za-z0-9])*(_)?)").Value != value)
                throw new InvalidFormatFieldNameException(fieldName, value);
            var check = value.Trim();
            foreach (var item in _fieldNames.Keys)
            {
                if (item == fieldName) continue;
                if (_fieldNames[item].ToLowerInvariant() == check)
                    throw new AlreadyFieldNameUsedException(item, value);
            }
            _fieldNames[fieldName] = value.Trim();
        }

        /// <summary>
        /// class initializer 
        /// </summary>
        public AutumnSettingsBuilder()
        {
            _autumnSettings = new AutumnSettings();
        }

        /// <summary>
        /// build result
        /// </summary>
        /// <returns></returns>
        public AutumnSettings Build()
        {
            if (_fieldNames.ContainsKey("PageNumberFieldName"))
            {
                _autumnSettings.PageNumberField = _fieldNames["PageNumberFieldName"];
            }
            if (_fieldNames.ContainsKey("PageSizeFieldName"))
            {
                _autumnSettings.PageSizeField = _fieldNames["PageSizeFieldName"];
            }
            if (_fieldNames.ContainsKey("SortFieldName"))
            {
                _autumnSettings.SortField = _fieldNames["SortFieldName"];
            }
            if (_fieldNames.ContainsKey("QueryFieldName"))
            {
                _autumnSettings.QueryField = _fieldNames["QueryFieldName"];
            }
            if (_autumnSettings.NamingStrategy == null)
            {
                _autumnSettings.NamingStrategy = new DefaultNamingStrategy();
            }
            _autumnSettings.JsonSerializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = _autumnSettings.NamingStrategy
                }
            };
            return _autumnSettings;
        }
       
        /// <summary>
        /// configuration of page size
        /// </summary>
        /// <param name="pageSizeFieldName">query parameter id for page size</param>
        /// <param name="pageSize">default page size</param>
        /// <returns></returns>
        public AutumnSettingsBuilder PageSizeFieldName(string pageSizeFieldName,
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
        public AutumnSettingsBuilder PageNumberFieldName(string pageNumberFieldName)
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
        public AutumnSettingsBuilder SortFieldName(string sortFieldName)
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
        public AutumnSettingsBuilder QueryFieldName(string queryFieldName)
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
        public AutumnSettingsBuilder NamingStrategy(NamingStrategy namingStrategy)
        {
            if (namingStrategy==null)
            {
                throw new ArgumentNullException(nameof(namingStrategy));
            }
            _autumnSettings.NamingStrategy = namingStrategy;
            return this;
        }

        /// <summary>
        /// configuration of hosting Environment
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public AutumnSettingsBuilder HostingEnvironment(IHostingEnvironment hostingEnvironment)
        {
            if (hostingEnvironment==null)
            {
                throw new ArgumentNullException(nameof(hostingEnvironment));
            }
            _autumnSettings.HostingEnvironment = hostingEnvironment;
            return this;
        }
    }
}