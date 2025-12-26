using System;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.DAL.Interfaces;

namespace VideoPlatform.BLL.Managers;

public class SeriesManager(ISeriesRepository seriesRepository) : ISeriesManager
{
    private readonly ISeriesRepository _seriesRepository =
        seriesRepository ?? throw new ArgumentNullException(nameof(seriesRepository));
}