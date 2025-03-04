using Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using service;
using System.Threading.Tasks;

namespace MyShop
{

    public class RatingMiddleware
    {
        private readonly RequestDelegate _next;

        public RatingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IRatingService ratingService)
        {

            Rating newRating = new()
            {
                Host = httpContext.Request.Host.Value,
                Method = httpContext.Request.Method,
                Path = httpContext.Request.Path.Value,
                Referer = httpContext.Request.Headers.Referer,
                UserAgent = httpContext.Request.Headers.UserAgent,
                RecordDate = DateTime.Now,

            };

            await ratingService.AddRating(newRating);
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RatingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRatingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RatingMiddleware>();
        }
    }
}
