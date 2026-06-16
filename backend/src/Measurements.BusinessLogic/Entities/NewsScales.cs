using Measurements.BusinessLogic.Enums;

namespace Measurements.BusinessLogic.Entities;

public static class NewsScales
{
    public static readonly Dictionary<MeasurementType, MeasurementScale> All = new()
    {
        [MeasurementType.TEMP] = new MeasurementScale([
            new(31, 35, 3),
            new(35, 36, 1),
            new(36, 38, 0),
            new(38, 39, 1),
            new(39, 42, 2)
        ]),
        [MeasurementType.HR] = new MeasurementScale([
            new(25, 40, 3),
            new(40, 50, 1),
            new(50, 90, 0),
            new(90, 110, 1),
            new(110, 130, 2),
            new(130, 220, 3)
        ]),
        [MeasurementType.RR] = new MeasurementScale([
            new(3, 8, 3),
            new(8, 11, 1),
            new(11, 20, 0),
            new(20, 24, 2),
            new(24, 60, 3)
        ])
    };
}