using Measurements.Api.Records;
using Measurements.BusinessLogic.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace Measurements.Api.Examples;

public class CalculateNewsScoreRequestExample : IExamplesProvider<CalculateNewsScoreRequest>
{
    /// <summary>
    /// An example for a successful CalculateNewsScoreRequest.
    /// </summary>
    /// <returns></returns>
    public CalculateNewsScoreRequest GetExamples() => new(
    [
        new(MeasurementType.TEMP, 39),
        new(MeasurementType.HR, 43),
        new(MeasurementType.RR, 19)
    ]);
}