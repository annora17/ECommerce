using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Diagnostics;
using System.Linq;

namespace EC.Presentation.Filters
{
    public class UserActivityFilter : IActionFilter
    {
        private const string CorrelationIdHeaderKey = "X-Correlation-ID";

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string correlationId = null;
            if (context.HttpContext.Request.Headers.TryGetValue(CorrelationIdHeaderKey, out StringValues correlationIds))
            {
                correlationId = correlationIds.FirstOrDefault(k => k.Equals(CorrelationIdHeaderKey));
                Debug.WriteLine($"CorrelationId from Request Header:{correlationId}");
                //_logger.LogInformation($"CorrelationId from Request Header:{ correlationId}");
            }
            else
            {
                correlationId = Guid.NewGuid().ToString();
                context.HttpContext.Request.Headers.Add(CorrelationIdHeaderKey, correlationId);
                Debug.WriteLine($"CorrelationId from Request Header:{correlationId}");
                //_logger.LogInformation($"Generated CorrelationId:{ correlationId}");
            }
        }
    }
}
