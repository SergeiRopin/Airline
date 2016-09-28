using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Exceptions
{
    public class NotUniqueFlightNumberException : ApplicationException
    {
        public NotUniqueFlightNumberException(string message) : base(message) { }
    }
}
