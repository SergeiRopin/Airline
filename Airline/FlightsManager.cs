using Airline.SetDateStrategy;
using Airline.TemplateMethod;
using Airport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    /// <summary>
    /// Realizes the logic related to flights
    /// </summary>
    class FlightsManager
    {
        private Airport.Airport _airport = Airport.Airport.Instance;

        private string _noMatchesMessage = "No matches found!";

        public void PrintFlights()
        {
            // Print arrivals.
            InputOutputHelper.PrintColorText("\nARRIVAL FLIGHTS:", ConsoleColor.DarkCyan);
            _airport.FilterFlights(flight => flight.ArrivalDeparture == ArrivalDeparture.Arrival)
                .ToList()
                .ForEach(flight =>
                {
                    Console.WriteLine(flight);
                });

            // Print departures.
            InputOutputHelper.PrintColorText("\nDEPARTURED FLIGHTS:", ConsoleColor.DarkCyan);
            _airport.FilterFlights(flight => flight.ArrivalDeparture == ArrivalDeparture.Departure)
                .ToList()
                .ForEach(flight =>
                {
                    Console.WriteLine(flight);
                });
        }

        /// <summary>
        /// Prints all available flights
        /// </summary>
        public void ViewAllFlights()
        {
            InputOutputHelper.PrintColorText("\n******** VIEW FLIGHTS CONSOLE MENU ********", ConsoleColor.DarkCyan);
            PrintFlights();
        }

        /// <summary>
        /// Asks to enter a flight number and returns the flight
        /// </summary>
        /// <returns>new flight</returns>
        public Flight RealizeGetFlightByNumber()
        {
            Console.Write("\nPlease enter a flight number: ");
            string flightNumber = Console.ReadLine();
            Flight flight = _airport.GetFlightByNumber(flightNumber);
            return flight;
        }

        /// <summary>
        /// Prints a flight matches with the entered number
        /// </summary>
        public void SearchFlightByNumber()
        {
            Flight flight = RealizeGetFlightByNumber();
            InputOutputHelper.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);
            Console.WriteLine(flight);

            if (flight == null)
                Console.WriteLine(_noMatchesMessage);
        }

        /// <summary>
        /// Prints flights matching the entered date and time
        /// </summary>
        public void SearchFlightByTime()
        {
            SetDateHelper dateHelper = new SetDateHelper(new FlightDate());
            DateTime flightTime = dateHelper.CreateDate();

            // Print matching flights.
            InputOutputHelper.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);
            int temp = 0;
            _airport.FilterFlights(flight => DateTime.Equals(flight.DateTime, flightTime))
                .ToList()
                .ForEach(flight =>
                {
                    Console.WriteLine(flight);
                    temp++;
                });
            if (temp == 0)
                Console.WriteLine(_noMatchesMessage);
        }

        /// <summary>
        /// Prints flights match with the entered city of arrival or departure
        /// </summary>
        public void SearchFlightByCity()
        {
            Console.Write("\nPlease enter an arrival/departure city: ");
            string city = Console.ReadLine();
            InputOutputHelper.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);

            // Print matching flights.
            int temp = 0;
            _airport.FilterFlights(flight =>
            String.Equals(city, flight.CityFrom, StringComparison.OrdinalIgnoreCase) |
            String.Equals(city, flight.CityTo, StringComparison.OrdinalIgnoreCase))
            .ToList()
            .ForEach(flight =>
            {
                Console.WriteLine(flight);
                temp++;
            });
            if (temp == 0)
                Console.WriteLine(_noMatchesMessage);
        }

        /// <summary>
        /// Prints flights in this hour
        /// </summary>
        public void SearchFlightsInThisHour()
        {
            InputOutputHelper.PrintColorText("\nFlights in this hour: ", ConsoleColor.DarkCyan);

            // Print matching flights.
            int temp = 0;
            _airport.FilterFlights(flight =>
            DateTime.Now > flight.DateTime.AddMinutes(-30) &&
            DateTime.Now < flight.DateTime.AddMinutes(30))
            .ToList()
            .ForEach(flight =>
            {
                Console.WriteLine(flight);
                temp++;
            });
            if (temp == 0)
                Console.WriteLine(_noMatchesMessage);
        }

        /// <summary>
        /// Prints all flights with the price of economy ticket lower than user input
        /// </summary>
        public void SearchCheapFlights()
        {
            InputOutputHelper.PrintColorText("\n******** SEARCH CHEAP FLIGHTS CONSOLE MENU ********", ConsoleColor.DarkCyan);

            decimal priceLimit = InputOutputHelper.SetValueType<decimal>($"\nPlease enter a limit of the flight price (dollars): $");
            InputOutputHelper.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);

            HashSet<Flight> economyFlights = new HashSet<Flight>();
            int temp = 0;
            _airport.FilterFlights(flight => true).ToList()
                .ForEach(flight => flight.Passengers
                .ForEach(passenger =>
                {
                    if (priceLimit >= passenger.Ticket.Price && passenger.Ticket.SeatClass == SeatClass.Economy)
                    {
                        bool isAdded = economyFlights.Add(flight);
                        if (isAdded)
                        {
                            Console.WriteLine($@"{flight}, ");
                            Console.WriteLine($"{passenger.Ticket}\n");
                        }
                        temp++;
                    }
                }));
            if (temp == 0)
                Console.WriteLine(_noMatchesMessage);
        }

        /// <summary>
        /// Adds a flight to the flights list
        /// </summary>
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

        /// <summary>
        /// Removes a flight from the flights list
        /// </summary>
        public void DeleteFlight()
        {
            InputOutputHelper.PrintColorText("\n******** DELETE FLIGHT CONSOLE MENU ********", ConsoleColor.DarkCyan);

            Console.WriteLine("\nTo delete the flight first choose the flight number. Actual flights:");
            PrintFlights();
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

        /// <summary>
        /// Allows to edit any property of any flight
        /// </summary>
        public void EditFlight()
        {
            Console.Clear();
            InputOutputHelper.PrintColorText("\n******** EDIT FLIGHT CONSOLE MENU ********", ConsoleColor.DarkCyan);

            Console.WriteLine("\nTo change the flight information first choose the flight number. Actual flights:");
            PrintFlights();
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
    }
}
