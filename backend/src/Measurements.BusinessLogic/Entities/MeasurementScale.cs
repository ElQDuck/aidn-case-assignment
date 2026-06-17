namespace Measurements.BusinessLogic.Entities;

public class MeasurementScale(IEnumerable<MeasurementRange> ranges)
{
    private readonly IReadOnlyList<MeasurementRange> _ranges = ranges.ToList();

    /// <summary>
    /// Check if the value is within the allowed range.
    /// </summary>
    /// <param name="value">The provided value.</param>
    /// <returns>A result with the value or an error.</returns>
    public Result<int> Evaluate(int value)
        => _ranges.FirstOrDefault(r => r.Contains(value)) is { } range
            ? Result.FromSuccess(range.Score)
            : Result.FromException<int>(new ResultException("invalid value", $"{value} is outside valid ranges", 400));
}