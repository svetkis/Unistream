using Unistream.TestTask.Common.Dto;

namespace Unistream.TestTask.DAL.Repositories;

public class InsertTransaction
{
    public Guid? Id { get; set; }

    public DateTime? OperationDate { get; set; }

    public decimal Amount { get; set; }
}

public interface ITransactionRepository
{
    GetTransactionDto Get(Guid id);
    GetTransactionDto Insert(InsertTransaction transaction);
}