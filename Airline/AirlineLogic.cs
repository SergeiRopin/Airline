using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    class AirlineLogic
    {
        public void DisplayFlights()
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;

            List<Passenger> istanbulPassengers = new List<Passenger>
            {
                new Passenger("Sergei", "Ropin", "Ukraine", "BS059862", new DateTime(1987, 06, 25), Sex.Male, new Ticket(SeatClass.Business, 400M)),
                new Passenger("Roman", "Goy", "Ukraine", "HT459863", new DateTime(1989, 06, 20), Sex.Male, new Ticket(SeatClass.Economy, 200M)),
                new Passenger("Anna", "Sidorchuk", "Ukraine", "RT8915623", new DateTime(1991, 12, 29), Sex.Female, new Ticket(SeatClass.Economy, 200M)),
                new Passenger("Masud", "Hadjivand", "Iran", "UI458255", new DateTime(1983, 02, 08), Sex.Female, new Ticket(SeatClass.Business, 450M))
            };

            List<Passenger> warsawPassengers = new List<Passenger>
            {
                new Passenger("Andrei", "Ivanov", "Russia", "OP8952365", new DateTime(1965, 05, 13), Sex.Male, new Ticket(SeatClass.Economy, 230M)),
                new Passenger("Oleg", "Garmash", "Ukraine", "NE4153652", new DateTime(1936, 11, 11), Sex.Male, new Ticket(SeatClass.Business, 550M)),
                new Passenger("Sarah", "Andersen", "USA", "TR15513665", new DateTime(1995, 08, 29), Sex.Female, new Ticket(SeatClass.Economy, 330M)),
                new Passenger("Taras", "Gus", "Turkey", "ER525123", new DateTime(19553, 02, 22), Sex.Female, new Ticket(SeatClass.Economy, 340M))
            };
            List<Passenger> odessaPassengers = new List<Passenger>
            {
                new Passenger("Anton", "", "Russia", "OP8952365", new DateTime(1965, 05, 13), Sex.Male, new Ticket(SeatClass.Economy, 230M)),
                new Passenger("Oleg", "Garmash", "Ukraine", "NE4153652", new DateTime(1936, 11, 11), Sex.Male, new Ticket(SeatClass.Business, 550M)),
                new Passenger("Sarah", "Andersen", "USA", "TR15513665", new DateTime(1995, 08, 29), Sex.Female, new Ticket(SeatClass.Economy, 330M)),
                new Passenger("Taras", "Gus", "Turkey", "ER525123", new DateTime(19553, 02, 22), Sex.Female, new Ticket(SeatClass.Economy, 340M))
            };

            Flight istanbul = new Flight("PC 753", "Kharkiv", "Istanbul", Terminal.A, Gate.A1, FlightStatus.DeparturedAt, new DateTime(year, month, day, 03, 20, 00), istanbulPassengers);
        }
    }
}
