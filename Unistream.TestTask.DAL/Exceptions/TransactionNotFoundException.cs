namespace Unistream.TestTask.DAL.Exceptions;

public class TransactionNotFoundException : Exception
{
    public TransactionNotFoundException(string message) : base(message) { }
}
