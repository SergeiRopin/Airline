using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    public delegate void MenuItemHandler();

    class Program
    {
        static MenuManager _menuManager = new MenuManager();
        static AirlineManager airlineManager = new AirlineManager();

        static void Main(string[] args)
        {
            Console.WindowHeight = 50;
            Console.WindowWidth = 160;
            airlineManager.InitializeAirport();

            do
            {
                Console.Clear();
                InputOutputHelper.PrintColorText("********** AIRLINE **********\n", ConsoleColor.DarkGreen);
                Console.WriteLine(@"Welcome to airline main menu:

                1. View all flights (without passengers);
                2. Search a flight;
                3. View all flight’s passengers;
                4. Search passengers;
                5. Search flights with lower price;
                6. Add, delete, edit flights;
                7. Add, delete, edit passengers.");
                Console.Write("\nPlease choose menu item number: ");

                _menuManager.MenuHandler = ManageMainMenu;
                _menuManager.HandleExceptions();

                InputOutputHelper.PrintColorText("\nPress \"Esc\" to exit; press any key to return to the airline main menu\n", ConsoleColor.DarkGreen);
            }
            while (Console.ReadKey().Key != ConsoleKey.Escape);
        }

        static void ManageMainMenu()
        {

            int index = (int)uint.Parse(Console.ReadLine());
            IDictionary<int, MenuItemHandler> menuItems = new Dictionary<int, MenuItemHandler>
                    {
                        { 1, airlineManager.ViewAllFlights },
                        { 2, airlineManager.SearchFlights },
                        { 3, airlineManager.ViewPassengers },
                        { 4, airlineManager.SearchPassengers },
                        { 5, airlineManager.SearchFlightsWithLowPrice },
                        { 6, airlineManager.EditFlightsInfo },
                        //{ 7, airlineManager }
                    };
            _menuManager.MenuItemHandler = menuItems[index];
            _menuManager.CallMenuItem();
        }
    }
}

