using Measurements.BusinessLogic.Enums;

namespace Measurements.Api.Records;

public record MeasurementRequest(MeasurementType Type, int Value);
public record CalculateNewsScoreRequest(IEnumerable<MeasurementRequest> Measurements);