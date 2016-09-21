using Airport;
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
        static Airport.Airport _airport = Airport.Airport.Instance;
        static bool _exit;

        static void Main(string[] args)
        {
            Console.WindowHeight = 50;
            Console.WindowWidth = 160;
            InitializeAirport();

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

        static void InitializeAirport()
        {
            _airport.AddFlight(new Flight(ArrivalDeparture.Departure, "PC 753", "Kharkiv", "Istanbul", "MAU", Terminal.A, Gate.A1, FlightStatus.DeparturedAt, new DateTime(2016, 09, 05, 02, 00, 00),
                new List<Passenger>
                {
                    new Passenger("Sergei", "Ropin", "Ukraine", "BS059862", new DateTime(1987, 06, 25), Sex.Male, new Ticket(SeatClass.Business, 400M)),
                    new Passenger("Roman", "Goy", "Ukraine", "HT459863", new DateTime(1989, 06, 20), Sex.Male, new Ticket(SeatClass.Economy, 200M)),
                    new Passenger("Anna", "Sidorchuk", "Ukraine", "RT8915623", new DateTime(1991, 12, 29), Sex.Female, new Ticket(SeatClass.Economy, 200M)),
                    new Passenger("Masud", "Hadjivand", "Iran", "UI458255", new DateTime(1983, 02, 08), Sex.Female, new Ticket(SeatClass.Business, 400M))
                }));
            _airport.AddFlight(new Flight(ArrivalDeparture.Arrival, "EY 8470", "Warshaw", "Kharkiv", "MAU", Terminal.A, Gate.A3, FlightStatus.InFlight, new DateTime(2016, 09, 05, 15, 00, 00),
                new List<Passenger>
                {
                    new Passenger("Andrei", "Ivanov", "Russia", "OP8952365", new DateTime(1965, 05, 13), Sex.Male, new Ticket(SeatClass.Economy, 230M)),
                    new Passenger("Oleg", "Garmash", "Ukraine", "NE4153652", new DateTime(1936, 11, 11), Sex.Male, new Ticket(SeatClass.Business, 550M)),
                    new Passenger("Sarah", "Andersen", "USA", "TR15513665", new DateTime(1995, 08, 29), Sex.Female, new Ticket(SeatClass.Economy, 330M)),
                    new Passenger("Taras", "Gus", "Turkey", "ER525123", new DateTime(1955, 02, 22), Sex.Female, new Ticket(SeatClass.Economy, 340M))
                }));
            _airport.AddFlight(new Flight(ArrivalDeparture.Arrival, "PS 026", "Odessa", "Kharkiv", "MAU", Terminal.A, Gate.A1, FlightStatus.ExpectedAt, new DateTime(2016, 09, 05, 23, 15, 00),
                new List<Passenger>
                {
                    new Passenger("Alexander", "Oleinyk", "Moldova", "IK25365885", new DateTime(1993, 03, 12), Sex.Male, new Ticket(SeatClass.Economy, 130M)),
                    new Passenger("Artem", "Karpenko", "Ukraine", "AS2519698", new DateTime(1991, 02, 25), Sex.Male, new Ticket(SeatClass.Economy, 130M)),
                    new Passenger("Yurii", "Vashuk", "Ukraine", "RT1234567", new DateTime(1966, 08, 23), Sex.Male, new Ticket(SeatClass.Business, 300M)),
                    new Passenger("Ivan", "Klimov", "USA", "AD1586947", new DateTime(1987, 06, 15), Sex.Male, new Ticket(SeatClass.Economy, 130M))
                }));
        }
    }
}

