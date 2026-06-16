using Measurements.BusinessLogic2.Entities;

namespace Measurements.BusinessLogic2.Services;

public class MeasurementsCalculationService: IMeasurementsCalculationService
{
    /// <inheritdoc/>
    public async Task<Result<int>> CalculateScoreAsync(int temperature, int heartRate, int respiratoryRate)
    {
        return Result.FromSuccess(1);
    }
}