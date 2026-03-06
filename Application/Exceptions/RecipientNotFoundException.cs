using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    
    public sealed class RecipientNotFoundException : ApplicationException
    {
        public RecipientNotFoundException() :
            base("Recipient was not found in the database")
        { }
    }
}
