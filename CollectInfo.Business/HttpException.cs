using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CollectInfo.Business
{
    public class HttpException : Exception
    {
        public int StatusCode { get; }
        public HttpException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }

    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpException ex)
            {
                context.Response.StatusCode = ex.StatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(new
                {
                    error = ex.Message
                }.ToString());
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(new
                {
                    error = "An unexpected error occurred.",
                    detail = ex.Message
                }.ToString());
            }
        }
    }


}
