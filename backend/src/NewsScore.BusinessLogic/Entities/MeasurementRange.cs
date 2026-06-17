namespace NewsScore.BusinessLogic.Entities;

/// <summary>
/// The measurement range for the different measurement typse.
/// </summary>
/// <param name="From">Beginning of the range (exclusive).</param>
/// <param name="To">End of the range (inclusive).</param>
/// <param name="Score">The score of the range.</param>
public record MeasurementRange(int From, int To, int Score)
{
    /// <summary>
    /// Checks if the requested value is within the range following the logic:
    /// - All starting values are exclusive.
    /// - All ending values are inclusive.
    /// - Values outside of the defined ranges are invalid.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool Contains(int value) => value > From && value <= To;
}