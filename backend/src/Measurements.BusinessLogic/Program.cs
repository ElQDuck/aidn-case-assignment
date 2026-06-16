using Measurements.BusinessLogic2.Services;

namespace Measurements.BusinessLogic2
{
    public static class MeasurementsService
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<IMeasurementsCalculationService, MeasurementsCalculationService>();
            return services;
        }
    }
}