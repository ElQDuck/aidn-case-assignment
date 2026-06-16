namespace Measurements.BusinessLogic.Entities;

public class MeasurementScale
{
    private readonly IReadOnlyList<MeasurementRange> _ranges;

    public MeasurementScale(IEnumerable<MeasurementRange> ranges)
        => _ranges = ranges.ToList();

    public Result<int> Evaluate(int value)
        => _ranges.FirstOrDefault(r => r.Contains(value)) is { } range
            ? Result.FromSuccess(range.Score)
            : Result.FromException<int>(new ResultException("invalid_value", $"{value} is outside valid ranges", 400));
}