using NewsScore.BusinessLogic.Entities;

namespace NewsScore.BusinessLogic.Services;

public interface INewsScoreCalculationService
{
    /// <summary>
    /// Calculates an individual score based on provided health measurements.
    /// </summary>
    /// <param name="measurements">The measurement values of the patient.</param>
    /// <returns>The news score.</returns>
    Task<Result<int>> CalculateScoreAsync(IEnumerable<Measurement> measurements);
}