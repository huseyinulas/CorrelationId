using System;
using Microsoft.Extensions.Options;

namespace CorrelationId
{
    /// <inheritdoc />
    public class CorrelationContextFactory : ICorrelationContextFactory
    {
        private readonly ICorrelationContextAccessor _correlationContextAccessor;
        private readonly IOptions<CorrelationIdOptions> _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="CorrelationContextFactory" /> class.
        /// </summary>
        public CorrelationContextFactory() 
            : this(null, null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CorrelationContextFactory"/> class.
        /// </summary>
        /// <param name="correlationContextAccessor">The <see cref="ICorrelationContextAccessor"/> through which the <see cref="CorrelationContext"/> will be set.</param>
        /// <param name="options"></param>
        public CorrelationContextFactory(ICorrelationContextAccessor correlationContextAccessor, IOptions<CorrelationIdOptions> options = null)
        {
            _correlationContextAccessor = correlationContextAccessor;
            _options = options;
        }

        /// <inheritdoc />
        [Obsolete("This overload of create is depreciated. It's expected that the overload which accepts the correlation id and a header be used instead. This overload will be remove in a future release.")]
        public CorrelationContext Create(string correlationId)
        {
            var correlationContext = new CorrelationContext(correlationId, _options?.Value?.Header ?? string.Empty);

            if (_correlationContextAccessor != null)
            {
                _correlationContextAccessor.CorrelationContext = correlationContext;
            }

            return correlationContext;
        }

        /// <inheritdoc />
        public CorrelationContext Create(string correlationId, string header)
        {
            var correlationContext = new CorrelationContext(correlationId, header);

            if (_correlationContextAccessor != null)
            {
                _correlationContextAccessor.CorrelationContext = correlationContext;
            }

            return correlationContext;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_correlationContextAccessor != null)
            {
                _correlationContextAccessor.CorrelationContext = null;
            }
        }
    }
}