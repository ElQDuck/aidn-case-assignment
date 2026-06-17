using NewsScore.BusinessLogic.Enums;

namespace NewsScore.BusinessLogic.Entities;

public record Measurement(MeasurementType Type, int Value);