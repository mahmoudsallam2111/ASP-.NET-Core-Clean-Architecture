using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagement.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string Message) : base(Message)
        {

        }
        public BadRequestException(string Message, Task<FluentValidation.Results.ValidationResult> validationResult) : this(Message)
        {
            ValidationErrors = new Dictionary<string, string[]>();

            if (validationResult != null && validationResult.Result != null)
            {
                foreach (var error in validationResult.Result.Errors)
                {
                    if (!ValidationErrors.ContainsKey(error.PropertyName))
                    {
                        ValidationErrors.Add(error.PropertyName, new string[] { error.ErrorMessage });
                    }
                    else
                    {
                        ValidationErrors[error.PropertyName] = ValidationErrors[error.PropertyName].Concat(new string[] { error.ErrorMessage }).ToArray();
                    }
                }

            }
        }
      public IDictionary<string, string[]> ValidationErrors { get; set; }

    }
}
