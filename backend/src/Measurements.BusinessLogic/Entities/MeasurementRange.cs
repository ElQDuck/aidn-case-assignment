namespace Measurements.BusinessLogic.Entities;

public record MeasurementRange(int From, int To, int Score)
{
    public bool Contains(int value) => value > From && value <= To;
}