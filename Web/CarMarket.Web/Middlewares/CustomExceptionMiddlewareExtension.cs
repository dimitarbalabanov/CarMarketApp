namespace CarMarket.Web.Middlewares
{
    using Microsoft.AspNetCore.Builder;

    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionHandler(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
