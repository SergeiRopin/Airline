using Airport.Exceptions;
using AirportManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportManager
{
    public class Airport : IAirportModel
    {
        public Airport()
        {
            _flights = new List<Flight>
            {
                new Flight(ArrivalDeparture.Departure, "PC 753", "Kharkiv", "Istanbul", "MAU", Terminal.A, Gate.A1, FlightStatus.DeparturedAt, new DateTime(2016, 09, 05, 02, 00, 00),
                new List<Passenger>
                {
                    new Passenger("Sergei", "Ropin", "Ukraine", "BS059862", new DateTime(1987, 06, 25), Sex.Male, new Ticket(SeatClass.Business, 400M)),
                    new Passenger("Roman", "Pupkin", "Ukraine", "HT459863", new DateTime(1989, 06, 20), Sex.Male, new Ticket(SeatClass.Economy, 200M)),
                    new Passenger("Anna", "Pupkina", "Ukraine", "RT8915623", new DateTime(1991, 12, 29), Sex.Female, new Ticket(SeatClass.Economy, 200M)),
                    new Passenger("Masud", "Pupkin", "Iran", "UI458255", new DateTime(1983, 02, 08), Sex.Female, new Ticket(SeatClass.Business, 400M))
                }),
                new Flight(ArrivalDeparture.Arrival, "EY 8470", "Warshaw", "Kharkiv", "MAU", Terminal.A, Gate.A3, FlightStatus.InFlight, new DateTime(2016, 09, 05, 15, 00, 00),
                new List<Passenger>
                {
                    new Passenger("Andrei", "Ivanov", "Russia", "OP8952365", new DateTime(1965, 05, 13), Sex.Male, new Ticket(SeatClass.Economy, 230M)),
                    new Passenger("Oleg", "Petrov", "Ukraine", "NE4153652", new DateTime(1936, 11, 11), Sex.Male, new Ticket(SeatClass.Business, 550M)),
                    new Passenger("Sarah", "Petrova", "Ukraine", "TR15513665", new DateTime(1995, 08, 29), Sex.Female, new Ticket(SeatClass.Economy, 330M)),
                    new Passenger("Taras", "Sidorov", "Russia", "ER525123", new DateTime(1955, 02, 22), Sex.Female, new Ticket(SeatClass.Economy, 340M))
                }),
                new Flight(ArrivalDeparture.Arrival, "PS 026", "Odessa", "Kharkiv", "MAU", Terminal.A, Gate.A1, FlightStatus.ExpectedAt, new DateTime(2016, 09, 05, 23, 15, 00),
                new List<Passenger>
                {
                    new Passenger("Alexander", "Alexandrov", "Moldova", "IK25365885", new DateTime(1993, 03, 12), Sex.Male, new Ticket(SeatClass.Economy, 130M)),
                    new Passenger("Artem", "Artemov", "Ukraine", "AS2519698", new DateTime(1991, 02, 25), Sex.Male, new Ticket(SeatClass.Economy, 130M)),
                    new Passenger("Yurii", "Yuriev", "Ukraine", "RT1234567", new DateTime(1966, 08, 23), Sex.Male, new Ticket(SeatClass.Business, 300M)),
                    new Passenger("Taras", "Tarasov", "Georgia", "AD1586947", new DateTime(1987, 06, 15), Sex.Male, new Ticket(SeatClass.Economy, 130M))
                })
            };
        }

        /// <summary>
        /// Collection of flights
        /// </summary>
        private IList<Flight> _flights;

        public void AddFlight(Flight flight)
        {
            if (flight != null)
            {
                if (_flights.Any(x =>            
                String.Equals(x.Number.Replace(" ", string.Empty), flight.Number.Replace(" ", string.Empty), StringComparison.OrdinalIgnoreCase)))
                {
                    throw new NotUniqueFlightNumberException("\nThe fligth number is already exists. The flight was not added!");
                }
                else _flights.Add(flight);
            }
        }

        public void DeleteFlight(Flight flight)
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

        public IEnumerable<Flight> FilterFlights(Func<Flight, bool> predicate) => _flights.Where(predicate);
    }
}
