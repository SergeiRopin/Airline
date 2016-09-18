using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.TemplateMethod
{
    class InvalidEntityInputException : ApplicationException
    {
        public InvalidEntityInputException(string message) : base(message) { }
    }
}
