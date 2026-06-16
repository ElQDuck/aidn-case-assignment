using Measurements.Api.Examples;
using Measurements.Api.Records;
using Measurements.BusinessLogic.Entities;
using Measurements.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace Measurements.Api.Controllers;

/// <summary>
/// The REST API controller for claims.
/// </summary>
[ApiController]
public class MeasurementsController: ControllerBase
{
    private readonly ILogger<MeasurementsController> _logger;
    private readonly IMeasurementsCalculationService _measurementsCalculationService;
    
    /// <summary>
    /// Initializes an instance of the <see cref="MeasurementsController"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="measurementsCalculationService">The measurements calculation service.</param>
    public MeasurementsController(ILogger<MeasurementsController> logger, IMeasurementsCalculationService measurementsCalculationService)
    {
        _logger = logger;
        _measurementsCalculationService = measurementsCalculationService;
    }
    
    /// <summary>
    /// The route to calculate the NEWS score.
    /// </summary>
    /// <param name="temperature"></param>
    /// <param name="heartRate"></param>
    /// <param name="respiratoryRate"></param>
    /// <returns>The NEWS score.</returns>
    [HttpPost("NEWS")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerRequestExample(typeof(CalculateNewsScoreRequest), typeof(CalculateNewsScoreRequestExample))]
    public async Task<ActionResult> CalculateNewsScoreAsync([FromBody] CalculateNewsScoreRequest request)
    {
        var measurements = request.Measurements.Select(m => new Measurement(m.Type, m.Value));
        var result = await _measurementsCalculationService.CalculateScoreAsync(measurements);
        result.EnsureSuccess();
        return Ok(result.Value);
    }
}