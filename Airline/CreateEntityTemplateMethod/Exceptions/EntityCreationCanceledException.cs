using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.TemplateMethod
{
    class EntityCreationCanceledException : ApplicationException
    {
        public EntityCreationCanceledException(string message) : base(message) { }
    }
}
