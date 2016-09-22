using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport
{
    public class Airport
    {
        readonly static Airport s_instance = new Airport();

        public static Airport Instance
        {
            get
            {
                return s_instance;
            }
        }

        private Airport()
        {
            _flights = new List<Flight>();
        }

        /// <summary>
        /// Collection of flights
        /// </summary>
        private IList<Flight> _flights;

        public void AddFlight(Flight flight)
        {
            if (flight != null)
                _flights.Add(flight);
        }

        public void RemoveFlight(Flight flight)
        {
            if (flight != null)
                _flights.Remove(flight);
        }

        public void EditFlight(Flight actualFlight, Flight updatedFlight)
        {
            if (actualFlight != null && updatedFlight != null)
            {
                int index = _flights.IndexOf(actualFlight);
                if (index != -1)
                    _flights[index] = updatedFlight;
            }
        }

        public Flight GetFlightByNumber(string flightNumber)
        {
            Flight flight = _flights.FirstOrDefault(x =>
                String.Equals(x.Number.Replace(" ", string.Empty), flightNumber.Replace(" ", string.Empty), StringComparison.OrdinalIgnoreCase));
            return flight;
        }

        public IEnumerable<Flight> FilterFlights(Func<Flight, bool> predicate) => _flights.Where(predicate);
    }
}
