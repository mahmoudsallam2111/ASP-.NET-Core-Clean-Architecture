using HR.LeaveManagement.API.Models;
using HR.LeaveManagement.Application.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using SendGrid.Helpers.Errors.Model;
using System.Net;

namespace HR.LeaveManagement.API.MiddleWare
{
    public class ExceptionMiddleware
    { 
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            // set default status code && initialize a customproblem model to display the message
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            CustomValidationProblemDetails problem = new CustomValidationProblemDetails();

            switch (ex)
            {
                case Application.Exceptions.BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    problem = new CustomValidationProblemDetails
                    {
                        Title = badRequestException.Message,
                        Status = (int)statusCode,
                        Detail = badRequestException.InnerException?.Message,
                        Type = nameof(badRequestException),
                        Errors = badRequestException.ValidationErrors
                    };
                    break;
                case Application.Exceptions.NotFoundException NotFound:
                    statusCode = HttpStatusCode.NotFound;
                    problem = new CustomValidationProblemDetails
                    {
                        Title = NotFound.Message,
                        Status = (int)statusCode,
                        Type = nameof(NotFound),
                        Detail = NotFound.InnerException?.Message,
                    };
                    break;
                default:
                    problem = new CustomValidationProblemDetails
                    {
                        Title = ex.Message,
                        Status = (int)statusCode,
                        Type = nameof(HttpStatusCode.InternalServerError),
                        Detail = ex.StackTrace,
                    };
                    break;
            }

            httpContext.Response.StatusCode = (int)statusCode;
            // to enhance logging
            var  logging_Message = JsonConvert.SerializeObject(problem);
            logger.LogError(logging_Message);
            //
            await httpContext.Response.WriteAsJsonAsync(problem);

        }
    }

}

