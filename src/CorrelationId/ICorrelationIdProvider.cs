
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;

namespace CorrelationId
{
    /// <summary>
    /// Provides a correlation ID.
    /// </summary>
    public interface ICorrelationIdProvider
    {
        /// <summary>
        /// Creates a correlation ID.
        /// </summary>
        /// <returns>A correlation ID.</returns>
        string GenerateCorrelationId(HttpContext httpContext);
    }

    public class DefaultCorrelationIdProvider : ICorrelationIdProvider
    {
        private readonly CorrelationIdOptions _options;

        public DefaultCorrelationIdProvider(IOptions<CorrelationIdOptions> options) =>
            _options = options.Value;

        public string GenerateCorrelationId(HttpContext httpContext)
        {
            var correlationIdFoundInRequestHeader = httpContext.Request.Headers.TryGetValue(_options.Header, out var correlationId);

            if (RequiresGenerationOfCorrelationId(correlationIdFoundInRequestHeader, correlationId))
                correlationId = GenerateCorrelationId(httpContext.TraceIdentifier);

            return correlationId;
        }

        private static bool RequiresGenerationOfCorrelationId(bool idInHeader, StringValues idFromHeader) =>
          !idInHeader || StringValues.IsNullOrEmpty(idFromHeader);

        private StringValues GenerateCorrelationId(string traceIdentifier) =>
            _options.UseGuidForCorrelationId || string.IsNullOrEmpty(traceIdentifier) ? Guid.NewGuid().ToString() : traceIdentifier;
    }
}