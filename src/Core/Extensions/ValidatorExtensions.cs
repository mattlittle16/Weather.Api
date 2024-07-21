using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace Core.Extensions
{
    public static class ValidatorExtensions
    {
        public static List<dynamic> PrintErrors(this ValidationResult result)    
        {
            return result.Errors.Select(x => new { Message = x.ErrorMessage, Code = x.ErrorCode, x.AttemptedValue, x.PropertyName }).ToList<object>(); 
        }
    }
}