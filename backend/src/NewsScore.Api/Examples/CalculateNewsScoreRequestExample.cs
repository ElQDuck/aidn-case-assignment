using NewsScore.BusinessLogic.Enums;
using NewsScore.Api.Records;
using Swashbuckle.AspNetCore.Filters;

namespace NewsScore.Api.Examples;

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