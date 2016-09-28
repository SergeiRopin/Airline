using AirportManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    /// <summary>
    /// Contains common methods for FlightsManager and PassengersManager
    /// </summary>
    class AirlineManager
    {
        MvpManager _manager = MvpManager.Instance;
        public string NoMatchesMessage => "No matches found!";

        /// <summary>
        /// Prints all available flights
        /// </summary>
        public void PrintFlights()
        {
            // Print arrivals.
            InputOutputHelper.PrintColorText("\nARRIVAL FLIGHTS:", ConsoleColor.DarkCyan);
            _manager.OnFilteringFlightsEventRaised(new FilteringFlightsEventArgs(flight =>
            flight.ArrivalDeparture == ArrivalDeparture.Arrival))
            .ToList()
            .ForEach(flight =>
            {
                Console.WriteLine(flight);
            });

            // Print departures.
            InputOutputHelper.PrintColorText("\nDEPARTURED FLIGHTS:", ConsoleColor.DarkCyan);
            _manager.OnFilteringFlightsEventRaised(new FilteringFlightsEventArgs(flight =>
            flight.ArrivalDeparture == ArrivalDeparture.Departure))
            .ToList()
            .ForEach(flight =>
            {
                Console.WriteLine(flight);
            });
        }

        /// <summary>
        /// Asks to enter a flight number and returns the flight
        /// </summary>
        /// <returns>new flight</returns>
        public Flight GetFlightByNumber()
        {
            Console.Write("\nPlease enter a flight number: ");
            string flightNumber = Console.ReadLine();
            Flight flight = _manager.OnFilteringFlightsEventRaised(new FilteringFlightsEventArgs(x =>
            String.Equals(x.Number.Replace(" ", string.Empty), flightNumber.Replace(" ", string.Empty), StringComparison.OrdinalIgnoreCase))).FirstOrDefault();
            return flight;
        }
    }
}
