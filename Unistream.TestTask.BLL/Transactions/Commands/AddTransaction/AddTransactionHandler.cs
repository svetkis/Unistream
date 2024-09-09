using MediatR;
using Microsoft.Extensions.Logging;
using Unistream.TestTask.Common.Dto;
using Unistream.TestTask.DAL.Repositories;
namespace Unistream.TestTask.BLL.Transactions.Commands.AddTransaction;

public class AddTransactionHandler : IRequestHandler<InsertTransactionCommand, GetTransactionDto>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ILogger<AddTransactionHandler> _logger;

    public AddTransactionHandler(
        ITransactionRepository transactionRepository,
        ILogger<AddTransactionHandler> logger)
    {
        _transactionRepository = transactionRepository;
        _logger = logger;
    }

    public async Task<GetTransactionDto> Handle(InsertTransactionCommand request, CancellationToken cancellationToken)
    {
        InsertTransaction insertTransaction = new InsertTransaction
        {
            Amount = request.Transaction.Amount
        };

        if (request.Transaction.Id.HasValue)
        {
            insertTransaction.Id = request.Transaction.Id.Value;
        }

        if (request.Transaction.OperationDate.HasValue)
        {
            insertTransaction.OperationDate = request.Transaction.OperationDate.Value;
        }

        var result = _transactionRepository.Insert(insertTransaction);

        _logger.LogInformation($"Transaction was added: {result.Id}");
        return result;
    }
}
