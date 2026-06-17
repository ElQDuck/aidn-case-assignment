using NewsScore.BusinessLogic.Entities;

namespace NewsScore.BusinessLogic.Services;

public class NewsScoreCalculationService: INewsScoreCalculationService
{
    /// <inheritdoc/>
    public async Task<Result<int>> CalculateScoreAsync(IEnumerable<Measurement> measurements)
    {
        var scores = new List<int>();

        foreach (var measurement in measurements)
        {
            if (!MeasurementsScales.All.TryGetValue(measurement.Type, out var scale))
                // Could probably log the error...
                return Result.FromException<int>(
                    // TODO: Move into messages
                    new ResultException("unknown type", $"Unknown measurement type {measurement.Type}", 400));

            var result = scale.Evaluate(measurement.Value);
            if (!result.IsSuccess) return result;

            scores.Add(result.Value);
        }

        return Result.FromSuccess(scores.Sum());
    }
}