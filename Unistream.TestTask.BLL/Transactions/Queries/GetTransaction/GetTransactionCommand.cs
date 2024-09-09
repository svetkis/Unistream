using MediatR;

namespace Unistream.TestTask.BLL.Transactions.Queries.GetTransaction;

public class GetTransactionCommand : IRequest<string>
{
    public string Id { get; set; }
}
