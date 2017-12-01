﻿using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Autumn.Mvc.Models.Paginations.Exceptions
{
    public abstract class PageableException : AutumnException 
    {
        public ModelBindingContext Origin { get; }

        protected PageableException(ModelBindingContext origin, string message, Exception innerException = null) : base(
            message,
            innerException)
        {
            Origin = origin;
        }
    }

}