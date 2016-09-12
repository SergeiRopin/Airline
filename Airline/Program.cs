using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    class Program
    {
        static MenuManager _menuManager = new MenuManager();
        static AirlineManager airlineManager = new AirlineManager();
        static bool exit = true;

        static void Main(string[] args)
        {
            Console.WindowHeight = 50;
            Console.WindowWidth = 160;
            airlineManager.InitializeAirport();

            while (exit)
            {
                Console.Clear();
                InputOutputHelper.PrintColorText("********** AIRLINE **********\n", ConsoleColor.DarkGreen);

                Console.WriteLine(@"Welcome to airline main menu:

                1. View all flights (without passengers);
                2. Search flights;
                3. Search flights with low price;
                4. Search passengers;
                5. Add, delete, edit flights;
                6. Add, delete, edit passengers.

                Enter ""0"" to exit...");
                Console.Write("\nPlease choose menu item number: ");

                Action menuHandler = ManageMainMenu;
                _menuManager.CatchMenuExceptions(menuHandler);
            }
        }

        static void ManageMainMenu()
        {
            int index = (int)uint.Parse(Console.ReadLine());
            IDictionary<int, Action> menuItems = new Dictionary<int, Action>
                    {
                        { 1, airlineManager.ViewAllFlights },
                        { 2, airlineManager.SearchFlights },
                        { 3, airlineManager.SearchFlightsWithLowPrice },
                        { 4, airlineManager.SearchPassengers },
                        { 5, airlineManager.EditFlightsInfo },
                        { 6, airlineManager.EditPassengersInfo },
                        { 0, new Action(() => exit = false) }
                    };
            Action menuItemHandler = menuItems[index];
            _menuManager.CallMenuItem(menuItemHandler);
        }
    }
}

