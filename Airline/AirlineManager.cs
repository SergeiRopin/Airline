using Airline.TemplateMethod;
using Airport;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    class AirlineManager
    {
        private AirportManager _airport = AirportManager.Instance;

        private string _noMatchesMessage = "No matches found!";

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
            InputOutputHelper.PrintColorText("\n******** VIEW FLIGHTS CONSOLE MENU ********", ConsoleColor.DarkCyan);

            // Print arrivals.
            InputOutputHelper.PrintColorText("\nARRIVAL FLIGHTS:", ConsoleColor.DarkCyan);
            _airport.GetAllFlights().ToList().ForEach(x =>
            {
                if (x.ArrivalDeparture == ArrivalDeparture.Arrival)
                {
                    Console.WriteLine(x);
                }
            });

            // Print departures.
            InputOutputHelper.PrintColorText("\nDEPARTURED FLIGHTS:", ConsoleColor.DarkCyan);
            _airport.GetAllFlights().ToList().ForEach(x =>
            {
                if (x.ArrivalDeparture == ArrivalDeparture.Departure)
                {
                    Console.WriteLine(x);
                }
            });
        }


        public void SearchFlightByNumber()
        {
            Flight flight = RealizeGetFlightByNumber();
            InputOutputHelper.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);
            Console.WriteLine(flight);

            if (flight == null)
                Console.WriteLine(_noMatchesMessage);
        }

        public void SearchFlightByTime()
        {
            DateTime flightTime = InputOutputHelper.CreateDateTime("\nSpecify the time of a flight in the following format:");

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

        public void SearchFlightByCity()
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

        public void SearchFlightsInThisHour()
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
        public void SearchCheapFlights()
        {
            InputOutputHelper.PrintColorText("\n******** SEARCH CHEAP FLIGHTS CONSOLE MENU ********", ConsoleColor.DarkCyan);

            decimal priceLimit = InputOutputHelper.CreateValueType<decimal>($"\nPlease enter a limit of the flight price (dollars): $");
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
        }


        public void SearchPassengerByName()
        {
            string name = InputOutputHelper.CreateString("\nPlease enter a name of the passenger: ");
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

        public void SearchPassengerByPassport()
        {
            string passport = InputOutputHelper.CreateString("\nPlease enter a passport of the passenger: ");
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
        /// Asks to enter a flight number and returns the flight
        /// </summary>
        /// <returns>new flight</returns>
        private Flight RealizeGetFlightByNumber()
        {
            Console.Write("\nPlease enter a flight number: ");
            string flightNumber = Console.ReadLine();
            Flight flight = _airport.GetFlightByNumber(flightNumber);
            return flight;
        }

        public void AddFlight()
        {
            InputOutputHelper.PrintColorText("\n******** ADD A NEW FLIGHT CONSOLE MENU ********", ConsoleColor.DarkCyan);

            CreateEditEntityHelper entityHelper = new CreateEditEntityHelper();
            Flight newFlight = entityHelper.CreateEntity<Flight>();
            if (newFlight != null)
            {
                _airport.AddFlight(newFlight);
                InputOutputHelper.PrintColorText($"\nFlight \"{newFlight.Number}\" was successfully added!", ConsoleColor.DarkCyan);
                InputOutputHelper.PrintColorText(newFlight.ToString(), ConsoleColor.DarkCyan);
            }
        }

        public void DeleteFlight()
        {
            InputOutputHelper.PrintColorText("\n******** DELETE FLIGHT CONSOLE MENU ********", ConsoleColor.DarkCyan);

            Flight flight = RealizeGetFlightByNumber();
            if (flight != null)
            {
                Console.WriteLine("\n" + flight);
                Console.Write($"\nYou want to remove the flight: {flight.Number}? (Y/N): ");
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

        public void EditFlight()
        {
            Console.Clear();
            InputOutputHelper.PrintColorText("\n******** EDIT FLIGHT CONSOLE MENU ********", ConsoleColor.DarkCyan);

            Flight actualFlight = RealizeGetFlightByNumber();
            if (actualFlight != null)
            {
                Console.WriteLine("\nActual fligth information:");
                InputOutputHelper.PrintColorText(actualFlight.ToString(), ConsoleColor.DarkCyan);
                Console.WriteLine("\nFollow the instruction to update the flight information:");

                CreateEditEntityHelper entityHelper = new CreateEditEntityHelper();
                Flight updatedFlight = entityHelper.EditEntity<Flight>(actualFlight);
                if (updatedFlight != null)
                {
                    _airport.EditFlight(actualFlight, updatedFlight);
                    InputOutputHelper.PrintColorText($"\nFlight \"{updatedFlight.Number}\" was successfully updated!", ConsoleColor.DarkCyan);
                    InputOutputHelper.PrintColorText(updatedFlight.ToString(), ConsoleColor.DarkCyan);
                }
            }
            else Console.WriteLine($"\n{_noMatchesMessage}");
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

        public void AddPassenger()
        {
            InputOutputHelper.PrintColorText("\n******** ADD A NEW PASSENGER CONSOLE MENU ********", ConsoleColor.DarkCyan);

            Console.WriteLine("\nTo add the passenger first enter the flight number.");
            Flight flight = RealizeGetFlightByNumber();
            if (flight != null)
            {
                InputOutputHelper.PrintColorText("\nFill an information about passenger:", ConsoleColor.DarkCyan);
                CreateEditEntityHelper entityHelper = new CreateEditEntityHelper();
                Passenger passenger = entityHelper.CreateEntity<Passenger>();
                if (passenger != null)
                {
                    _airport.AddPassenger(flight, passenger);

                    InputOutputHelper.PrintColorText($"\nPassenger was successfully added to the flight \"{flight.Number}\"!", ConsoleColor.DarkCyan);
                    InputOutputHelper.PrintColorText(passenger.ToString(), ConsoleColor.DarkCyan);
                }
            }
            else Console.WriteLine($"\n{_noMatchesMessage}");
        }

        public void DeletePassenger()
        {
            InputOutputHelper.PrintColorText("\n******** DELETE PASSENGER CONSOLE MENU ********", ConsoleColor.DarkCyan);

            Flight flight = RealizeGetFlightByNumber();
            Passenger passenger = GetPassengerByPassport(flight);

            if (flight != null && passenger != null)
            {
                InputOutputHelper.PrintColorText($"\nFlight: {flight.Number}, {passenger}", ConsoleColor.DarkCyan);
                Console.Write($"\nYou want to remove the passenger: {passenger.FirstName} {passenger.LastName}? (Y/N): ");
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

        public void EditPassenger()
        {
            InputOutputHelper.PrintColorText("\n******** EDIT PASSENGER CONSOLE MENU ********", ConsoleColor.DarkCyan);

            Console.WriteLine("\nTo change the passenger information first enter the flight number and passenger passport.");
            Flight flight = RealizeGetFlightByNumber();
            Passenger actualPassenger = GetPassengerByPassport(flight);

            if (flight != null && actualPassenger != null)
            {
                Console.WriteLine("\nActual passenger information:");
                InputOutputHelper.PrintColorText($"Flight: {flight.Number}, {actualPassenger}", ConsoleColor.DarkCyan);

                Console.WriteLine("\nUpdate an information about the passenger:");
                CreateEditEntityHelper entityHelper = new CreateEditEntityHelper();
                Passenger updatedPassenger = entityHelper.EditEntity<Passenger>(actualPassenger);
                if (updatedPassenger != null)
                {
                    _airport.EditPassenger(flight, actualPassenger, updatedPassenger);
                    InputOutputHelper.PrintColorText($"\nPassenger information was successfully updated!\n", ConsoleColor.DarkCyan);
                    InputOutputHelper.PrintColorText(updatedPassenger.ToString(), ConsoleColor.DarkCyan);
                }
            }
            else Console.WriteLine($"\n{_noMatchesMessage}");
        }
    }
}
