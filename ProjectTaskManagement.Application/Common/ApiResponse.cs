using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTaskManagement.Application.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }
        public int StatusCode { get; set; }
        public static ApiResponse<T> Ok(T data, string message = "Success") => new()
        {
            Success = true,
            Data = data,
            Message = message,
            StatusCode = 200
        };
        public static ApiResponse<T> Fail(string message) => new()
        {
            Success = false,
            Message = message,
            StatusCode = 400
        };
        public static ApiResponse<T> Fail(List<string> errors) =>   new() 
        {
            Success = false,
            Message = "Validation failed",
            Errors = errors 
        };
        public static ApiResponse<T> NotFound(string message = "Not found") => new()
        {
            Success = false,
            Message = message,
            StatusCode = 404
        };

    }
}
