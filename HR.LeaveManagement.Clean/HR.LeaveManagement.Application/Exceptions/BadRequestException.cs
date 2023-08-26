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

            validationErrors = new();

            foreach (var error in validationResult.Result.Errors)
            {
                validationErrors.Add(error.ErrorMessage);
            }

        }

        public List<string> validationErrors { get; set; }

    }
}
