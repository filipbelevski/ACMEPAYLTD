using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Payment.API.Common.Exceptions;
using Payment.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationException = Payment.Domain.Exceptions.ApplicationException;

namespace Payment.API.Common.ExceptionHandling
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int statusCode = GetStatusCode(exception);

            var response = new
            {
                title = GetTitle(exception),
                status = statusCode,
                details = exception.Message,
                errors = GetErrors(exception)
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }

        private static object GetTitle(Exception exception)
        {
            if (exception is ApplicationException applicationException)
            {
                return applicationException.Title;
            }

            return "Server error";
        }

        private static int GetStatusCode(Exception exception)
        {
            if (exception is BadRequestException)
            {
                return StatusCodes.Status400BadRequest;
            }
            if (exception is NotFoundException)
            {
                return StatusCodes.Status404NotFound;
            }

            if ( exception is ValidationException)
            {
                return StatusCodes.Status400BadRequest;
            }

            return StatusCodes.Status500InternalServerError;

        }

        private static IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
        {
            IReadOnlyDictionary<string, string[]> errors = null;

            if (exception is ValidationException validationException)
            {
                errors = validationException.ErrorsDictionary;
            }

            return errors;
        }
    }
}
