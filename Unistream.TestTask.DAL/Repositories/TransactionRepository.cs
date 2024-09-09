using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Unistream.TestTask.Common.Dto;
using Unistream.TestTask.DAL.Context;
using Unistream.TestTask.DAL.Entities;
using Unistream.TestTask.DAL.Exceptions;

namespace Unistream.TestTask.DAL.Repositories;

public class TransactionRepositoryConfig
{
    public decimal Tolerance { get; set; }
}

internal class TransactionRepository : ITransactionRepository
{
    private readonly ITransactionContext _dataContext;
    private readonly TransactionRepositoryConfig _config;
    private readonly ILogger<TransactionRepository> _logger;

    public TransactionRepository(
        ITransactionContext transactionContext,
        IOptions<TransactionRepositoryConfig> options,
        ILogger<TransactionRepository> logger)
    {
        _dataContext = transactionContext;
        _config = options.Value;
        _logger = logger;
    }

    public GetTransactionDto Get(Guid id)
    {
        if (!_dataContext.Transactions.ContainsKey(id))
        {
            _logger.LogError($"Transaction {id} not found");
            throw new TransactionNotFoundException($"Transaction {id} not found");
        }

        var result = _dataContext.Transactions[id];

        return new GetTransactionDto
        {
            Id = result.Id,
            OperationDate = result.OperationDate,
            Amount = result.Amount,
        };
    }

    public GetTransactionDto Insert(InsertTransaction transaction)
    {
        if (Math.Abs(transaction.Amount) <= _config.Tolerance)
        {
            _logger.LogError($"Can't add transaction with amount smaller then: {_config.Tolerance}");
            throw new ZeroAmountTransactionException();
        }

        var entity = new Entity
        {
            Amount = transaction.Amount,
        };

        if (transaction.Id.HasValue)
        {
            entity.Id = transaction.Id.Value;
        }

        if (transaction.OperationDate.HasValue)
        {
            entity.OperationDate = transaction.OperationDate.Value;
        }

        try
        {
            _dataContext.Transactions.Add(entity.Id, entity);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Adding transaction failed: {ex.Message}");
            throw;
        }

        return new GetTransactionDto
        {
            Id = entity.Id,
            OperationDate = entity.OperationDate,
            Amount = transaction.Amount
        };
    }
}
