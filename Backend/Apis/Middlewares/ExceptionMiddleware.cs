using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NLog;


namespace Apis.Middlewares
{
 
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); 
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "An unhandled exception occurred.");
                throw; 
            }
        }
    }

}
