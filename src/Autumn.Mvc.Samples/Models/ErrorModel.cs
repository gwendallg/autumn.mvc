using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Autumn.Mvc.Samples.Models
{
    public class ErrorModel
    {
        public List<string> Messages { get; set; }
        
        public ErrorModel(ModelStateDictionary modelState)
        {
            Messages = new List<string>();
            if (modelState == null) throw new ArgumentNullException(nameof(modelState));
            foreach (var item in modelState.Values)
            foreach (var error in item.Errors)
                Messages.Add(error.ErrorMessage);
        }

        
    }
}