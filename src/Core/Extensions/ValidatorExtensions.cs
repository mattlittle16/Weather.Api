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