using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoPlatform.DAL;
using VideoPlatform.DAL.Interfaces;
using VideoPlatform.DAL.Repositories;
using VideoPlatform.Domain.Entities;
using VideoPlatform.Domain.Enums;

namespace VideoPlatform.Tests.IntegrationTests;

[TestClass]
public class ExperimentsRepositoryIntegrationTests
{
    private IExperimentsRepository _experimentsRepository;

    [TestInitialize]
    public void TestInit()
    {
        var options = new DbContextOptionsBuilder<VideoPlatformContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new VideoPlatformContext(options);

        _experimentsRepository = new ExperimentsRepository(context);
    }

    [TestMethod]
    public async Task CreateEntityTestAsync()
    {
        var entity = new Experiment
        {
            Name = "testExperiment",
            CreatedUserId = 1,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            Status = ExperimentStatus.Active,
            Type = ExperimentType.Content
        };

        var entityResult = await _experimentsRepository.CreateEntityAsync(entity);
        Assert.IsNotNull(entityResult);
        Assert.AreEqual("testExperiment", entityResult.Name);

        var result = await _experimentsRepository.GetEntitiesAsync();
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Count);
    }

    [TestMethod]
    public async Task UpdateEntityTestAsync()
    {
        var entity = new Experiment
        {
            Name = "testExperiment",
            CreatedUserId = 1,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            Status = ExperimentStatus.Active,
            Type = ExperimentType.Content
        };

        var entityResult = await _experimentsRepository.CreateEntityAsync(entity);
        Assert.IsNotNull(entityResult);
        Assert.AreEqual("testExperiment", entityResult.Name);

        entityResult.Name = "testExperimentNew";
        await _experimentsRepository.UpdateEntityAsync(entityResult);

        var result = await _experimentsRepository.GetEntityByIdAsync(entityResult.Id);
        Assert.IsNotNull(result);
        Assert.AreEqual("testExperimentNew", result.Name);
    }

    [TestMethod]
    public async Task RemoveEntityAsync()
    {
        var entity = new Experiment
        {
            Name = "testExperiment",
            CreatedUserId = 1,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            Status = ExperimentStatus.Active,
            Type = ExperimentType.Content
        };

        var entityResult = await _experimentsRepository.CreateEntityAsync(entity);
        Assert.IsNotNull(entityResult);
        Assert.AreEqual("testExperiment", entityResult.Name);

        await _experimentsRepository.RemoveEntityAsync(entityResult.Id);

        var result = await _experimentsRepository.IsEntityExistAsync(entityResult);
        Assert.IsFalse(result);
    }
}