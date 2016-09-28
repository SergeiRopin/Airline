using Airline.SetDateStrategy;
using Airline.TemplateMethod;
using AirportManager;
using Airport.Exceptions;
using PresenterStorage;
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
        MvpManager _manager = MvpManager.Instance;
        AirlineManager _airlineManager = new AirlineManager();

        /// <summary>
        /// Prints all available flights
        /// </summary>
        public void ViewAllFlights()
        {
            InputOutputHelper.PrintColorText("\n******** VIEW FLIGHTS CONSOLE MENU ********", ConsoleColor.DarkCyan);
            _airlineManager.PrintFlights();
        }

        /// <summary>
        /// Prints a flight matches with the entered number
        /// </summary>
        public void SearchFlightByNumber()
        {
            Flight flight = _airlineManager.GetFlightByNumber();
            InputOutputHelper.PrintColorText("\nResults of the search: ", ConsoleColor.DarkCyan);
            Console.WriteLine(flight);

            if (flight == null)
                Console.WriteLine(_airlineManager.NoMatchesMessage);
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
            _manager.OnFilteringFlightsEventRaised(new FilteringFlightsEventArgs(flight => DateTime.Equals(flight.DateTime, flightTime)))
            .ToList()
            .ForEach(flight =>
            {
                Console.WriteLine(flight);
                temp++;
            });
            if (temp == 0)
                Console.WriteLine(_airlineManager.NoMatchesMessage);
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
            _manager.OnFilteringFlightsEventRaised(new FilteringFlightsEventArgs(flight =>
            String.Equals(city, flight.CityFrom, StringComparison.OrdinalIgnoreCase) |
            String.Equals(city, flight.CityTo, StringComparison.OrdinalIgnoreCase)))
            .ToList()
            .ForEach(flight =>
            {
                Console.WriteLine(flight);
                temp++;
            });
            if (temp == 0)
                Console.WriteLine(_airlineManager.NoMatchesMessage);
        }

        /// <summary>
        /// Prints flights in this hour
        /// </summary>
        public void SearchFlightsInThisHour()
        {
            InputOutputHelper.PrintColorText("\nFlights in this hour: ", ConsoleColor.DarkCyan);

            // Print matching flights.
            int temp = 0;
            _manager.OnFilteringFlightsEventRaised(new FilteringFlightsEventArgs(flight =>
            DateTime.Now > flight.DateTime.AddMinutes(-30) &&
            DateTime.Now < flight.DateTime.AddMinutes(30)))
            .ToList()
            .ForEach(flight =>
            {
                Console.WriteLine(flight);
                temp++;
            });
            if (temp == 0)
                Console.WriteLine(_airlineManager.NoMatchesMessage);
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
            _manager.OnFilteringFlightsEventRaised(new FilteringFlightsEventArgs(flight => true))
                .ToList()
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
                Console.WriteLine(_airlineManager.NoMatchesMessage);
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
                try
                {
                    _manager.OnAddingFlightEventRaised(this, new FlightEventArgs(newFlight));
                    InputOutputHelper.PrintColorText($"\nFlight \"{newFlight.Number}\" was successfully added!", ConsoleColor.DarkCyan);
                    InputOutputHelper.PrintColorText(newFlight.ToString(), ConsoleColor.DarkCyan);
                }
                catch (NotUniqueFlightNumberException e)
                {
                    InputOutputHelper.PrintColorText(e.Message, ConsoleColor.Red);
                }
            }
        }

        /// <summary>
        /// Removes a flight from the flights list
        /// </summary>
        public void DeleteFlight()
        {
            InputOutputHelper.PrintColorText("\n******** DELETE FLIGHT CONSOLE MENU ********", ConsoleColor.DarkCyan);

            Console.WriteLine("\nTo delete the flight first choose the flight number. Actual flights:");
            _airlineManager.PrintFlights();
            Flight flightToDelete = _airlineManager.GetFlightByNumber();
            if (flightToDelete != null)
            {
                Console.WriteLine("\n" + flightToDelete);
                Console.Write($"\nYou want to remove the flight: {flightToDelete.Number}? (Y/N): ");
                string confirmation;
                do
                {
                    confirmation = Console.ReadLine().ToUpper();
                    switch (confirmation)
                    {
                        case "Y":
                            _manager.OnDeletingFlightEventRaised(this, new FlightEventArgs(flightToDelete));
                            InputOutputHelper.PrintColorText($"\nFlight {flightToDelete.Number} was successfully removed!", ConsoleColor.DarkCyan);
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
            else Console.WriteLine($"\n{_airlineManager.NoMatchesMessage}");
        }

        /// <summary>
        /// Allows to edit any property of any flight
        /// </summary>
        public void EditFlight()
        {
            Console.Clear();
            InputOutputHelper.PrintColorText("\n******** EDIT FLIGHT CONSOLE MENU ********", ConsoleColor.DarkCyan);

            Console.WriteLine("\nTo change the flight information first choose the flight number. Actual flights:");
            _airlineManager.PrintFlights();
            Flight actualFlight = _airlineManager.GetFlightByNumber();
            if (actualFlight != null)
            {
                Console.WriteLine("\nActual fligth information:");
                InputOutputHelper.PrintColorText(actualFlight.ToString(), ConsoleColor.DarkCyan);
                Console.WriteLine("\nFollow the instruction to update the flight information:");

                CreateEditEntityHelper entityHelper = new CreateEditEntityHelper();
                Flight updatedFlight = entityHelper.EditEntity<Flight>(actualFlight);
                if (updatedFlight != null)
                {
                    _manager.OnEditingFlightEventRaised(this, new EditingFlightEventArgs(actualFlight, updatedFlight));
                    InputOutputHelper.PrintColorText($"\nFlight \"{updatedFlight.Number}\" was successfully updated!", ConsoleColor.DarkCyan);
                    InputOutputHelper.PrintColorText(updatedFlight.ToString(), ConsoleColor.DarkCyan);
                }
            }
            else Console.WriteLine($"\n{_airlineManager.NoMatchesMessage}");
        }
    }
}
