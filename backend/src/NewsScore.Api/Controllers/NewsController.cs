using NewsScore.BusinessLogic.Entities;
using NewsScore.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using NewsScore.Api.Examples;
using NewsScore.Api.Records;
using Swashbuckle.AspNetCore.Filters;

namespace NewsScore.Api.Controllers;

/// <summary>
/// The REST API controller for claims.
/// </summary>
[ApiController]
public class NewsController: ControllerBase
{
    private readonly ILogger<NewsController> _logger;
    private readonly INewsScoreCalculationService _newsScoreCalculationService;
    
    /// <summary>
    /// Initializes an instance of the <see cref="NewsController"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="newsScoreCalculationService">The measurements calculation service.</param>
    public NewsController(ILogger<NewsController> logger, INewsScoreCalculationService newsScoreCalculationService)
    {
        _logger = logger;
        _newsScoreCalculationService = newsScoreCalculationService;
    }
    
    /// <summary>
    /// The route to calculate the NEWS score.
    /// </summary>
    /// <param name="request">The calculate NEWS score request. Contains TEMP, HR, RR as int values.</param>
    /// <returns>The NEWS score.</returns>
    [HttpPost("NEWS")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerRequestExample(typeof(CalculateNewsScoreRequest), typeof(CalculateNewsScoreRequestExample))]
    public async Task<ActionResult> CalculateNewsScoreAsync([FromBody] CalculateNewsScoreRequest request)
    {
        var measurements = request.Measurements.Select(m => new Measurement(m.Type, m.Value));
        var result = await _newsScoreCalculationService.CalculateScoreAsync(measurements);
        result.EnsureSuccess();
        return Ok(new NewsScoreResponse(result.Value));
    }
}