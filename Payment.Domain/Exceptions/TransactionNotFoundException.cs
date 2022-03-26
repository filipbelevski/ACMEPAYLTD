using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Domain.Exceptions
{
    public class TransactionNotFoundException : NotFoundException
    {
        public TransactionNotFoundException(string message)
            : base(message)
        {
        }
    }
}
