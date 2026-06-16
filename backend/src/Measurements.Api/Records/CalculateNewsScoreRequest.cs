using System.ComponentModel.DataAnnotations;

namespace Measurements.Api.Records;

public record CalculateNewsScoreRequest(
    [Required] int BodyTemperature,
    [Required] int HeartRate,
    [Required] int RespiratoryRate
    );