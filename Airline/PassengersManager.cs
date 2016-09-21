﻿using Airline.TemplateMethod;
using Airport;
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
        private Airport.Airport _airport = Airport.Airport.Instance;
        private FlightsManager _flightsManager = new FlightsManager();

        private string _noMatchesMessage = "No matches found!";

        /// <summary>
        /// Prints passengers matching the entered name (partial coincidence)
        /// </summary>
        public void SearchPassengerByName()
        {
            string name = InputOutputHelper.SetString("\nPlease enter a name of the passenger: ");
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

        /// <summary>
        /// Prints passengers matching the entered flight number (full coincidence)
        /// </summary>
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

        /// <summary>
        /// Prints passengers matching the entered passport (partial coincidence)
        /// </summary>
        public void SearchPassengerByPassport()
        {
            string passport = InputOutputHelper.SetString("\nPlease enter a passport of the passenger: ");
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

            Console.WriteLine("\nTo add the passenger first enter the flight number.");
            Flight flight = _flightsManager.RealizeGetFlightByNumber();
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

        /// <summary>
        /// Removes a passenger from the selected flight
        /// </summary>
        public void DeletePassenger()
        {
            InputOutputHelper.PrintColorText("\n******** DELETE PASSENGER CONSOLE MENU ********", ConsoleColor.DarkCyan);

            Flight flight = _flightsManager.RealizeGetFlightByNumber();
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

        /// <summary>
        /// Allows to edit any passenger's property of any flight
        /// </summary>
        public void EditPassenger()
        {
            InputOutputHelper.PrintColorText("\n******** EDIT PASSENGER CONSOLE MENU ********", ConsoleColor.DarkCyan);

            Console.WriteLine("\nTo change the passenger information first enter the flight number and passenger passport.");
            Flight flight = _flightsManager.RealizeGetFlightByNumber();
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