using System.Collections.Concurrent;
using Unistream.TestTask.DAL.Entities;
namespace Unistream.TestTask.DAL.Context;

internal class TransactionContext : ITransactionContext
{
    public IDictionary<Guid, Entity> Transactions { get; } = new ConcurrentDictionary<Guid, Entity>();
}
