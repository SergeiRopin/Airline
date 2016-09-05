using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    public delegate void MenuHandler();

    class AirlineManager
    {
        MenuManager _menuManager = new MenuManager();
        Airport _airport = new Airport();

        private string _noMatchesMessage = "No matches found!";
        private string _returnToMain = "\nPress \"Backpace\" to return to the main menu; press any key to search another flight";

        /// <summary>
        /// View all available flights
        /// </summary>
        public void ViewAllFlights()
        {
            Console.Clear();
            AuxiliaryMethods.PrintColorText("\n******** VIEW FLIGHTS MENU ********", ConsoleColor.DarkCyan);

            // Print arrivals.
            AuxiliaryMethods.PrintColorText("\nARRIVAL FLIGHTS:", ConsoleColor.DarkCyan);
            foreach (var flight in _airport.GetFlights())
            {
                if (flight.ArrivalDeparture == ArrivalDeparture.Arrival)
                {
                    Console.WriteLine(flight);
                }
            }

            // Print departures.
            AuxiliaryMethods.PrintColorText("\nDEPARTURED FLIGHTS:", ConsoleColor.DarkCyan);
            foreach (var flight in _airport.GetFlights())
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

                _menuManager.MenuHandler = ManageSearchFlightMenu;
                _menuManager.HandleExceptions();

                AuxiliaryMethods.PrintColorText(_returnToMain, ConsoleColor.DarkGreen);
            }
            while (Console.ReadKey().Key != ConsoleKey.Backspace);
        }

        private void ManageSearchFlightMenu()
        {
            int index = (int)uint.Parse(Console.ReadLine());

            IDictionary<int, MenuItemHandler> menuItems = new Dictionary<int, MenuItemHandler>
            {
                { 1, SearchFlightByNumber },
                { 2, SearchFlightByTime },
                { 3, SearchFlightByCity },
                { 4, SearchFlightsInHour}
            };

            _menuManager.MenuItemHandler = menuItems[index];
            _menuManager.CallMenuItem();
        }

        private void SearchFlightByNumber()
        {
            Console.Write("\nPlease enter a number of the flight: ");
            string flightNumber = Console.ReadLine();
            AuxiliaryMethods.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);

            // Print matching flights.
            int temp = 0;
            foreach (var flight in _airport.GetFlights())
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
            foreach (var flight in _airport.GetFlights())
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
            Console.Write("\nPlease enter an arrival/departure city: ");
            string city = Console.ReadLine();
            AuxiliaryMethods.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);

            // Print matching flights.
            int temp = 0;
            foreach (var flight in _airport.GetFlights())
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
            foreach (var flight in _airport.GetFlights())
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

                Console.Write("\nPlease enter a number of flight to view all passengers: ");
                string flightNumber = Console.ReadLine();
                AuxiliaryMethods.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);

                int temp = 0;
                foreach (var flight in _airport.GetFlights())
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

                AuxiliaryMethods.PrintColorText(_returnToMain, ConsoleColor.DarkGreen);
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

                1. Search by Name (first or last name);
                2. Search by Flight number;
                3. Search by Passport;");

                Console.Write("Your choise: ");

                _menuManager.MenuHandler = ManageSearchPassengersMenu;
                _menuManager.HandleExceptions();

                AuxiliaryMethods.PrintColorText(_returnToMain, ConsoleColor.DarkGreen);
            }
            while (Console.ReadKey().Key != ConsoleKey.Backspace);
        }

        private void ManageSearchPassengersMenu()
        {
            int index = (int)uint.Parse(Console.ReadLine());
            IDictionary<int, MenuItemHandler> menuItems = new Dictionary<int, MenuItemHandler>
            {
                { 1, SearchPassengerByName },
                //{ 2, SearchPassengerByFlight },
                { 3, SearchPassengerByPassport }
            };
            _menuManager.MenuItemHandler = menuItems[index];
            _menuManager.CallMenuItem();
        }

        private void SearchPassengerByName()
        {
            Console.Write("\nPlease enter a name of the passenger: ");
            string name = Console.ReadLine();
            AuxiliaryMethods.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);

            // Print matching passengers.
            int temp = 0;
            foreach (var flight in _airport.GetFlights())
            {
                foreach (var passenger in flight.PassengersList)
                {
                    if (passenger.FirstName.ToUpper().Contains(name.ToUpper()) |
                        passenger.LastName.ToUpper().Contains(name.ToUpper()))
                    {
                        Console.WriteLine(passenger);
                        temp++;
                    }
                }
            }
            if (temp == 0)
                Console.WriteLine(_noMatchesMessage);
        }

        //private void SearchPassengerByFlight()
        //{

        //}

        private void SearchPassengerByPassport()
        {
            Console.Write("\nPlease enter a passport of the passenger: ");
            string passport = Console.ReadLine();
            AuxiliaryMethods.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);

            // Print matching passengers.
            int temp = 0;
            foreach (var flight in _airport.GetFlights())
            {
                foreach (var passenger in flight.PassengersList)
                {
                    if (passenger.Passport.ToUpper().Contains(passport.ToUpper()))
                    {
                        Console.WriteLine(passenger);
                        temp++;
                    }
                }
            }
            if (temp == 0)
                Console.WriteLine(_noMatchesMessage);
        }

        /// <summary>
        /// Search all flights with the price of economy ticket lower than user input
        /// </summary>
        public void SearchLowerPrice()
        {
            do
            {
                Console.Clear();
                AuxiliaryMethods.PrintColorText("\n******** SEARCH FLIGHTS WITH THE LOWER PRICE MENU ********", ConsoleColor.DarkCyan);

                Console.Write($"\nPlease enter a limit of the flight price (dollars): $");
                decimal priceLimit = decimal.Parse(Console.ReadLine());
                AuxiliaryMethods.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);

                HashSet<Flight> economyFlights = new HashSet<Flight>();
                int temp = 0;
                foreach (var flight in _airport.GetFlights())
                {
                    foreach (var passenger in flight.PassengersList)
                    {
                        if (priceLimit >= passenger.Ticket.Price)
                        {
                            bool isAdded = economyFlights.Add(flight);
                            if (isAdded)
                            {
                                Console.WriteLine($@"{flight}, ");
                                Console.WriteLine($"{passenger.Ticket}\n");
                            }
                            temp++;
                        }
                    }
                }
                if (temp == 0)
                    Console.WriteLine(_noMatchesMessage);

                AuxiliaryMethods.PrintColorText(_returnToMain, ConsoleColor.DarkGreen);
            }
            while (Console.ReadKey().Key != ConsoleKey.Backspace);
        }

        /// <summary>
        /// Allows add, delete and edit flights information
        /// </summary>
        public void EditFlights()
        {
            do
            {
                Console.Clear();
                AuxiliaryMethods.PrintColorText("\n******** EDIT FLIGHTS MENU ********", ConsoleColor.DarkCyan);
                Console.WriteLine(@"Please choose one of the following items (enter a menu number):

                1. Add flight;
                2. Delete flight;
                3. Edit flight.");

                Console.Write("Your choise: ");

                _menuManager.MenuHandler = ManageEditFlightsMenu;
                _menuManager.HandleExceptions();

                AuxiliaryMethods.PrintColorText(_returnToMain, ConsoleColor.DarkGreen);
            }
            while (Console.ReadKey().Key != ConsoleKey.Backspace);

        }

        private void ManageEditFlightsMenu()
        {
            int index = (int)uint.Parse(Console.ReadLine());
            IDictionary<int, MenuItemHandler> menuItems = new Dictionary<int, MenuItemHandler>
            {
                { 1, AddFlight },
                { 2, DeleteFlight },
                { 3, EditFlight }
            };
            _menuManager.MenuItemHandler = menuItems[index];
            _menuManager.CallMenuItem();
        }

        private void AddFlight()
        {
            ArrivalDeparture arrivalDeparture = default(ArrivalDeparture);
            do
            {
                Console.Write("\nEnter is a flight Arrival or Departure: ");
                arrivalDeparture = (ArrivalDeparture)Enum.Parse(typeof(ArrivalDeparture), Console.ReadLine(), true);
            } while (arrivalDeparture == ArrivalDeparture.Arrival | arrivalDeparture == ArrivalDeparture.Arrival | arrivalDeparture.ToString() == "skip");

            Console.Write("Enter a number of the flight: ");
            string number = Console.ReadLine();

            Console.Write("Enter a city of departure: ");
            string cityFrom = Console.ReadLine();

            Console.Write("Enter a city of destination: ");
            string cityTo = Console.ReadLine();

            Console.Write("Enter an airline: ");
            string airline = Console.ReadLine();

            Console.Write("Enter a terminal of the flight (\"A\", \"B\"): ");
            string terminal = Console.ReadLine();


            //_airport.AddFlight(new Flight(arrivalDeparture, number, cityFrom, cityTo, airline, terminal, gate, status, dateTime, passengersList));
        }

        private void DeleteFlight()
        {

        }

        private void EditFlight()
        {

        }
    }
}
