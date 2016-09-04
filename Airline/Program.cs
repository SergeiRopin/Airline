using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    public delegate void MenuHandler();

    class Program
    {
        static MenuCaller _menuCaller = new MenuCaller();

        static void Main(string[] args)
        {
            Console.WindowHeight = 50;
            Console.WindowWidth = 150;
            do
            {
                Console.Clear();
                AuxiliaryMethods.PrintColorText("**********AIRLINE**********\n", ConsoleColor.DarkGreen);

                Console.WriteLine(@"Please make the choise (enter the number):

                1. View all flights (without passengers);
                2. Search a flight;
                3. View all flight’s passengers;
                4. Search passengers;
                5. Search a flight with lower price;
                6. Edit flights information;
                7. Edit passengers information.");

                Console.Write("Your choise: ");

                _menuCaller.ExceptionsHandler = CallMainMenu;
                _menuCaller.HandleExceptions();

                AuxiliaryMethods.PrintColorText("\nPress \"Space\" to exit; press any key to return to the main menu\n", ConsoleColor.DarkGreen);
            }
            while (Console.ReadKey().Key != ConsoleKey.Spacebar);
        }

        static void CallMainMenu()
        {
            AirlineLogic airlineLogic = new AirlineLogic();

            int index = (int)uint.Parse(Console.ReadLine());
            IDictionary<int, MenuHandler> menuItems = new Dictionary<int, MenuHandler>
                    {
                        { 1, airlineLogic.ViewAllFlights },
                        { 2, airlineLogic.SearchFlight },
                        { 3, airlineLogic.ViewPassengers },
                        { 4, airlineLogic.SearchPassengers },
                    };
            _menuCaller.MenuHandler = menuItems[index];
            _menuCaller.CallMenuItem();
        }
    }
}

