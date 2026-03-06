using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public sealed class NoValuePaymentException : ApplicationException
    {
        public NoValuePaymentException():
            base("Payment must have a value")
        { }
    }
}
