using MediatR;
using Microsoft.Extensions.Logging;
using Unistream.TestTask.BLL.Transactions.Commands.AddTransaction;
using Unistream.TestTask.BLL.Transactions.Queries.DeserializeTransaction;
using Unistream.TestTask.DAL.Repositories;

namespace Unistream.TestTask.BLL.Transactions.Queries.GetTransaction;

public class GetTransactionHandler : IRequestHandler<GetTransactionCommand, string>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMediator _mediator;
    private readonly ILogger<AddTransactionHandler> _logger;

    public GetTransactionHandler(
        ITransactionRepository transactionRepository,
        IMediator mediator,
        ILogger<AddTransactionHandler> logger)
    {
        _transactionRepository = transactionRepository;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<string> Handle(GetTransactionCommand request, CancellationToken cancellationToken)
    {
        bool isValidGuid = Guid.TryParse(request.Id, out var guid);

        if(!isValidGuid)
        {
            _logger.LogError($"Invalid guid value: {request.Id}");
            throw new Exception($"Invalid guid value: {request.Id}");
        }

        var dto = _transactionRepository.Get(guid);

        return await _mediator.Send(new SerializeTransactionQuery { Dto = dto });
    }
}
