using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using NewsScore.Api.Controllers;
using NewsScore.Api.Records;
using NewsScore.BusinessLogic.Entities;
using NewsScore.BusinessLogic.Enums;
using NewsScore.BusinessLogic.Services;
using NSubstitute;
using NUnit.Framework;

[Category("UnitTests")]
public class NewsControllerTests
{
    private ILogger<NewsController> _loggerMock = null!;
    private INewsScoreCalculationService _newsScoreCalculationServiceMock = null!;
    private NewsController _testee = null!;

    [SetUp]
    public void SetUp()
    {
        _loggerMock = Substitute.For<ILogger<NewsController>>();
        _newsScoreCalculationServiceMock = Substitute.For<INewsScoreCalculationService>();
        _testee = new NewsController(_loggerMock, _newsScoreCalculationServiceMock);
    }

    [Test]
    public async Task NewsController_GetNewsValue_HappyPath()
    {
        // Prepare
        var returnValue = Task.FromResult(Result.FromSuccess(2));
        _newsScoreCalculationServiceMock.CalculateScoreAsync(Arg.Any<IEnumerable<Measurement>>()).Returns(returnValue);

        var request = new CalculateNewsScoreRequest([
            new MeasurementsData(MeasurementType.TEMP, 39),
            new MeasurementsData(MeasurementType.HR, 43),
            new MeasurementsData(MeasurementType.RR, 319),
        ]);
        
        // Act
        var result = await _testee.CalculateNewsScoreAsync(request);
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var ok = (OkObjectResult)result;
        var realValue = (NewsScoreResponse)ok.Value!;
        Assert.That(realValue.Score, Is.EqualTo(2));
    }
}