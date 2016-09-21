using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport
{
    public class AirportManager
    {
        readonly static AirportManager s_instance = new AirportManager();
        
        public static AirportManager Instance
        {
            get
            {
                return s_instance;
            }
        }

        private AirportManager()
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

        public IEnumerable<Flight> GetAllFlights() => _flights;

        public Flight GetFlightByNumber(string flightNumber)
        {
            Flight flight = _flights.FirstOrDefault(x =>
                String.Equals(x.Number.Replace(" ", string.Empty), flightNumber.Replace(" ", string.Empty), StringComparison.OrdinalIgnoreCase));
            return flight;
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

        public List<Passenger> GetPassengers(Flight flight)
        {
            List<Passenger> passengers = flight.Passengers;
            return passengers;
        }

        public void AddPassenger(Flight flight, Passenger passenger)
        {
            if (flight != null && passenger != null)
                _flights.FirstOrDefault(x => x == flight)
                    .Passengers.Add(passenger);
        }

        public void RemovePassenger(Flight flight, Passenger passenger)
        {
            if (flight != null)
                _flights.Where(x => x == flight).FirstOrDefault(y => y.Passengers.Remove(passenger));
        }

        public void EditPassenger(Flight flight, Passenger actualPassenger, Passenger updatedPassenger)
        {
            if (flight != null && actualPassenger != null && updatedPassenger != null)
            {
                int flightIndex = _flights.IndexOf(flight);
                if (flightIndex != -1)
                {
                    int passengerIndex = flight.Passengers.IndexOf(actualPassenger);
                    if (passengerIndex != -1)
                        flight.Passengers[passengerIndex] = updatedPassenger;
                }
            }
        }
    }
}
