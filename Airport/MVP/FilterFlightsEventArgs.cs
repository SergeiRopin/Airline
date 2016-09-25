using AirportManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    public class FilteringFlightsEventArgs : EventArgs
    {
        Func<Flight, bool> _predicate;

        public FilteringFlightsEventArgs(Func<Flight, bool> predicate)
        {
            _predicate = predicate;
        }

        public Func<Flight, bool> Predicate => _predicate;
    }
}
