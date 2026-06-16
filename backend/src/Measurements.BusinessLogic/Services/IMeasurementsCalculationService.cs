using Measurements.BusinessLogic.Entities;

namespace Measurements.BusinessLogic.Services;

public interface IMeasurementsCalculationService
{
    /// <summary>
    /// Calculates an individual score based on provided health measurements.
    /// </summary>
    /// <param name="temperature">The body temperature of the patient.</param>
    /// <param name="heartRate">The heart rate of the patient.</param>
    /// <param name="respiratoryRate">The respiratory rate of the patient.</param>
    /// <returns>The news score.</returns>
    Task<Result<int>> CalculateScoreAsync(int temperature, int heartRate, int respiratoryRate);
}