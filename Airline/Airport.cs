using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    class Airport
    {
        private List<Flight> _flights = new List<Flight>();

        public void AddFlight(Flight flight) => _flights.Add(flight);

        public IList<Flight> GetFlights() => _flights;

        public Flight GetFlightByNumber(string number)
        {
            Flight flight = null;
            foreach (var item in _flights)
            {
                if (String.Equals(item.Number.Replace(" ", string.Empty), flight.Number.Replace(" ", string.Empty), StringComparison.OrdinalIgnoreCase))
                    flight = item;
            }
            return flight;
        }
    }
}
