using System;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.DAL.Interfaces;

namespace VideoPlatform.BLL.Managers
{
    public class ExperimentManager : IExperimentManager
    {
        private readonly IExperimentsRepository _experimentsRepository;

        public ExperimentManager(IExperimentsRepository experimentsRepository)
        {
            _experimentsRepository = experimentsRepository ?? throw new ArgumentNullException(nameof(experimentsRepository));
        }
    }
}