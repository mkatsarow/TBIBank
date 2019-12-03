using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TBIBankApp.Infrastructure.Middleware
{
    public class BadRequestMiddleware
    {
        private readonly RequestDelegate next;
        public BadRequestMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await this.next.Invoke(httpContext);

            if (httpContext.Response.StatusCode == 400)
            {
                httpContext.Response.Redirect("/Home/BadRequest");
            }
            //Add other status codes here! remove 404
        }
    }
}
