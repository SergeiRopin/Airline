using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    class Ticket
    {
        public FlightClass FlightClass { get; private set; }
        public double Price { get; private set; }

        public Ticket(FlightClass flightClass, double price)
        {
            FlightClass = flightClass;
            Price = price;
        }

        public Ticket() { }
    }
}
