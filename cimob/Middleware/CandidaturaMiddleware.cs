using System.Threading.Tasks;
using cimob.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace cimob.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CandidaturaMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly ApplicationStatus _candidaturaStatus;

        public CandidaturaMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            //if (context.User.Identity.IsAuthenticated && (context.Request.Path.StartsWithSegments("TipoMobilidade") || context.Request.Path.StartsWithSegments("Application")))
            //{
                
            //    if (!_candidaturaStatus.HasCandidatura(context.User))
            //        return _next(context);
            //}

            return _next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class HasCandidaturaExtensions
    {
        public static IApplicationBuilder UseHasCandidatura(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CandidaturaMiddleware>();
        }
    }
}
