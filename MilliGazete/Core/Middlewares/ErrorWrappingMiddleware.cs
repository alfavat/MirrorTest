using Core.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace Core.Middlewares
{
    public class ErrorWrappingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorWrappingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (AuthenticationException ex)
            {
                context.Response.StatusCode = 401;
                WriteResponse(context, ex);
            }
            catch (AuthorizationException ex)
            {
                context.Response.StatusCode = 403;
                WriteResponse(context, ex);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = 422;
                WriteResponse(context, ex);
            }
            catch (TransactionException ex)
            {
                context.Response.StatusCode = 400;
                WriteResponse(context, ex);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                WriteResponse(context, ex);
            }
        }
        public async void WriteResponse(HttpContext context, Exception ex)
        {
            if (!context.Response.HasStarted)
            {
                string message = ex.Message +
                    (ex.InnerException != null ? (ex.InnerException.Message +
                    (ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : "")) : "");
                var msg = message.StringIsNullOrEmpty() ? "" : message.Replace("Validation failed:", "").Replace("--", "").Replace(":", "");
                context.Response.ContentType = "application/json; charset=utf-8";
                var response = new ApiResponse(context.Response.StatusCode, msg);
                var json = JsonConvert.SerializeObject(response);
                var txt = json + Environment.NewLine;
                using (StreamWriter w = File.AppendText("errors.txt"))
                {
                    w.WriteLine(txt);
                }
                await context.Response.WriteAsync(json);
            }
        }
    }

    public static class ErrorWrappingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorWrappingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorWrappingMiddleware>();
        }
    }

}