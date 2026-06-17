using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using NewsScore.BusinessLogic.Entities;
using NewsScore.BusinessLogic.Enums;
using NewsScore.BusinessLogic.Services;
using NSubstitute;
using NUnit.Framework;

[Category("UnitTests")]
public class NewsControllerTests
{
    private NewsScoreCalculationService _testee = null!;

    [SetUp]
    public void SetUp()
    {
        _testee = new NewsScoreCalculationService();
    }

    [Test]
    public async Task NewsScoreCalculationService_CalculateScoreAsync_HappyPath()
    {
        // Prepare
        var measurement = new Measurement[]
        {
            new Measurement(MeasurementType.TEMP, 39),
            new Measurement(MeasurementType.HR, 43),
            new Measurement(MeasurementType.RR, 19),
        };
        
        // Act
        var result = await _testee.CalculateScoreAsync(measurement);
        
        // Assert
        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Value, Is.EqualTo(2));
    }

    [Test]
    public async Task NewsScoreCalculationService_CalculateScoreAsync_OutsideRange()
    {
        // Prepare
        var measurement = new Measurement[]
        {
            new Measurement(MeasurementType.TEMP, 39),
            new Measurement(MeasurementType.HR, 443),
            new Measurement(MeasurementType.RR, 19),
        };
        
        // Act
        var result = await _testee.CalculateScoreAsync(measurement);
        
        // Assert
        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.Exception, Is.InstanceOf<ResultException>());
        var ex = (ResultException)result.Exception!;
        Assert.That(ex.StatusCode, Is.EqualTo(400));
    }

    [Test]
    public async Task NewsScoreCalculationService_CalculateScoreAsync_UnknownType()
    {
        // Prepare
        var measurement = new Measurement[]
        {
            new Measurement((MeasurementType)9001, 39),
        };
        
        // Act
        var result = await _testee.CalculateScoreAsync(measurement);
        
        // Assert
        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.Exception, Is.InstanceOf<ResultException>());
        var ex = (ResultException)result.Exception!;
        Assert.That(ex.Error, Is.EqualTo("unknown type"));
        Assert.That(ex.StatusCode, Is.EqualTo(400));
    }
}