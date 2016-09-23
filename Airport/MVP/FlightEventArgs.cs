using AirportManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    public class FlightEventArgs : EventArgs
    {
        Flight _flight;

        public FlightEventArgs(Flight flight)
        {
            _flight = flight;
        }

        public Flight Flight => _flight;
    }
}
