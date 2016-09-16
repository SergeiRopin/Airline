using Airport;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    class AirlineManager
    {
        private MenuManager _menuManager = new MenuManager();
        private AirportManager _airport = new AirportManager();

        private string _noMatchesMessage = "No matches found!";
        private string _returnToMain = "\nPress \"Space\" to return to the airline main menu; press any key to continue with the current menu...";

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
            foreach (var flight in _airport.GetAllFlights())
            {
                if (flight.ArrivalDeparture == ArrivalDeparture.Arrival)
                {
                    Console.WriteLine(flight);
                }
            }

            // Print departures.
            InputOutputHelper.PrintColorText("\nDEPARTURED FLIGHTS:", ConsoleColor.DarkCyan);
            foreach (var flight in _airport.GetAllFlights())
            {
                if (flight.ArrivalDeparture == ArrivalDeparture.Departure)
                {
                    Console.WriteLine(flight);
                }
            }
            InputOutputHelper.PrintColorText("\nPress any key to continue...", ConsoleColor.DarkCyan);
            Console.ReadKey();
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

                Action menuHandler = ManageSearchFlightMenu;
                _menuManager.CatchMenuExceptions(menuHandler);

                InputOutputHelper.PrintColorText(_returnToMain, ConsoleColor.DarkGreen);
            }
            while (Console.ReadKey().Key != ConsoleKey.Spacebar);
        }

        private void ManageSearchFlightMenu()
        {
            int index = (int)uint.Parse(Console.ReadLine());

            IDictionary<int, Action> menuItems = new Dictionary<int, Action>
            {
                { 1, SearchFlightByNumber },
                { 2, SearchFlightByTime },
                { 3, SearchFlightByCity },
                { 4, SearchFlightsInThisHour}
            };
            Action menuItemHandler = menuItems[index];
            _menuManager.CallMenuItem(menuItemHandler);
        }

        private void SearchFlightByNumber()
        {
            Flight flight = RealizeGetFlightByNumber();
            InputOutputHelper.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);
            Console.WriteLine(flight);

            if (flight == null)
                Console.WriteLine(_noMatchesMessage);
        }

        private void SearchFlightByTime()
        {
            DateTime flightTime = InputOutputHelper.CheckDateTimeInput("\nSpecify the time of a flight in the following format:");

            // Print matching flights.
            InputOutputHelper.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);
            int temp = 0;
            _airport.GetAllFlights().ToList().ForEach(x =>
            {
                if (x.DateTime == flightTime)
                {
                    Console.WriteLine(x);
                    temp++;
                }
            });
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
            foreach (var flight in _airport.GetAllFlights())
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
            foreach (var flight in _airport.GetAllFlights())
                if (DateTime.Now > flight.DateTime.AddMinutes(-30) && DateTime.Now < flight.DateTime.AddMinutes(30))
                {
                    Console.WriteLine(flight);
                    temp++;
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

                decimal priceLimit = InputOutputHelper.CheckDecimalInput($"\nPlease enter a limit of the flight price (dollars): $");
                InputOutputHelper.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);

                HashSet<Flight> economyFlights = new HashSet<Flight>();
                int temp = 0;
                _airport.GetAllFlights().ToList()
                .ForEach(x => x.Passengers
                .ForEach(y =>
                {
                    if (priceLimit >= y.Ticket.Price && y.Ticket.SeatClass == SeatClass.Economy)
                    {
                        bool isAdded = economyFlights.Add(x);
                        if (isAdded)
                        {
                            Console.WriteLine($@"{x}, ");
                            Console.WriteLine($"{y.Ticket}\n");
                        }
                        temp++;
                    }
                }));
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
                Console.WriteLine(@"To find the passenger please choose one of the following search criterions (enter a menu number):

                1. Search by Name (first or last name);
                2. Search by Flight number;
                3. Search by Passport;");
                Console.Write("Your choise: ");

                Action menuHandler = ManageSearchPassengersMenu;
                _menuManager.CatchMenuExceptions(menuHandler);

                InputOutputHelper.PrintColorText(_returnToMain, ConsoleColor.DarkGreen);
            }
            while (Console.ReadKey().Key != ConsoleKey.Spacebar);
        }

        private void ManageSearchPassengersMenu()
        {
            int index = (int)uint.Parse(Console.ReadLine());
            IDictionary<int, Action> menuItems = new Dictionary<int, Action>
            {
                { 1, SearchPassengerByName },
                { 2, SearchPassengersByFlightNumber },
                { 3, SearchPassengerByPassport }
            };
            Action menuItemHandler = menuItems[index];
            _menuManager.CallMenuItem(menuItemHandler);
        }

        private void SearchPassengerByName()
        {
            string name = InputOutputHelper.CheckStringInput("\nPlease enter a name of the passenger: ");
            InputOutputHelper.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);

            // Print matching passengers.
            int temp = 0;
            _airport.GetAllFlights().ToList()
                .ForEach(x => x.Passengers
                .ForEach(y =>
                {
                    if (y.FirstName.ToUpper().Contains(name.ToUpper()) |
                        y.LastName.ToUpper().Contains(name.ToUpper()))
                    {
                        Console.WriteLine($"Flight: {x.Number}, " + y);
                        temp++;
                    }
                }));
            if (temp == 0)
                Console.WriteLine(_noMatchesMessage);
        }

        public void SearchPassengersByFlightNumber()
        {
            Console.Write("\nPlease enter a number of flight to view all passengers: ");
            string flightNumber = Console.ReadLine();
            InputOutputHelper.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);

            Flight flight = _airport.GetFlightByNumber(flightNumber);
            if (flight != null)
                _airport.GetPassengers(flight).ForEach(x => Console.WriteLine($"Flight: {flight.Number}, " + x));
            else Console.WriteLine(_noMatchesMessage);
        }

        private void SearchPassengerByPassport()
        {
            string passport = InputOutputHelper.CheckStringInput("\nPlease enter a passport of the passenger: ");
            InputOutputHelper.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);

            // Print matching passengers.
            int temp = 0;
            _airport.GetAllFlights().ToList()
                .ForEach(x => x.Passengers
                .ForEach(y =>
                {
                    if (y.Passport.ToUpper().Contains(passport.ToUpper()))
                    {
                        Console.WriteLine($"Flight: {x.Number}, " + y);
                        temp++;
                    }
                }));
            if (temp == 0)
                Console.WriteLine(_noMatchesMessage);
        }

        /// <summary>
        /// Allows add, delete and edit flight's information
        /// </summary>
        public void EditFlightsInfo()
        {
            do
            {
                Console.Clear();
                InputOutputHelper.PrintColorText("\n******** EDIT FLIGHTS INFORMATION MENU ********\n", ConsoleColor.DarkCyan);
                Console.WriteLine(@"Please choose one of the following items (enter a menu number):

                1. Add a flight;
                2. Delete a flight;
                3. Edit a flight's information.");

                Console.Write("Your choise: ");

                Action menuHandler = ManageEditFlightsInfoMenu;
                _menuManager.CatchMenuExceptions(menuHandler);

                InputOutputHelper.PrintColorText(_returnToMain, ConsoleColor.DarkGreen);
            }
            while (Console.ReadKey().Key != ConsoleKey.Spacebar);
        }

        private void ManageEditFlightsInfoMenu()
        {
            int index = (int)uint.Parse(Console.ReadLine());
            IDictionary<int, Action> menuItems = new Dictionary<int, Action>
            {
                { 1, AddFlight },
                { 2, DeleteFlight },
                { 3, EditFlight }
            };
            Action menuItemHandler = menuItems[index];
            _menuManager.CallMenuItem(menuItemHandler);
        }

        private Flight CreateFlight()
        {
            string number = InputOutputHelper.CheckStringInput("\nEnter a number of the flight: ");

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

            //Console.WriteLine("\nEnter a flight time in the following format: ");
            DateTime flightTime = InputOutputHelper.CheckDateTimeInput("\nEnter a flight time in the following format: ");

            Flight createdFlight = new Flight(arrivalDeparture, number, cityFrom, cityTo, airline, terminal, gate, status, flightTime, new List<Passenger>());
            return createdFlight;
        }

        private Flight RealizeGetFlightByNumber()
        {
            Console.Write("\nPlease enter a flight number: ");
            string flightNumber = Console.ReadLine();
            Flight flight = _airport.GetFlightByNumber(flightNumber);
            return flight;
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

            Flight flight = RealizeGetFlightByNumber();
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
            else Console.WriteLine($"\n{_noMatchesMessage}");
        }

        private void EditFlight()
        {
            Console.Clear();
            InputOutputHelper.PrintColorText("\n******** EDIT FLIGHT MENU ********", ConsoleColor.DarkCyan);

            Flight flight = RealizeGetFlightByNumber();
            if (flight != null)
            {
                Console.WriteLine("\nActual fligth information:");
                InputOutputHelper.PrintColorText(flight.ToString(), ConsoleColor.DarkCyan);

                Console.WriteLine("\nFollow the instruction to update the flight information:");

                //Edit flight number.
                InputOutputHelper.PrintColorText($"\nActual flight number: {flight.Number}", ConsoleColor.DarkCyan);
                Func<string, string> flightNumberHandler = InputOutputHelper.CheckStringInput;
                string number = EditFlightHelper(flightNumberHandler, "\nEnter a number of the flight: ");

                //Edit arrival/departure info.
                InputOutputHelper.PrintColorText($"\nActual flight type: {flight.ArrivalDeparture}", ConsoleColor.DarkCyan);
                Func<string, ArrivalDeparture> arrivalDepartureHandler = InputOutputHelper.CheckEnumInput<ArrivalDeparture>;
                string arrivalDepartureMessage = "\n" + @"Enter a flight type. Choose a number from the following list:
                1. Arrival
                2. Departure";
                var arrivalDeparture = EditFlightHelper(arrivalDepartureHandler, arrivalDepartureMessage);

                //Edit city of departure.
                InputOutputHelper.PrintColorText($"\nActual departure city: {flight.CityFrom}", ConsoleColor.DarkCyan);
                Func<string, string> cityFromHandler = InputOutputHelper.CheckStringInput;
                string cityFrom = EditFlightHelper(cityFromHandler, "\nEnter a city of departure: ");

                //Edit city of departure.
                InputOutputHelper.PrintColorText($"\nActual arrival city: {flight.CityTo}", ConsoleColor.DarkCyan);
                Func<string, string> cityToHandler = InputOutputHelper.CheckStringInput;
                string cityTo = EditFlightHelper(cityToHandler, "\nEnter a city of arrival: ");

                //Edit airline.
                InputOutputHelper.PrintColorText($"\nActual airline: {flight.Airline}", ConsoleColor.DarkCyan);
                Func<string, string> airlineHandler = InputOutputHelper.CheckStringInput;
                string airline = EditFlightHelper(airlineHandler, "\nEnter an airline: ");

                //Edit terminal.
                InputOutputHelper.PrintColorText($"\nActual terminal: {flight.Terminal}", ConsoleColor.DarkCyan);
                Func<string, Terminal> terminalHandler = InputOutputHelper.CheckEnumInput<Terminal>;
                string terminalMessage = "\n" + @"Enter a terminal of the flight. Choose a number from the following list:
                1. A
                2. B";
                var terminal = EditFlightHelper(terminalHandler, terminalMessage);

                //Edit gate.
                InputOutputHelper.PrintColorText($"\nActual gate: {flight.Gate}", ConsoleColor.DarkCyan);
                Func<string, Gate> gateHandler = InputOutputHelper.CheckEnumInput<Gate>;
                string gateMessage = "\n" + @"Enter a gate of the flight. Choose a number from the following list:
                1. A1
                2. A2
                3. A3
                4. A4";
                var gate = EditFlightHelper(gateHandler, gateMessage);

                //Edit flight status.
                InputOutputHelper.PrintColorText($"\nActual flight status: {flight.Status}", ConsoleColor.DarkCyan);
                Func<string, FlightStatus> statusHandler = InputOutputHelper.CheckEnumInput<FlightStatus>;
                string statusMessage = "\n" + @"Enter a flight status. Choose a number from the following list:
                1. CheckIn
                2. GateClosed
                3. Arrived
                4. DeparturedAt
                5. Unknown
                6. Canceled
                7. ExpectedAt
                8. Delayed
                9. InFlight
                10. Boarding";
                var status = EditFlightHelper(statusHandler, statusMessage);

                //Edit fligth time.
                InputOutputHelper.PrintColorText($"\nActual flight date and time: {flight.DateTime}", ConsoleColor.DarkCyan);
                Func<string, DateTime> dateTimeHandler = InputOutputHelper.CheckDateTimeInput;
                DateTime dateTime = EditFlightHelper(dateTimeHandler, "\nEnter a flight time in the following format: ");
                
                Flight updatedFlight = new Flight(arrivalDeparture, number,  cityFrom, cityTo, airline, terminal, gate, status, dateTime, new List<Passenger>());
            }
            else Console.WriteLine($"\n{_noMatchesMessage}");









            //Flight actualFlight = RealizeGetFlightByNumber();
            //if (actualFlight != null)
            //{
            //    Console.WriteLine("\nActual flight information:");
            //    InputOutputHelper.PrintColorText(actualFlight.ToString(), ConsoleColor.DarkCyan);

            //    Console.WriteLine("\nFollow the instruction to update the flight information:");
            //    Flight updatedFlight = CreateFlight();

            //    _airport.EditFlight(actualFlight, updatedFlight);
            //    InputOutputHelper.PrintColorText($"\nFlight \"{updatedFlight.Number}\" was successfully updated!", ConsoleColor.DarkCyan);
            //    InputOutputHelper.PrintColorText(updatedFlight.ToString(), ConsoleColor.DarkCyan);
            //}
            //else Console.WriteLine($"\n{_noMatchesMessage}");
        }

        /// <summary>
        /// Allows add, delete and edit passenger's information
        /// </summary>
        public void EditPassengersInfo()
        {
            do
            {
                Console.Clear();
                InputOutputHelper.PrintColorText("\n******** EDIT PASSENGERS INFORMATION MENU ********\n", ConsoleColor.DarkCyan);
                Console.WriteLine(@"Please choose one of the following menu items (enter a menu number):

                1. Add a passenger;
                2. Delete a passenger;
                3. Edit a passenger's information.");

                Console.Write("Your choise: ");

                Action menuHandler = ManageEditPassengersInfoMenu;
                _menuManager.CatchMenuExceptions(menuHandler);

                InputOutputHelper.PrintColorText(_returnToMain, ConsoleColor.DarkGreen);
            }
            while (Console.ReadKey().Key != ConsoleKey.Spacebar);
        }

        private void ManageEditPassengersInfoMenu()
        {
            int index = (int)uint.Parse(Console.ReadLine());
            IDictionary<int, Action> menuItems = new Dictionary<int, Action>
            {
                { 1, AddPassenger },
                { 2, DeletePassenger },
                { 3, EditPassenger }
            };
            Action menuItemHandler = menuItems[index];
            _menuManager.CallMenuItem(menuItemHandler);
        }

        public Passenger CreatePassenger()
        {
            string firstName = InputOutputHelper.CheckStringInput("\nEnter a first name: ");

            string lastName = InputOutputHelper.CheckStringInput("\nEnter a last name: ");

            string nationality = InputOutputHelper.CheckStringInput("\nEnter a nationalty: ");

            string passport = InputOutputHelper.CheckStringInput("\nEnter a passport: ");

            Console.Write('\n');
            var sex = InputOutputHelper.CheckEnumInput<Sex>(@"Enter a sex of the passenger. Choose a number from the following list:
                1. Male
                2. Female");

            //Console.WriteLine("\nEnter a passenger birthday: ");
            DateTime birthday = InputOutputHelper.CheckDateTimeInput("\nEnter a passenger birthday: ");

            InputOutputHelper.PrintColorText("\nPassenger information successfully filled. Enter a ticket information.", ConsoleColor.DarkCyan);

            Console.Write('\n');
            var seatClass = InputOutputHelper.CheckEnumInput<SeatClass>(@"Enter a seat class. Choose a number from the following list:
                1. Economy
                2. Business");

            decimal price = InputOutputHelper.CheckDecimalInput("\nEnter a ticket price (dollars): ");

            Passenger createdFlight = new Passenger(firstName, lastName, nationality, passport, birthday, sex, new Ticket(seatClass, price));
            return createdFlight;

        }

        public Passenger GetPassengerByPassport(Flight flight)
        {
            Passenger passenger = null;
            if (flight != null)
            {
                Console.Write("\nPlease enter a passenger passport: ");
                string passport = Console.ReadLine();
                passenger = flight.Passengers
                    .FirstOrDefault(x => String.Equals(x.Passport, passport, StringComparison.OrdinalIgnoreCase));
            }
            return passenger;
        }

        private void AddPassenger()
        {
            Console.Clear();
            InputOutputHelper.PrintColorText("\n******** ADD A NEW PASSENGER MENU ********", ConsoleColor.DarkCyan);

            Flight flight = RealizeGetFlightByNumber();
            if (flight != null)
            {
                InputOutputHelper.PrintColorText("\nFill an information about passenger:", ConsoleColor.DarkCyan);
                Passenger passenger = CreatePassenger();

                _airport.AddPassenger(flight, passenger);

                InputOutputHelper.PrintColorText($"\nPassenger was successfully added to the flight \"{flight.Number}\"!", ConsoleColor.DarkCyan);
                InputOutputHelper.PrintColorText(passenger.ToString(), ConsoleColor.DarkCyan);
            }
            else Console.WriteLine($"\n{_noMatchesMessage}");
        }

        private void DeletePassenger()
        {
            Console.Clear();
            InputOutputHelper.PrintColorText("\n******** DELETE PASSENGER MENU ********", ConsoleColor.DarkCyan);

            Flight flight = RealizeGetFlightByNumber();
            Passenger passenger = GetPassengerByPassport(flight);

            if (flight != null && passenger != null)
            {
                InputOutputHelper.PrintColorText($"\nFlight: {flight.Number}, {passenger}", ConsoleColor.DarkCyan);
                Console.Write($"\nYou want to remove the passenger: {passenger.FirstName} {passenger.LastName} . Are you sure? Y/N: ");
                string confirmation;
                do
                {
                    confirmation = Console.ReadLine().ToUpper();
                    switch (confirmation)
                    {
                        case "Y":
                            _airport.RemovePassenger(flight, passenger);
                            InputOutputHelper.PrintColorText($"\nPassenger was successfully removed!", ConsoleColor.DarkCyan);
                            break;
                        case "N":
                            InputOutputHelper.PrintColorText("\nPassenger removing canceled!", ConsoleColor.DarkCyan);
                            break;
                        default:
                            InputOutputHelper.PrintColorText("\nPlease make a choise. Y/N: ", ConsoleColor.DarkCyan);
                            break;
                    }
                } while (confirmation != "Y" & confirmation != "N");
            }
            else Console.WriteLine($"\n{_noMatchesMessage}");
        }

        private void EditPassenger()
        {
            Console.Clear();
            InputOutputHelper.PrintColorText("\n******** EDIT PASSENGER MENU ********", ConsoleColor.DarkCyan);

            Flight flight = RealizeGetFlightByNumber();
            Passenger actualPassenger = GetPassengerByPassport(flight);

            if (flight != null && actualPassenger != null)
            {
                Console.WriteLine("\nActual passenger information:");
                InputOutputHelper.PrintColorText($"Flight: {flight.Number}, {actualPassenger}", ConsoleColor.DarkCyan);

                Console.WriteLine("\nUpdate an information about the passenger:");
                Passenger updatedPassenger = CreatePassenger();

                _airport.EditPassenger(flight, actualPassenger, updatedPassenger);

                InputOutputHelper.PrintColorText($"\nPassenger information was successfully updated!\n", ConsoleColor.DarkCyan);
                InputOutputHelper.PrintColorText(updatedPassenger.ToString(), ConsoleColor.DarkCyan);
            }
            else Console.WriteLine($"\n{_noMatchesMessage}");
        }







        public T EditFlightHelper<T>(Func<string, T> flightHandler, string infoMessage)
        {
            T output = default(T);
            string key;
            do
            {
                Console.Write("To change actual value press \"C\" button, to keep the default value press \"Enter\": ");
                key = Console.ReadKey().Key.ToString().ToUpper();
                switch (key)
                {
                    case "C":
                        output = flightHandler.Invoke(infoMessage);
                        InputOutputHelper.PrintColorText($"Information has been updated!", ConsoleColor.DarkCyan);
                        break;
                    case "ENTER":
                        InputOutputHelper.PrintColorText("\nDefault information has been kept!", ConsoleColor.DarkCyan);
                        break;
                    default:
                        InputOutputHelper.PrintColorText("\nPlease make a choise. \"C\" / \"Enter\": ", ConsoleColor.Red);
                        break;
                }
            } while (key != "ENTER" & key != "C");
            return output;
        }
    }
}
