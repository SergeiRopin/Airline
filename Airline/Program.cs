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

        static void Main(string[] args)
        {
            Console.WindowHeight = 50;
            Console.WindowWidth = 150;
            do
            {
                Console.Clear();
                AuxiliaryMethods.PrintColorText("********** AIRLINE **********\n", ConsoleColor.DarkGreen);

                Console.WriteLine(@"Please make the choise (enter the number):

                1. View all flights (without passengers);
                2. Search a flight;
                3. View all flight’s passengers;
                4. Search passengers;
                5. Search flights with lower price;
                6. Edit flights;
                7. Edit passengers.");

                Console.Write("Your choise: ");

                _menuManager.MenuHandler = ManageMainMenu;
                _menuManager.HandleExceptions();

                AuxiliaryMethods.PrintColorText("\nPress \"Space\" to exit; press any key to return to the main menu\n", ConsoleColor.DarkGreen);
            }
            while (Console.ReadKey().Key != ConsoleKey.Spacebar);
        }

        static void ManageMainMenu()
        {
            AirlineManager airlineManager = new AirlineManager();

            int index = (int)uint.Parse(Console.ReadLine());
            IDictionary<int, MenuItemHandler> menuItems = new Dictionary<int, MenuItemHandler>
                    {
                        { 1, airlineManager.ViewAllFlights },
                        { 2, airlineManager.SearchFlight },
                        { 3, airlineManager.ViewPassengers },
                        { 4, airlineManager.SearchPassengers },
                        //{ 5, airlineManager.SearchLowerPrice },
                    };
            _menuManager.MenuItemHandler = menuItems[index];
            _menuManager.CallMenuItem();
        }
    }
}

