﻿using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Autumn.Mvc.Models.Paginations.Exceptions
{
    public class AutumnInvalidPageSizeValueException : AutumnPageableException
    {
        public AutumnInvalidPageSizeValueException(ModelBindingContext origin,object pageSize,
            Exception innerException = null) : base(origin,
            string.Format("Invalid page size value : {0}", pageSize), innerException)
        {
        }
    }
}