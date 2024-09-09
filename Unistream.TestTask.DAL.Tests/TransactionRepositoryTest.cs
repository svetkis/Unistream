using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Unistream.TestTask.Common.Dto;
using Unistream.TestTask.DAL.Context;
using Unistream.TestTask.DAL.Entities;
using Unistream.TestTask.DAL.Exceptions;
using Unistream.TestTask.DAL.Repositories;

namespace Unistream.TestTask.DAL.Tests;

public class TransactionRepositoryTest
{
    private IServiceProvider _serviceProvider;
    private Mock<ITransactionContext> _dataContextMock;

    public TransactionRepositoryTest()
    {
        var services = new ServiceCollection();

        services.Configure<TransactionRepositoryConfig>(options =>
        {
            options.Tolerance = 0.0001m;
        });

        services.AddLogging();

        services.RegisterDataContext();

        _dataContextMock = GetTransactionContextMock();

        services.AddSingleton(_dataContextMock.Object);

        _serviceProvider = services.BuildServiceProvider();
    }

    private Mock<ITransactionContext> GetTransactionContextMock()
    {
        var entities = new Dictionary<Guid, Entity>();
        var dataContextMock = new Mock<ITransactionContext>();
        dataContextMock
            .Setup(m => m.Transactions)
            .Returns(entities);

        return dataContextMock;
    }

    [Fact]
    public void InsertTransaction()
    {
        //preparation 
        var repository = _serviceProvider.GetService<ITransactionRepository>();

        //execution 
        var result = repository.Insert(
            new InsertTransaction
            {
                Amount = 578.05m
            });

        //checks 
        _dataContextMock.Object.Transactions.Count().Should().Be(1);
        _dataContextMock.Object.Transactions.ContainsKey(result.Id).Should().BeTrue();
    }

    [Fact]
    public void GetNotExistTransaction()
    {
        //preparation 
        var repository = _serviceProvider.GetService<ITransactionRepository>();

        Assert.Throws<TransactionNotFoundException>(() => repository.Get(Guid.NewGuid()));
    }

    [Fact]
    public void GetExistTransaction()
    {
        //preparation
        var id = Guid.NewGuid();
        _dataContextMock.Object.Transactions.Add(id, new Entity { Id = id, OperationDate = DateTime.Now, Amount = 54.3234m});
        var repository = _serviceProvider.GetService<ITransactionRepository>();

        var result = repository.Get(id);
    }

    [Fact]
    public void NotDuplicatedTransactionTest()
    {
        //preparation 
        var repository = _serviceProvider.GetService<ITransactionRepository>();

        var insertData = new InsertTransaction
        {
            Id = Guid.NewGuid(),
            Amount = 58.05m
        };

        //execution 
        repository.Insert(insertData);

        Assert.Throws<ArgumentException>(() => repository.Insert(insertData));

        //checks 
        _dataContextMock.Object.Transactions.Count().Should().Be(1);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(0.0000006)]
    public void NotAcceptZeroTransactionTest(decimal value)
    {
        //preparation 
        var repository = _serviceProvider.GetService<ITransactionRepository>();

        var insertData = new InsertTransaction
        {
            Id = Guid.NewGuid(),
            Amount = value
        };

        Assert.Throws<ZeroAmountTransactionException>(() => repository.Insert(insertData));

        _dataContextMock.Object.Transactions.Count().Should().Be(0);
    }
}