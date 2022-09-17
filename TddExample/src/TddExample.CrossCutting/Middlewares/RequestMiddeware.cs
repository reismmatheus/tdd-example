using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TddExample.CrossCutting.Middlewares
{
    public class RequestMiddeware
    {
        private readonly RequestDelegate _next;
        public RequestMiddeware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                //Stopwatch watch = new Stopwatch();
                //watch.Start();
                // await _next(httpContext);
                //watch.Stop();
                //httpContext.Request.Headers.Add("x-time", watch.ElapsedMilliseconds.ToString());

                Stopwatch watch = new Stopwatch();
                watch.Start();
                httpContext.Response.OnStarting(() =>
                {
                    watch.Stop();
                    httpContext.Response.Headers.Add("x-time", watch.ElapsedMilliseconds.ToString());
                    return Task.CompletedTask;
                });
                await _next(httpContext);
                watch.Stop();
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new { message = "Error message" }));
        }
    }
}
