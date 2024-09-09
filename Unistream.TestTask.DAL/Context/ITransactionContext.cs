using Unistream.TestTask.DAL.Entities;

namespace Unistream.TestTask.DAL.Context;

public interface ITransactionContext
{
    IDictionary<Guid, Entity> Transactions { get; }
}