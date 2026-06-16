using Measurements.BusinessLogic.Enums;

namespace Measurements.BusinessLogic.Entities;

public record Measurement(MeasurementType Type, int Value);