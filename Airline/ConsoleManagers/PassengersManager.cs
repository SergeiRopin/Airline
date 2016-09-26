using Airline.TemplateMethod;
using AirportManager;
using PresenterStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    /// <summary>
    /// Realizes the logic related to passengers
    /// </summary>
    class PassengersManager
    {
        MvpManager _manager = MvpManager.Instance;
        AirlineManager _airlineManager = new AirlineManager();
        
        /// <summary>
        /// Returns passengers from the selected flight by choosen predicate
        /// </summary>
        public IEnumerable<Passenger> FilterPassengers(Flight flight, Func<Passenger, bool> predicate) =>
            flight.Passengers.Where(predicate);

        /// <summary>
        /// Print all passengers from the selected flight
        /// </summary>
        public void PrintPassengersByFlightNumber(Flight flight)
        {
            FilterPassengers(flight, passenger => true)
                .ToList()
                .ForEach(passenger => Console.WriteLine($"Flight: {flight.Number}, " + passenger));
        }

        /// <summary>
        /// Prints passengers matching the entered name (partial coincidence)
        /// </summary>
        public void SearchPassengerByName()
        {
            string name = InputOutputHelper.SetString("\nPlease enter a name of the passenger: ");
            InputOutputHelper.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);

            // Print matching passengers.
            int temp = 0;
            _manager.OnFilteringFlightsEventRaised(new FilteringFlightsEventArgs(flight => true))
                .ToList()
                .ForEach(flight => flight.Passengers
                .ForEach(passenger =>
                {
                    if (passenger.FirstName.ToUpper().Contains(name.ToUpper()) | passenger.LastName.ToUpper().Contains(name.ToUpper()))
                    {
                        Console.WriteLine($"Flight: {flight.Number}, " + passenger);
                        temp++;
                    }
                }));
            if (temp == 0)
                Console.WriteLine(_airlineManager.NoMatchesMessage);
        }

        /// <summary>
        /// Prints passengers matching the entered flight number (full coincidence)
        /// </summary>
        public void SearchPassengersByFlightNumber()
        {
            Console.WriteLine("\nActual flights:");
            _airlineManager.PrintFlights();

            Flight flight = _airlineManager.GetFlightByNumber();
            InputOutputHelper.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);

            if (flight != null)
                PrintPassengersByFlightNumber(flight);
            else Console.WriteLine(_airlineManager.NoMatchesMessage);
        }

        /// <summary>
        /// Prints passengers matching the entered passport (partial coincidence)
        /// </summary>
        public void SearchPassengerByPassport()
        {
            string passport = InputOutputHelper.SetString("\nPlease enter a passport of the passenger: ");
            InputOutputHelper.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);

            // Print matching passengers.
            int temp = 0;
            _manager.OnFilteringFlightsEventRaised(new FilteringFlightsEventArgs(flight => true))
                .ToList()
                .ForEach(flight => flight.Passengers
                .ForEach(passenger =>
                {
                    if (passenger.Passport.ToUpper().Contains(passport.ToUpper()))
                    {
                        Console.WriteLine($"Flight: {flight.Number}, " + passenger);
                        temp++;
                    }
                }));
            if (temp == 0)
                Console.WriteLine(_airlineManager.NoMatchesMessage);
        }

        /// <summary>
        /// Asks to enter a passport number (full coincidence) and return a passenger
        /// </summary>
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

        /// <summary>
        /// Adds a passenger to the selected flight
        /// </summary>
        public void AddPassenger()
        {
            InputOutputHelper.PrintColorText("\n******** ADD A NEW PASSENGER CONSOLE MENU ********", ConsoleColor.DarkCyan);

            Console.WriteLine("\nTo add a passenger first enter the flight number. Actual flights:");
            _airlineManager.PrintFlights();
            Flight flight = _airlineManager.GetFlightByNumber();
            if (flight != null)
            {
                InputOutputHelper.PrintColorText("\nFill an information about passenger:", ConsoleColor.DarkCyan);
                CreateEditEntityHelper entityHelper = new CreateEditEntityHelper();
                Passenger passenger = entityHelper.CreateEntity<Passenger>();
                if (passenger != null)
                {
                    flight.Passengers.Add(passenger);

                    InputOutputHelper.PrintColorText($"\nPassenger was successfully added to the flight \"{flight.Number}\"!", ConsoleColor.DarkCyan);
                    InputOutputHelper.PrintColorText(passenger.ToString(), ConsoleColor.DarkCyan);
                }
            }
            else Console.WriteLine($"\n{_airlineManager.NoMatchesMessage}");
        }

        /// <summary>
        /// Removes a passenger from the selected flight
        /// </summary>
        public void DeletePassenger()
        {
            InputOutputHelper.PrintColorText("\n******** DELETE PASSENGER CONSOLE MENU ********", ConsoleColor.DarkCyan);

            Console.WriteLine("\nTo delete a passenger first enter the flight number and passenger passport. Actual flights:");
            _airlineManager.PrintFlights();
            Flight flight = _airlineManager.GetFlightByNumber();

            Console.WriteLine("\nActual passengers:");
            PrintPassengersByFlightNumber(flight);
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
                            flight.Passengers.Remove(passenger);
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
            else Console.WriteLine($"\n{_airlineManager.NoMatchesMessage}");
        }

        /// <summary>
        /// Allows to edit any passenger's property of any flight
        /// </summary>
        public void EditPassenger()
        {
            InputOutputHelper.PrintColorText("\n******** EDIT PASSENGER CONSOLE MENU ********", ConsoleColor.DarkCyan);

            Console.WriteLine("\nTo change the passenger information first enter the flight number and passenger passport. Actual flights:");
            _airlineManager.PrintFlights();
            Flight flight = _airlineManager.GetFlightByNumber();

            Console.WriteLine("\nActual passengers:");
            PrintPassengersByFlightNumber(flight);
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
                    int passengerIndex = flight.Passengers.IndexOf(actualPassenger);
                    if (passengerIndex != -1)
                        flight.Passengers[passengerIndex] = updatedPassenger;

                    InputOutputHelper.PrintColorText($"\nPassenger information was successfully updated!\n", ConsoleColor.DarkCyan);
                    InputOutputHelper.PrintColorText(updatedPassenger.ToString(), ConsoleColor.DarkCyan);
                }
            }
            else Console.WriteLine($"\n{_airlineManager.NoMatchesMessage}");
        }
    }
}
