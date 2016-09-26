using AirportManager;
using PresenterStorage;
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
        static bool _exit;

        static void Main(string[] args)
        {
            Console.WindowHeight = 50;
            Console.WindowWidth = 160;
            
            //Initialize view and presenter.
            IView view = MvpManager.Instance;
            Presenter presenter = new Presenter(view);

            while (!_exit)
            {
                Console.Clear();
                InputOutputHelper.PrintColorText("********** AIRLINE **********\n", ConsoleColor.DarkGreen);

                Console.WriteLine(@"Welcome to airline main menu:

                1. View all flights (without passengers);
                2. Search flights;
                3. Search flights with low price;
                4. Search passengers;
                5. Add, delete, edit flights;
                6. Add, delete, edit passengers;

                0. Exit");
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
                        { 1, _menuManager.ViewAllFlightsMenu },
                        { 2, _menuManager.SearchFlights },
                        { 3, _menuManager.SearchFlightsWithLowPriceMenu },
                        { 4, _menuManager.SearchPassengers },
                        { 5, _menuManager.EditFlights },
                        { 6, _menuManager.EditPassengers },
                        { 0, new Action(() => _exit = true) }
                    };
            do
            {
                Console.Clear();
                Action menuItemHandler = menuItems[index];
                menuItemHandler();
                if (_exit)
                    break; 
            } while (Console.ReadKey().Key != ConsoleKey.Spacebar);
        }
    }
}

