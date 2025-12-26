using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VideoPlatform.DAL;
using VideoPlatform.DAL.Interfaces;
using VideoPlatform.DAL.Repositories;
using VideoPlatform.Domain.Entities;
using VideoPlatform.Domain.Enums;
using VideoPlatform.Tests.Infrastructure;

namespace VideoPlatform.Tests.UnitTests;

[TestClass]
public class ExperimentsRepositoryTests
{
    private IExperimentsRepository _experimentsRepository;

    [TestInitialize]
    public void TestInit()
    {
        var dataList = new Collection<Experiment>
        {
            new()
            {
                Id = 1,
                Name = "testExperiment",
                CreatedUserId = 1,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(1),
                Status = ExperimentStatus.Active,
                Type = ExperimentType.Content
            }
        };

        var data = dataList.AsQueryable();

        var mockSet = new Mock<DbSet<Experiment>>();

        mockSet.As<IAsyncEnumerable<Experiment>>().Setup(m => m.GetAsyncEnumerator(CancellationToken.None))
            .Returns(new TestAsyncEnumerator<Experiment>(data.GetEnumerator()));
        mockSet.As<IQueryable<Experiment>>().Setup(m => m.Provider)
            .Returns(new TestAsyncQueryProvider<Experiment>(data.Provider));
        mockSet.As<IQueryable<Experiment>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Experiment>>().Setup(m => m.ElementType).Returns(data.ElementType);
        using var enumerator = data.GetEnumerator();
        mockSet.As<IQueryable<Experiment>>().Setup(m => m.GetEnumerator()).Returns(enumerator);

        var mockContext = new Mock<VideoPlatformContext>(new DbContextOptions<VideoPlatformContext>());

        mockContext.Setup(m => m.Set<Experiment>()).Returns(mockSet.Object);

        mockContext.Setup(m => m.AddAsync(It.IsAny<Experiment>(), It.IsAny<CancellationToken>()))
            .Callback((Experiment model, CancellationToken _) =>
            {
                dataList.Add(model);
                mockSet.As<IAsyncEnumerable<Experiment>>().Setup(m => m.GetAsyncEnumerator(CancellationToken.None))
                    .Returns(new TestAsyncEnumerator<Experiment>(data.GetEnumerator()));
            })
            .Returns((Experiment _, CancellationToken _) => new ValueTask<EntityEntry<Experiment>>());

        mockContext.Setup(m => m.Update(It.IsAny<Experiment>())).Callback((Experiment model) =>
        {
            var item = dataList.FirstOrDefault(x => x.Id.Equals(model.Id));
            if (item != null)
            {
                item.Name = model.Name;
                item.CreatedUserId = model.CreatedUserId;
                item.StartDate = model.StartDate;
                item.EndDate = model.EndDate;
                item.Status = model.Status;
                item.Type = model.Type;
            }
        }).Returns<EntityState>(null!);

        mockContext.Setup(m => m.Remove(It.IsAny<Experiment>())).Callback((Experiment model) =>
        {
            var item = dataList.FirstOrDefault(x => x.Id.Equals(model.Id));
            if (item != null) dataList.Remove(item);

            mockSet.As<IAsyncEnumerable<Experiment>>().Setup(m => m.GetAsyncEnumerator(CancellationToken.None))
                .Returns(new TestAsyncEnumerator<Experiment>(data.GetEnumerator()));
        }).Returns<EntityState>(null!);

        _experimentsRepository = new ExperimentsRepository(mockContext.Object);
    }

    [TestMethod]
    public async Task GetEntityByIdTest1Async()
    {
        var result = await _experimentsRepository.GetEntityByIdAsync(1);
        Assert.IsNotNull(result);
        Assert.AreEqual("testExperiment", result.Name);
    }

    [TestMethod]
    public async Task GetEntityByIdTest2Async()
    {
        var result = await _experimentsRepository.GetEntityByIdAsync(2);
        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task GetEntityTest1Async()
    {
        var result = await _experimentsRepository.GetEntityAsync(x => x.Name.Equals("testExperiment"));
        Assert.IsNotNull(result);
        Assert.AreEqual("testExperiment", result.Name);
    }

    [TestMethod]
    public async Task GetEntityTest2Async()
    {
        var result = await _experimentsRepository.GetEntityAsync(x => x.Name.Equals("test"));
        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task GetEntitiesTest1Async()
    {
        var result = await _experimentsRepository.GetEntitiesAsync();
        Assert.IsNotNull(result);
        Assert.HasCount(1, result);
    }

    [TestMethod]
    public async Task GetEntitiesTest2Async()
    {
        var result = await _experimentsRepository.GetEntitiesAsync(x => x.Name.Equals("testExperiment"));
        Assert.IsNotNull(result);
        Assert.HasCount(1, result);
    }

    [TestMethod]
    public async Task GetEntitiesTest3Async()
    {
        var result = await _experimentsRepository.GetEntitiesAsync(x => x.Name.Equals("test"));
        Assert.IsNotNull(result);
        Assert.HasCount(0, result);
    }

    [TestMethod]
    public async Task IsEntityExistTest1Async()
    {
        var result = await _experimentsRepository.IsEntityExistAsync(new Experiment
        {
            Id = 1,
            Name = "testExperiment",
            CreatedUserId = 1,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            Status = ExperimentStatus.Active,
            Type = ExperimentType.Content
        });
        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task IsEntityExistTest2Async()
    {
        var result = await _experimentsRepository.IsEntityExistAsync(new Experiment
        {
            Id = 2,
            Name = "test",
            CreatedUserId = 1,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            Status = ExperimentStatus.Active,
            Type = ExperimentType.Content
        });
        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task CreateEntityTestAsync()
    {
        var entity = new Experiment
        {
            Id = 2,
            Name = "testExperimentNext",
            CreatedUserId = 1,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            Status = ExperimentStatus.Active,
            Type = ExperimentType.Content
        };

        var entityResult = await _experimentsRepository.CreateEntityAsync(entity);
        Assert.IsNotNull(entityResult);
        Assert.AreEqual("testExperimentNext", entityResult.Name);

        var result = await _experimentsRepository.GetEntitiesAsync();
        Assert.IsNotNull(result);
        Assert.HasCount(2, result);
    }

    [TestMethod]
    public async Task UpdateEntityTestAsync()
    {
        var entity = new Experiment
        {
            Id = 1,
            Name = "testExperimentNew",
            CreatedUserId = 1,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            Status = ExperimentStatus.Active,
            Type = ExperimentType.Content
        };

        await _experimentsRepository.UpdateEntityAsync(entity);

        var result = await _experimentsRepository.GetEntityByIdAsync(1);
        Assert.IsNotNull(result);
        Assert.AreEqual("testExperimentNew", result.Name);
    }

    [TestMethod]
    public async Task RemoveEntityTest1Async()
    {
        await _experimentsRepository.RemoveEntityAsync(1);

        var result = await _experimentsRepository.GetEntityByIdAsync(1);
        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task RemoveEntityTest2Async()
    {
        await _experimentsRepository.RemoveEntityAsync(2);

        var result = await _experimentsRepository.GetEntitiesAsync();
        Assert.IsNotNull(result);
        Assert.HasCount(1, result);
    }
}