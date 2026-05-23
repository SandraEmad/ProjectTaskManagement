using System.Net;
using System.Text.Json;
using ProjectTaskManagement.Application.Common;
using ProjectTaskManagement.Domain.Exceptions;

namespace ProjectTaskManagement.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger; 

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                await WriteResponse(context, HttpStatusCode.NotFound, ex.Message);
            }
            catch (UnauthorizedException ex)
            {
                await WriteResponse(context, HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                await WriteResponse(context, HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (DomainException ex)
            {
                await WriteResponse(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred");
                await WriteResponse(context, HttpStatusCode.InternalServerError,
                    "An unexpected error occurred.");
            }
        }

        private static async Task WriteResponse(
            HttpContext context, HttpStatusCode statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = ApiResponse<object>.Fail(message);
            var json = JsonSerializer.Serialize(response,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            await context.Response.WriteAsync(json);
        }
    }
}