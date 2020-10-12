using System;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.DAL.Interfaces;

namespace VideoPlatform.BLL.Managers
{
    public class SeriesManager : ISeriesManager
    {
        private readonly ISeriesRepository _seriesRepository;

        public SeriesManager(ISeriesRepository seriesRepository)
        {
            _seriesRepository = seriesRepository ?? throw new ArgumentNullException(nameof(seriesRepository));
        }
    }
}