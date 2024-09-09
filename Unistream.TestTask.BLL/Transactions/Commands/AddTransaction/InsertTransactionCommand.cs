using MediatR;
using Unistream.TestTask.Common;
using Unistream.TestTask.Common.Dto;

namespace Unistream.TestTask.BLL.Transactions.Commands.AddTransaction;

public class InsertTransactionCommand : IRequest<GetTransactionDto>
{
    public InsertTransactionModel Transaction { get; set; }
}
