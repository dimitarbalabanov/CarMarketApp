namespace CarMarket.Web.Middlewares
{
    using System;
    using System.Threading.Tasks;

    using CarMarket.Web.Infrastructure;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await this.next(httpContext);
            }
            catch (Exception ex)
            {
                await this.HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception is ArgumentNullException || context.Response.StatusCode == 404)
            {
                var result = new ViewResult
                {
                    ViewName = "~/Views/Shared/NotFound.cshtml",
                };

                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.ExecuteResultAsync(result);
                }
            }
            //else if (exception is Exception)
            //{
            //    var result = new ViewResult
            //    {
            //        ViewName = "~/Views/Shared/Forbidden.cshtml",
            //    };

            //    if (!context.Response.HasStarted)
            //    {
            //        context.Response.StatusCode = StatusCodes.Status403Forbidden;
            //        await context.ExecuteResultAsync(result);
            //    }
            //}
            else
            {
                context.Response.StatusCode = StatusCodes.Status204NoContent;
            }
        }
    }
}
