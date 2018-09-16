using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CorrelationId
{
    /// <summary>
    /// Extensions on the <see cref="IServiceCollection"/>.
    /// </summary>
    public static class CorrelationIdServiceExtensions
    {
        /// <summary>
        /// Adds required services to support the Correlation ID functionality.
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static IServiceCollection AddCorrelationId(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddCorrelationId<DefaultCorrelationIdProvider>();
        }

        /// <summary>
        /// Adds required services to support the Correlation ID functionality.
        /// </summary>
        /// <typeparam name="TProvider">The implementation type of <see cref="ICorrelationIdProvider"/> to 
        /// use for generating the correlation ID to apply.</typeparam>
        /// <param name="serviceCollection"></param>
        public static IServiceCollection AddCorrelationId<TProvider>(this IServiceCollection serviceCollection) 
            where TProvider: class, ICorrelationIdProvider
        {
            serviceCollection.TryAddSingleton<ICorrelationContextAccessor, CorrelationContextAccessor>();
            serviceCollection.TryAddTransient<ICorrelationContextFactory, CorrelationContextFactory>();
            serviceCollection.TryAddTransient(typeof(ICorrelationIdProvider), typeof(TProvider));

            return serviceCollection;
        }
    }
}
