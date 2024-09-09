using MediatR;
using Unistream.TestTask.Common.Dto;

namespace Unistream.TestTask.BLL.Transactions.Queries.DeserializeTransaction;

public class SerializeTransactionQuery : IRequest<string>
{
    public GetTransactionDto Dto { get; set; }
}
