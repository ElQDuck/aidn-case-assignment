using Measurements.BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Measurements.BusinessLogic
{
    /// <summary>
    /// Extension methods for setting up business logic and persistence services in an <see cref="IServiceCollection"/>.
    /// </summary>
    public static class MeasurementsService
    {
        /// <summary>
        /// Adds business logic services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to add services to.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            // Register BusinessLogic services
            services.AddScoped<IMeasurementsCalculationService, MeasurementsCalculationService>();
            
            return services;
        }
    }
}