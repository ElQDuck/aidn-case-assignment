using NewsScore.BusinessLogic.Enums;

namespace NewsScore.Api.Records;

public record MeasurementsData(MeasurementType Type, int Value);
public record CalculateNewsScoreRequest(IEnumerable<MeasurementsData> Measurements);