using Measurements.BusinessLogic.Entities;

namespace Measurements.BusinessLogic.Services;

public class MeasurementsCalculationService: IMeasurementsCalculationService
{
    /// <inheritdoc/>
    public async Task<Result<int>> CalculateScoreAsync(IEnumerable<Measurement> measurements)
    {
        var scores = new List<int>();

        foreach (var measurement in measurements)
        {
            if (!NewsScales.All.TryGetValue(measurement.Type, out var scale))
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