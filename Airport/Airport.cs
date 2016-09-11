using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    class Airport
    {
        private IList<Flight> _flights;

        public Airport()
        {
            _flights = new List<Flight>();
        }

        public void AddFlight(Flight flight)
        {
            if (flight != null)
                _flights.Add(flight);
        }

        public void RemoveFlight(Flight flight)
        {
            if (flight != null)
                _flights.Add(flight);
        }

        public IEnumerable<Flight> GetFlights() => _flights;

        public Flight GetFlightByNumber(string number)
        {
            Flight flight = _flights.FirstOrDefault(x =>
                String.Equals(x.Number.Replace(" ", string.Empty), number.Replace(" ", string.Empty), StringComparison.OrdinalIgnoreCase));
            return flight;
        }

        public void EditFlight(Flight actualFlight, Flight editedFlight)
        {
            int index = _flights.IndexOf(actualFlight);
            if (index != -1)
                _flights[index] = editedFlight;
        }
    }
}
