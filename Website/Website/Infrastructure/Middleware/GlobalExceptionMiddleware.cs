using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Infrastructure.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate next;
        public readonly Serilog.ILogger log;

        public GlobalExceptionMiddleware(RequestDelegate _next)
        {
            next = _next;
            log = Log.Logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {

                switch (ex)
                {

                    default:
                        // unhandled error
                        log.Error(" GlobalException:" + ex.ToString());
                        break;
                }

                throw;
            }
        }
    }
}
