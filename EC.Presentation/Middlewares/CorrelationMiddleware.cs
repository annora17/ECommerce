using EC.Presentation.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Serilog.Context;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EC.Presentation.Middlewares
{
    public class CorrelationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly CorrelationIdOptions _options;


        public CorrelationMiddleware(RequestDelegate next, IOptions<CorrelationIdOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _next = next ?? throw new ArgumentNullException(nameof(next));

            _options = options.Value;
        }
        public Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Headers.TryGetValue(_options.Header, out StringValues correlationId))
            {
                httpContext.TraceIdentifier = correlationId;
            }
            else
            {
                if (httpContext.Request.Cookies.TryGetValue("Track", out string trackCookie))
                {
                    correlationId = trackCookie;
                }
                else
                {
                    httpContext.Response.Cookies.Append("Track", httpContext.TraceIdentifier);
                    correlationId = httpContext.TraceIdentifier;
                }

                httpContext.Request.Headers.Add(_options.Header, correlationId);

                // logging mekanizmasına correlation id otomatik ekler.
                LogContext.PushProperty("Correlation-ID", correlationId.ToString());
            }

            // apply the correlation ID to the response header for client side tracking
            httpContext.Response.OnStarting(() =>
            {
                // Burada trace identifier kullanılmak istenilmez ise guid değeri kullanılmalıdır.
                correlationId = String.IsNullOrWhiteSpace(correlationId) ? httpContext.TraceIdentifier : correlationId;

                httpContext.Response.Headers.Add(_options.Header, correlationId); //new[] { httpContext.TraceIdentifier });

                return Task.CompletedTask;
            });

            Debug.WriteLine($"Trace : {httpContext.TraceIdentifier}");

            return _next(httpContext);
        }
    }

    public static class RequestCorrelationMiddlewareExtensions
    {
        public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<CorrelationMiddleware>();
        }

        public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app, string header)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseCorrelationId(new CorrelationIdOptions
            {
                Header = header
            });
        }

        public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app, CorrelationIdOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return app.UseMiddleware<CorrelationMiddleware>(Options.Create(options));
        }
    }
}
