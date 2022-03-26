using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Domain.Exceptions
{
    public abstract class NotFoundException : ApplicationException
    {
        protected NotFoundException(string message)
            : base("Not Found", message)
        {
        }
    }
}
