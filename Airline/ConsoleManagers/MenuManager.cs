using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    class MenuManager
    {
        FlightsManager _flightsManager = new FlightsManager();
        PassengersManager _passengersManager = new PassengersManager();

        /// <summary>
        /// Catch exceptions happened during menu item selection 
        /// </summary>
        /// <param name="action">selected method</param>
        public void CatchMenuExceptions(Action action)
        {
            try
            {
                if (action != null)
                    action();
            }
            catch (KeyNotFoundException)
            {
                InputOutputHelper.PrintColorText("\nUnexpected value has been entered, please select the value from menu list!", ConsoleColor.Red);
            }
            catch (Exception ex)
            {
                InputOutputHelper.PrintColorText("\nWrong value has been entered! " + ex.Message, ConsoleColor.Red);
            }

            InputOutputHelper.PrintColorText("\nPress \"Space\" to return to the airline main menu; press any key to continue with the current menu...",
                ConsoleColor.DarkGreen);
        }

        /// <summary>
        /// View all flights menu
        /// </summary>
        public void ViewAllFlightsMenu()
        {
            Action menuItemHandler = _flightsManager.ViewAllFlights;
            CatchMenuExceptions(menuItemHandler);
        }

        /// <summary>
        /// Search flights menu
        /// </summary>
        public void SearchFlights()
        {
            InputOutputHelper.PrintColorText("\n******** SEARCH FLIGHT MENU ********\n", ConsoleColor.DarkCyan);
            Console.WriteLine(@"Please choose one of the following search criterions (enter a menu number):

            1. Search by number;
            2. Search by time of arrival/departure;
            3. Search by city;
            4. Search all flights in this hour;");
            Console.Write("Your choise: ");

            Action menuHandler = CallSearchFlightsMenu;
            CatchMenuExceptions(menuHandler);
        }

        private void CallSearchFlightsMenu()
        {
            int index = (int)uint.Parse(Console.ReadLine());
            IDictionary<int, Action> menuItems = new Dictionary<int, Action>
            {
                { 1, _flightsManager.SearchFlightByNumber },
                { 2, _flightsManager.SearchFlightByTime },
                { 3, _flightsManager.SearchFlightByCity },
                { 4, _flightsManager.SearchFlightsInThisHour}
            };
            Action menuItemHandler = menuItems[index];
            menuItemHandler();
        }

        /// <summary>
        /// Search flights with low price menu
        /// </summary>
        public void SearchFlightsWithLowPriceMenu()
        {
            Action menuItemHandler = _flightsManager.SearchCheapFlights;
            CatchMenuExceptions(menuItemHandler);
        }

        /// <summary>
        /// Search passengers menu
        /// </summary>
        public void SearchPassengers()
        {
            InputOutputHelper.PrintColorText("\n******** SEARCH PASSENGERS MENU ********\n", ConsoleColor.DarkCyan);
            Console.WriteLine(@"To find the passenger please choose one of the following search criterions (enter a menu number):

            1. Search by Name (first or last name);
            2. Search by Flight number;
            3. Search by Passport;");
            Console.Write("Your choise: ");

            Action menuHandler = CallSearchPassengersMenu;
            CatchMenuExceptions(menuHandler);
        }

        private void CallSearchPassengersMenu()
        {
            int index = (int)uint.Parse(Console.ReadLine());
            IDictionary<int, Action> menuItems = new Dictionary<int, Action>
            {
                { 1, _passengersManager.SearchPassengerByName },
                { 2, _passengersManager.SearchPassengersByFlightNumber },
                { 3, _passengersManager.SearchPassengerByPassport }
            };
            Action menuItemHandler = menuItems[index];
            menuItemHandler();
        }

        /// <summary>
        /// Edit flight's information menu
        /// </summary>
        public void EditFlights()
        {
            InputOutputHelper.PrintColorText("\n******** EDIT FLIGHTS INFORMATION MENU ********\n", ConsoleColor.DarkCyan);
            Console.WriteLine(@"Please choose one of the following items (enter a menu number):

            1. Add a flight;
            2. Delete a flight;
            3. Edit a flight's information.");
            Console.Write("Your choise: ");

            Action menuHandler = CallEditFlightsMenu;
            CatchMenuExceptions(menuHandler);
        }

        private void CallEditFlightsMenu()
        {
            int index = (int)uint.Parse(Console.ReadLine());
            IDictionary<int, Action> menuItems = new Dictionary<int, Action>
            {
                { 1, _flightsManager.AddFlight },
                { 2, _flightsManager.DeleteFlight },
                { 3, _flightsManager.EditFlight }
            };
            Action menuItemHandler = menuItems[index];
            menuItemHandler();
        }

        /// <summary>
        /// Edit passenger's information menu
        /// </summary>
        public void EditPassengers()
        {
            InputOutputHelper.PrintColorText("\n******** EDIT PASSENGERS INFORMATION MENU ********\n", ConsoleColor.DarkCyan);
            Console.WriteLine(@"Please choose one of the following menu items (enter a menu number):

            1. Add a passenger;
            2. Delete a passenger;
            3. Edit a passenger's information.");
            Console.Write("Your choise: ");

            Action menuHandler = CallEditPassengersMenu;
            CatchMenuExceptions(menuHandler);
        }

        private void CallEditPassengersMenu()
        {
            int index = (int)uint.Parse(Console.ReadLine());
            IDictionary<int, Action> menuItems = new Dictionary<int, Action>
            {
                { 1, _passengersManager.AddPassenger },
                { 2, _passengersManager.DeletePassenger },
                { 3, _passengersManager.EditPassenger }
            };
            Action menuItemHandler = menuItems[index];
            menuItemHandler();
        }
    }
}
