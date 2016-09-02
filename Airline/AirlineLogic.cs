using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    class AirlineLogic
    {
        static int year = DateTime.Now.Year;
        static int month = DateTime.Now.Month;
        static int day = DateTime.Now.Day;

        static List<Passenger> istanbulPassengers = new List<Passenger>
        {
            new Passenger("Sergei", "Ropin", "Ukraine", "BS059862", new DateTime(1987, 06, 25), Sex.Male, new Ticket(SeatClass.Business, 400M)),
            new Passenger("Roman", "Goy", "Ukraine", "HT459863", new DateTime(1989, 06, 20), Sex.Male, new Ticket(SeatClass.Economy, 200M)),
            new Passenger("Anna", "Sidorchuk", "Ukraine", "RT8915623", new DateTime(1991, 12, 29), Sex.Female, new Ticket(SeatClass.Economy, 200M)),
            new Passenger("Masud", "Hadjivand", "Iran", "UI458255", new DateTime(1983, 02, 08), Sex.Female, new Ticket(SeatClass.Business, 450M))
        };
        static List<Passenger> warsawPassengers = new List<Passenger>
        {
            new Passenger("Andrei", "Ivanov", "Russia", "OP8952365", new DateTime(1965, 05, 13), Sex.Male, new Ticket(SeatClass.Economy, 230M)),
            new Passenger("Oleg", "Garmash", "Ukraine", "NE4153652", new DateTime(1936, 11, 11), Sex.Male, new Ticket(SeatClass.Business, 550M)),
            new Passenger("Sarah", "Andersen", "USA", "TR15513665", new DateTime(1995, 08, 29), Sex.Female, new Ticket(SeatClass.Economy, 330M)),
            new Passenger("Taras", "Gus", "Turkey", "ER525123", new DateTime(1955, 02, 22), Sex.Female, new Ticket(SeatClass.Economy, 340M))
        };
        static List<Passenger> odessaPassengers = new List<Passenger>
        {
            new Passenger("Alexander", "Oleinyk", "Moldova", "IK25365885", new DateTime(1993, 03, 12), Sex.Male, new Ticket(SeatClass.Economy, 130M)),
            new Passenger("Artem", "Karpenko", "Ukraine", "AS2519698", new DateTime(1991, 02, 25), Sex.Male, new Ticket(SeatClass.Economy, 150M)),
            new Passenger("Yurii", "Vashuk", "Ukraine", "RT1234567", new DateTime(1966, 08, 23), Sex.Male, new Ticket(SeatClass.Business, 300M)),
            new Passenger("Ivan", "Klimov", "USA", "AD1586947", new DateTime(1987, 06, 15), Sex.Male, new Ticket(SeatClass.Economy, 130M))
        };

        List<Flight> flights = new List<Flight>
        {
            { new Flight(ArrivalDeparture.Departure, "PC 753", "Kharkiv", "Istanbul", "MAU", Terminal.A, Gate.A1, FlightStatus.DeparturedAt, new DateTime(year, month, day, 03, 20, 00), istanbulPassengers) },
            { new Flight(ArrivalDeparture.Arrival, "EY 8470", "Warshaw", "Kharkiv", "MAU", Terminal.A, Gate.A3, FlightStatus.InFlight, new DateTime(year, month, day, 15, 00, 00), warsawPassengers) },
            { new Flight(ArrivalDeparture.Arrival, "PS 026", "Odessa", "Kharkiv", "MAU", Terminal.A, Gate.A1, FlightStatus.ExpectedAt, new DateTime(year, month, day, 23, 15, 00), odessaPassengers) }
        };

        public void ViewAllFlights()
        {
            // Print arrivals.
            AuxiliaryClass.PrintColorText("\nARRIVAL FLIGHTS:", ConsoleColor.DarkCyan);
            foreach (var flight in flights)
            {
                if (flight.ArrivalDeparture == ArrivalDeparture.Arrival)
                {
                    Console.WriteLine(flight);
                }
            }

            // Print departures.
            AuxiliaryClass.PrintColorText("\nDEPARTURED FLIGHTS:", ConsoleColor.DarkCyan);
            foreach (var flight in flights)
            {
                if (flight.ArrivalDeparture == ArrivalDeparture.Departure)
                {
                    Console.WriteLine(flight);
                }
            }
        }

        public void ViewPassengers()
        {
            Console.WriteLine("\n");
        }

    }
}
