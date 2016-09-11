﻿using System;
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
        private MenuManager _menuManager = new MenuManager();
        private Airport _airport = new Airport();

        private string _noMatchesMessage = "No matches found!";
        private string _returnToMain = "\nPress \"Space\" to return to the airline main menu; press any key to continue with the current menu";

        public void InitializeAirport()
        {
            _airport.AddFlight(new Flight(ArrivalDeparture.Departure, "PC 753", "Kharkiv", "Istanbul", "MAU", Terminal.A, Gate.A1, FlightStatus.DeparturedAt, new DateTime(2016, 09, 05, 02, 00, 00),
                new List<Passenger>
                {
                    new Passenger("Sergei", "Ropin", "Ukraine", "BS059862", new DateTime(1987, 06, 25), Sex.Male, new Ticket(SeatClass.Business, 400M)),
                    new Passenger("Roman", "Goy", "Ukraine", "HT459863", new DateTime(1989, 06, 20), Sex.Male, new Ticket(SeatClass.Economy, 200M)),
                    new Passenger("Anna", "Sidorchuk", "Ukraine", "RT8915623", new DateTime(1991, 12, 29), Sex.Female, new Ticket(SeatClass.Economy, 200M)),
                    new Passenger("Masud", "Hadjivand", "Iran", "UI458255", new DateTime(1983, 02, 08), Sex.Female, new Ticket(SeatClass.Business, 400M))
                }));
            _airport.AddFlight(new Flight(ArrivalDeparture.Arrival, "EY 8470", "Warshaw", "Kharkiv", "MAU", Terminal.A, Gate.A3, FlightStatus.InFlight, new DateTime(2016, 09, 05, 15, 00, 00),
                new List<Passenger>
                {
                    new Passenger("Andrei", "Ivanov", "Russia", "OP8952365", new DateTime(1965, 05, 13), Sex.Male, new Ticket(SeatClass.Economy, 230M)),
                    new Passenger("Oleg", "Garmash", "Ukraine", "NE4153652", new DateTime(1936, 11, 11), Sex.Male, new Ticket(SeatClass.Business, 550M)),
                    new Passenger("Sarah", "Andersen", "USA", "TR15513665", new DateTime(1995, 08, 29), Sex.Female, new Ticket(SeatClass.Economy, 330M)),
                    new Passenger("Taras", "Gus", "Turkey", "ER525123", new DateTime(1955, 02, 22), Sex.Female, new Ticket(SeatClass.Economy, 340M))
                }));
            _airport.AddFlight(new Flight(ArrivalDeparture.Arrival, "PS 026", "Odessa", "Kharkiv", "MAU", Terminal.A, Gate.A1, FlightStatus.ExpectedAt, new DateTime(2016, 09, 05, 23, 15, 00),
                new List<Passenger>
                {
                    new Passenger("Alexander", "Oleinyk", "Moldova", "IK25365885", new DateTime(1993, 03, 12), Sex.Male, new Ticket(SeatClass.Economy, 130M)),
                    new Passenger("Artem", "Karpenko", "Ukraine", "AS2519698", new DateTime(1991, 02, 25), Sex.Male, new Ticket(SeatClass.Economy, 130M)),
                    new Passenger("Yurii", "Vashuk", "Ukraine", "RT1234567", new DateTime(1966, 08, 23), Sex.Male, new Ticket(SeatClass.Business, 300M)),
                    new Passenger("Ivan", "Klimov", "USA", "AD1586947", new DateTime(1987, 06, 15), Sex.Male, new Ticket(SeatClass.Economy, 130M))
                }));
        }

        /// <summary>
        /// View all available flights
        /// </summary>
        public void ViewAllFlights()
        {
            Console.Clear();
            InputOutputHelper.PrintColorText("\n******** VIEW FLIGHTS MENU ********", ConsoleColor.DarkCyan);

            // Print arrivals.
            InputOutputHelper.PrintColorText("\nARRIVAL FLIGHTS:", ConsoleColor.DarkCyan);
            foreach (var flight in _airport.GetFlights())
            {
                if (flight.ArrivalDeparture == ArrivalDeparture.Arrival)
                {
                    Console.WriteLine(flight);
                }
            }

            // Print departures.
            InputOutputHelper.PrintColorText("\nDEPARTURED FLIGHTS:", ConsoleColor.DarkCyan);
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
        public void SearchFlights()
        {
            do
            {
                Console.Clear();
                InputOutputHelper.PrintColorText("\n******** SEARCH FLIGHT MENU ********\n", ConsoleColor.DarkCyan);
                Console.WriteLine(@"Please choose one of the following search criterions (enter a menu number):

                1. Search by number;
                2. Search by time of arrival/departure;
                3. Search by city;
                4. Search all flights in this hour;");

                Console.Write("Your choise: ");

                _menuManager.MenuHandler = ManageSearchFlightMenu;
                _menuManager.HandleExceptions();

                InputOutputHelper.PrintColorText(_returnToMain, ConsoleColor.DarkGreen);
            }
            while (Console.ReadKey().Key != ConsoleKey.Spacebar);
        }

        private void ManageSearchFlightMenu()
        {
            int index = (int)uint.Parse(Console.ReadLine());

            IDictionary<int, MenuItemHandler> menuItems = new Dictionary<int, MenuItemHandler>
            {
                { 1, SearchFlightByNumber },
                { 2, SearchFlightByTime },
                { 3, SearchFlightByCity },
                { 4, SearchFlightsInThisHour}
            };

            _menuManager.MenuItemHandler = menuItems[index];
            _menuManager.CallMenuItem();
        }

        private void SearchFlightByNumber()
        {
            Console.Write("\nPlease enter a number of the flight: ");
            string flightNumber = Console.ReadLine();
            InputOutputHelper.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);

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

            InputOutputHelper.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);

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
            InputOutputHelper.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);

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

        private void SearchFlightsInThisHour()
        {
            InputOutputHelper.PrintColorText("\nFlights in this hour: ", ConsoleColor.DarkCyan);

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
                InputOutputHelper.PrintColorText("\n******** VIEW PASSENGERS MENU ********", ConsoleColor.DarkCyan);

                Console.Write("\nPlease enter a number of flight to view all passengers: ");
                string flightNumber = Console.ReadLine();
                InputOutputHelper.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);

                int temp = 0;
                foreach (var flight in _airport.GetFlights())
                {
                    if (String.Equals(flightNumber.Replace(" ", string.Empty), flight.Number.Replace(" ", string.Empty),
                        StringComparison.OrdinalIgnoreCase))
                    {
                        foreach (var passenger in flight.Passengers)
                        {
                            Console.WriteLine(passenger);
                        }
                        temp++;
                        break;
                    }
                }
                if (temp == 0)
                    Console.WriteLine(_noMatchesMessage);

                InputOutputHelper.PrintColorText(_returnToMain, ConsoleColor.DarkGreen);
            }
            while (Console.ReadKey().Key != ConsoleKey.Spacebar);
        }

        /// <summary>
        /// Search a passenger by the selected criterion
        /// </summary>
        public void SearchPassengers()
        {
            do
            {
                Console.Clear();
                InputOutputHelper.PrintColorText("\n******** SEARCH PASSENGERS MENU ********\n", ConsoleColor.DarkCyan);
                Console.WriteLine(@"Please choose one of the following search criterions (enter a menu number):

                1. Search by Name (first or last name);
                2. Search by Flight number;
                3. Search by Passport;");

                Console.Write("Your choise: ");

                _menuManager.MenuHandler = ManageSearchPassengersMenu;
                _menuManager.HandleExceptions();

                InputOutputHelper.PrintColorText(_returnToMain, ConsoleColor.DarkGreen);
            }
            while (Console.ReadKey().Key != ConsoleKey.Spacebar);
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
            string name = InputOutputHelper.CheckStringInput("Please enter a name of the passenger: ");

            InputOutputHelper.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);

            // Print matching passengers.
            int temp = 0;
            foreach (var flight in _airport.GetFlights())
            {
                foreach (var passenger in flight.Passengers)
                {
                    if (passenger.FirstName.ToUpper().Contains(name.ToUpper()) |
                        passenger.LastName.ToUpper().Contains(name.ToUpper()))
                    {
                        Console.WriteLine($"Flight: {flight.Number}, " + passenger);
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
            InputOutputHelper.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);

            // Print matching passengers.
            int temp = 0;
            foreach (var flight in _airport.GetFlights())
            {
                foreach (var passenger in flight.Passengers)
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
        public void SearchFlightsWithLowPrice()
        {
            do
            {
                Console.Clear();
                InputOutputHelper.PrintColorText("\n******** SEARCH FLIGHTS WITH THE LOWER PRICE MENU ********", ConsoleColor.DarkCyan);

                Console.Write($"\nPlease enter a limit of the flight price (dollars): $");
                decimal priceLimit = decimal.Parse(Console.ReadLine());
                InputOutputHelper.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);

                HashSet<Flight> economyFlights = new HashSet<Flight>();
                int temp = 0;
                foreach (var flight in _airport.GetFlights())
                {
                    foreach (var passenger in flight.Passengers)
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

                InputOutputHelper.PrintColorText(_returnToMain, ConsoleColor.DarkGreen);
            }
            while (Console.ReadKey().Key != ConsoleKey.Spacebar);
        }

        /// <summary>
        /// Allows add, delete and edit flights information
        /// </summary>
        public void EditFlightsInfo()
        {
            do
            {
                Console.Clear();
                InputOutputHelper.PrintColorText("\n******** EDIT FLIGHTS MENU ********\n", ConsoleColor.DarkCyan);
                Console.WriteLine(@"Please choose one of the following items (enter a menu number):

                1. Add a flight;
                2. Delete a flight;
                3. Edit a flight.");

                Console.Write("Your choise: ");

                _menuManager.MenuHandler = ManageEditFlightsMenu;
                _menuManager.HandleExceptions();

                InputOutputHelper.PrintColorText(_returnToMain, ConsoleColor.DarkGreen);
            }
            while (Console.ReadKey().Key != ConsoleKey.Spacebar);
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

        private Flight CreateFlight()
        {
            var number = InputOutputHelper.CheckStringInput("\nEnter a number of the flight: ");

            Console.Write('\n');
            var arrivalDeparture = InputOutputHelper.CheckEnumInput<ArrivalDeparture>
                (@"Enter a flight type. Choose a number from the following list:
                1. Arrival
                2. Departure");

            string cityFrom = InputOutputHelper.CheckStringInput("\nEnter a city of departure: ");

            string cityTo = InputOutputHelper.CheckStringInput("\nEnter a city of destination: ");

            string airline = InputOutputHelper.CheckStringInput("\nEnter an airline: ");

            Console.Write('\n');
            var terminal = InputOutputHelper.CheckEnumInput<Terminal>(@"Enter a terminal of the flight. Choose a number from the following list:
                1. A
                2. B");

            Console.Write('\n');
            var gate = InputOutputHelper.CheckEnumInput<Gate>(@"Enter a gate of the flight. Choose a number from the following list:
                1. A1
                2. A2
                3. A3
                4. A4");

            Console.Write('\n');
            var status = InputOutputHelper.CheckEnumInput<FlightStatus>(@"Enter a flight status. Choose a number from the following list:
                1. CheckIn
                2. GateClosed
                3. Arrived
                4. DeparturedAt
                5. Unknown
                6. Canceled
                7. ExpectedAt
                8. Delayed
                9. InFlight
                10. Boarding");

            Console.WriteLine("\nEnter a flight time in the following format: ");
            DateTime flightTime = InputOutputHelper.CheckDateTimeInput();

            Flight createdFlight = new Flight(arrivalDeparture, number, cityFrom, cityTo, airline, terminal, gate, status, flightTime, new List<Passenger>());
            return createdFlight;
        }

        private void AddFlight()
        {
            Console.Clear();
            InputOutputHelper.PrintColorText("\n******** ADD A NEW FLIGHT MENU ********", ConsoleColor.DarkCyan);

            Flight newFlight = CreateFlight();

            _airport.AddFlight(newFlight);
            InputOutputHelper.PrintColorText($"\nFlight \"{newFlight.Number}\" was successfully added!", ConsoleColor.DarkCyan);
            InputOutputHelper.PrintColorText(newFlight.ToString(), ConsoleColor.DarkCyan);
        }

        private void DeleteFlight()
        {
            Console.Clear();
            InputOutputHelper.PrintColorText("\n******** DELETE FLIGHT MENU ********", ConsoleColor.DarkCyan);

            Console.Write("\nPlease enter a flight number: ");
            string flightNumber = Console.ReadLine();

            Flight flight = _airport.GetFlightByNumber(flightNumber);
            if (flight != null)
            {
                Console.WriteLine("\n" + flight);
                Console.Write($"\nYou want to remove the flight: {flight.Number}. Are you sure? Y/N: ");
                string confirmation;
                do
                {
                    confirmation = Console.ReadLine().ToUpper();
                    switch (confirmation)
                    {
                        case "Y":
                            _airport.RemoveFlight(flight);
                            InputOutputHelper.PrintColorText($"\nFlight {flight.Number} was successfully removed!", ConsoleColor.DarkCyan);
                            break;
                        case "N":
                            InputOutputHelper.PrintColorText("\nFlight removing canceled!", ConsoleColor.DarkCyan);
                            break;
                        default:
                            InputOutputHelper.PrintColorText("\nPlease make a choise. Y/N: ", ConsoleColor.DarkCyan);
                            break;
                    }
                } while (confirmation != "Y" & confirmation != "N");
            }
            else
                Console.WriteLine($"\n{_noMatchesMessage}");
        }

        private void EditFlight()
        {
            Console.Clear();
            InputOutputHelper.PrintColorText("\n******** EDIT FLIGHT MENU ********", ConsoleColor.DarkCyan);

            Console.Write("\nPlease enter a flight number to edit: ");
            string flightNumber = Console.ReadLine();
            Flight originalFlight = _airport.GetFlightByNumber(flightNumber);
            if (originalFlight != null)
            {
                Console.WriteLine("\nActual flight information:");
                InputOutputHelper.PrintColorText(originalFlight.ToString(), ConsoleColor.DarkCyan);

                Console.WriteLine("\nFollow the instruction to update the flight information:");
                Flight editedFlight = CreateFlight();

                _airport.EditFlight(originalFlight, editedFlight);
                InputOutputHelper.PrintColorText($"\nFlight \"{editedFlight.Number}\" was successfully updated!", ConsoleColor.DarkCyan);
                InputOutputHelper.PrintColorText(editedFlight.ToString(), ConsoleColor.DarkCyan);
            }
            else
                Console.WriteLine($"\n{_noMatchesMessage}");
        }
    }
}
