using System;
using Autumn.Mvc.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Autumn.Mvc.Models.Paginations.Exceptions
{
    public abstract class PageableException : AutumnException 
    {
        public ModelBindingContext Origin { get; set; }

        protected PageableException(ModelBindingContext origin, string message, Exception innerException = null) : base(message,
            innerException)
        {
            Origin = origin;
        }
    }

}