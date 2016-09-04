using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    public delegate void ExceptionsHandler();

    class AirlineLogic
    {
        MenuCaller _menuCaller = new MenuCaller();

        static int _year = DateTime.Now.Year;
        static int _month = DateTime.Now.Month;
        static int _day = DateTime.Now.Day;

        private string _noMatchesMessage = "No matches found!";
        private string _returnCondition = "\nPress \"Backpace\" to return to the main menu; press any key to search another flight";

        static List<Passenger> _istanbulPassengers = new List<Passenger>
        {
            new Passenger("Sergei", "Ropin", "Ukraine", "BS059862", new DateTime(1987, 06, 25), Sex.Male, new Ticket(SeatClass.Business, 400M)),
            new Passenger("Roman", "Goy", "Ukraine", "HT459863", new DateTime(1989, 06, 20), Sex.Male, new Ticket(SeatClass.Economy, 200M)),
            new Passenger("Anna", "Sidorchuk", "Ukraine", "RT8915623", new DateTime(1991, 12, 29), Sex.Female, new Ticket(SeatClass.Economy, 200M)),
            new Passenger("Masud", "Hadjivand", "Iran", "UI458255", new DateTime(1983, 02, 08), Sex.Female, new Ticket(SeatClass.Business, 450M))
        };
        static List<Passenger> _warsawPassengers = new List<Passenger>
        {
            new Passenger("Andrei", "Ivanov", "Russia", "OP8952365", new DateTime(1965, 05, 13), Sex.Male, new Ticket(SeatClass.Economy, 230M)),
            new Passenger("Oleg", "Garmash", "Ukraine", "NE4153652", new DateTime(1936, 11, 11), Sex.Male, new Ticket(SeatClass.Business, 550M)),
            new Passenger("Sarah", "Andersen", "USA", "TR15513665", new DateTime(1995, 08, 29), Sex.Female, new Ticket(SeatClass.Economy, 330M)),
            new Passenger("Taras", "Gus", "Turkey", "ER525123", new DateTime(1955, 02, 22), Sex.Female, new Ticket(SeatClass.Economy, 340M))
        };
        static List<Passenger> _odessaPassengers = new List<Passenger>
        {
            new Passenger("Alexander", "Oleinyk", "Moldova", "IK25365885", new DateTime(1993, 03, 12), Sex.Male, new Ticket(SeatClass.Economy, 130M)),
            new Passenger("Artem", "Karpenko", "Ukraine", "AS2519698", new DateTime(1991, 02, 25), Sex.Male, new Ticket(SeatClass.Economy, 150M)),
            new Passenger("Yurii", "Vashuk", "Ukraine", "RT1234567", new DateTime(1966, 08, 23), Sex.Male, new Ticket(SeatClass.Business, 300M)),
            new Passenger("Ivan", "Klimov", "USA", "AD1586947", new DateTime(1987, 06, 15), Sex.Male, new Ticket(SeatClass.Economy, 130M))
        };

        IList<Flight> _flights = new List<Flight>
        {
            { new Flight(ArrivalDeparture.Departure, "PC 753", "Kharkiv", "Istanbul", "MAU", Terminal.A, Gate.A1, FlightStatus.DeparturedAt, new DateTime(_year, _month, _day, 02, 00, 00), _istanbulPassengers) },
            { new Flight(ArrivalDeparture.Arrival, "EY 8470", "Warshaw", "Kharkiv", "MAU", Terminal.A, Gate.A3, FlightStatus.InFlight, new DateTime(_year, _month, _day, 15, 00, 00), _warsawPassengers) },
            { new Flight(ArrivalDeparture.Arrival, "PS 026", "Odessa", "Kharkiv", "MAU", Terminal.A, Gate.A1, FlightStatus.ExpectedAt, new DateTime(_year, _month, _day, 23, 15, 00), _odessaPassengers) }
        };

        /// <summary>
        /// View all available flights
        /// </summary>
        public void ViewAllFlights()
        {
            Console.Clear();
            AuxiliaryMethods.PrintColorText("\n******** VIEW FLIGHTS MENU ********", ConsoleColor.DarkCyan);

            // Print arrivals.
            AuxiliaryMethods.PrintColorText("\nARRIVAL FLIGHTS:", ConsoleColor.DarkCyan);
            foreach (var flight in _flights)
            {
                if (flight.ArrivalDeparture == ArrivalDeparture.Arrival)
                {
                    Console.WriteLine(flight);
                }
            }

            // Print departures.
            AuxiliaryMethods.PrintColorText("\nDEPARTURED FLIGHTS:", ConsoleColor.DarkCyan);
            foreach (var flight in _flights)
            {
                if (flight.ArrivalDeparture == ArrivalDeparture.Departure)
                {
                    Console.WriteLine(flight);
                }
            }
        }

        /// <summary>
        /// Search flight by the selected criterion and print an information about the one
        /// </summary>
        public void SearchFlight()
        {
            do
            {
                Console.Clear();
                AuxiliaryMethods.PrintColorText("\n******** SEARCH FLIGHT MENU ********\n", ConsoleColor.DarkCyan);
                Console.WriteLine(@"Please choose one of the following search criterions (enter a menu number):

                1. Search by number;
                2. Search by time of arrival/departure;
                3. Search by city;
                4. Search all flights in this hour;");

                Console.Write("Your choise: ");

                _menuCaller.ExceptionsHandler = CallSearchFlightMenu;
                _menuCaller.HandleExceptions();

                AuxiliaryMethods.PrintColorText(_returnCondition, ConsoleColor.DarkGreen);
            }
            while (Console.ReadKey().Key != ConsoleKey.Backspace);
        }

        private void CallSearchFlightMenu()
        {
            int index = (int)uint.Parse(Console.ReadLine());

            IDictionary<int, MenuHandler> menuItems = new Dictionary<int, MenuHandler>
            {
                { 1, SearchFlightByNumber },
                { 2, SearchFlightByTime },
                { 3, SearchFlightByCity },
                { 4, SearchFlightsInHour}
            };

            _menuCaller.MenuHandler = menuItems[index];
            _menuCaller.CallMenuItem();
        }

        private void SearchFlightByNumber()
        {
            Console.Write("\nPlease enter a number of the flight: ");
            string flightNumber = Console.ReadLine();
            AuxiliaryMethods.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);

            // Print matching flights.
            int temp = 0;
            foreach (var flight in _flights)
            {
                if (String.Equals(flightNumber.Replace(" ", string.Empty), flight.Number.Replace(" ", string.Empty),
                    StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(flight);
                    temp++;
                    break;
                }

            }
            if (temp == 0)
                Console.WriteLine(_noMatchesMessage);
        }

        private void SearchFlightByTime()
        {
            Console.WriteLine("\nSpecify the time of a flight in the following format:");
            Console.Write("Hours (from 0 to 23): ");
            int hours = (int)uint.Parse(Console.ReadLine());
            Console.Write("Minutes (from 0 to 59): ");
            int minutes = (int)uint.Parse(Console.ReadLine());

            AuxiliaryMethods.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);

            // Print matching flights.
            int temp = 0;
            foreach (var flight in _flights)
                if (hours == flight.DateTime.Hour && minutes == flight.DateTime.Minute)
                {
                    Console.WriteLine(flight);
                    temp++;
                }
            if (temp == 0)
                Console.WriteLine(_noMatchesMessage);
        }

        private void SearchFlightByCity()
        {
            Console.WriteLine("\nPlease enter an arrival/departure city:");
            string city = Console.ReadLine();
            AuxiliaryMethods.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);

            // Print matching flights.
            int temp = 0;
            foreach (var flight in _flights)
                if (String.Equals(city, flight.CityFrom, StringComparison.OrdinalIgnoreCase) |
                    String.Equals(city, flight.CityTo, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(flight);
                    temp++;
                }
            if (temp == 0)
                Console.WriteLine(_noMatchesMessage);
        }

        private void SearchFlightsInHour()
        {
            AuxiliaryMethods.PrintColorText("\nFlights in this hour: ", ConsoleColor.DarkCyan);

            // Print matching flights.
            int temp = 0;
            foreach (var flight in _flights)
                if (DateTime.Now > flight.DateTime.AddMinutes(-30) && DateTime.Now < flight.DateTime.AddMinutes(30))
                {
                    Console.WriteLine(flight);
                    temp++;
                }
            if (temp == 0)
                Console.WriteLine(_noMatchesMessage);
        }

        /// <summary>
        /// View all passengers based on the flight number
        /// </summary>
        public void ViewPassengers()
        {
            do
            {
                Console.Clear();
                AuxiliaryMethods.PrintColorText("\n******** VIEW PASSENGERS MENU ********", ConsoleColor.DarkCyan);

                Console.WriteLine("\nPlease enter a number of flight to view all passengers:");
                string flightNumber = Console.ReadLine();
                AuxiliaryMethods.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);

                int temp = 0;
                foreach (var flight in _flights)
                {
                    if (String.Equals(flightNumber.Replace(" ", string.Empty), flight.Number.Replace(" ", string.Empty),
                        StringComparison.OrdinalIgnoreCase))
                    {
                        foreach (var passenger in flight.PassengersList)
                        {
                            Console.WriteLine(passenger);
                        }
                        temp++;
                        break;
                    }
                }
                if (temp == 0)
                    Console.WriteLine(_noMatchesMessage);

                AuxiliaryMethods.PrintColorText(_returnCondition, ConsoleColor.DarkGreen);
            }
            while (Console.ReadKey().Key != ConsoleKey.Backspace);
        }

        /// <summary>
        /// Search a passenger by the selected criterion
        /// </summary>
        public void SearchPassengers()
        {
            do
            {
                Console.Clear();
                AuxiliaryMethods.PrintColorText("\n******** SEARCH PASSENGERS MENU ********\n", ConsoleColor.DarkCyan);
                Console.WriteLine(@"Please choose one of the following search criterions (enter a menu number):

                1. Search by First name or Last name;
                2. Search by Flight number;
                3. Search by Passport;");

                Console.Write("Your choise: ");

                _menuCaller.ExceptionsHandler = CallSearchPassengersMenu;
                _menuCaller.HandleExceptions();

                AuxiliaryMethods.PrintColorText(_returnCondition, ConsoleColor.DarkGreen);
            }
            while (Console.ReadKey().Key != ConsoleKey.Backspace);
        }

        private void CallSearchPassengersMenu()
        {
            int index = (int)uint.Parse(Console.ReadLine());
            IDictionary<int, MenuHandler> menuItems = new Dictionary<int, MenuHandler>
            {
                { 1, SearchPassengerByName },
                { 2, SearchPassengerByFlight },
                { 3, SearchPassengerByPassport }
            };
            _menuCaller.MenuHandler = menuItems[index];
            _menuCaller.CallMenuItem();
        }

        private void SearchPassengerByName()
        {

        }

        private void SearchPassengerByFlight()
        {

        }

        private void SearchPassengerByPassport()
        {

        }
    }
}
