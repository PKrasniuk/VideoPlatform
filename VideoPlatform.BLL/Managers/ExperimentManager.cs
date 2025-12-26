using System;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.DAL.Interfaces;

namespace VideoPlatform.BLL.Managers;

public class ExperimentManager(IExperimentsRepository experimentsRepository) : IExperimentManager
{
    private readonly IExperimentsRepository _experimentsRepository =
        experimentsRepository ?? throw new ArgumentNullException(nameof(experimentsRepository));
}