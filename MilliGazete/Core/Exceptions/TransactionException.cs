using System;

namespace Core.Exceptions
{
    public class TransactionException : Exception
    {
        public TransactionException()
        {
        }
        public TransactionException(string message) : base(message)
        {
        }
    }
}
