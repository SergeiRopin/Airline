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
            #region Airport initialization
            //Airport airport = new Airport();

            //airport.AddFlight(new Flight(ArrivalDeparture.Departure, "PC 753", "Kharkiv", "Istanbul", "MAU", Terminal.A, Gate.A1, FlightStatus.DeparturedAt, new DateTime(2016, 09, 05, 02, 00, 00),
            //    new List<Passenger>
            //    {
            //        new Passenger("Sergei", "Ropin", "Ukraine", "BS059862", new DateTime(1987, 06, 25), Sex.Male, new Ticket(SeatClass.Business, 400M)),
            //        new Passenger("Roman", "Goy", "Ukraine", "HT459863", new DateTime(1989, 06, 20), Sex.Male, new Ticket(SeatClass.Economy, 200M)),
            //        new Passenger("Anna", "Sidorchuk", "Ukraine", "RT8915623", new DateTime(1991, 12, 29), Sex.Female, new Ticket(SeatClass.Economy, 200M)),
            //        new Passenger("Masud", "Hadjivand", "Iran", "UI458255", new DateTime(1983, 02, 08), Sex.Female, new Ticket(SeatClass.Business, 400M))
            //    }));
            //airport.AddFlight(new Flight(ArrivalDeparture.Arrival, "EY 8470", "Warshaw", "Kharkiv", "MAU", Terminal.A, Gate.A3, FlightStatus.InFlight, new DateTime(2016, 09, 05, 15, 00, 00),
            //    new List<Passenger>
            //    {
            //        new Passenger("Andrei", "Ivanov", "Russia", "OP8952365", new DateTime(1965, 05, 13), Sex.Male, new Ticket(SeatClass.Economy, 230M)),
            //        new Passenger("Oleg", "Garmash", "Ukraine", "NE4153652", new DateTime(1936, 11, 11), Sex.Male, new Ticket(SeatClass.Business, 550M)),
            //        new Passenger("Sarah", "Andersen", "USA", "TR15513665", new DateTime(1995, 08, 29), Sex.Female, new Ticket(SeatClass.Economy, 330M)),
            //        new Passenger("Taras", "Gus", "Turkey", "ER525123", new DateTime(1955, 02, 22), Sex.Female, new Ticket(SeatClass.Economy, 340M))
            //    }));
            //airport.AddFlight(new Flight(ArrivalDeparture.Arrival, "PS 026", "Odessa", "Kharkiv", "MAU", Terminal.A, Gate.A1, FlightStatus.ExpectedAt, new DateTime(2016, 09, 05, 23, 15, 00),
            //    new List<Passenger>
            //    {
            //        new Passenger("Alexander", "Oleinyk", "Moldova", "IK25365885", new DateTime(1993, 03, 12), Sex.Male, new Ticket(SeatClass.Economy, 130M)),
            //        new Passenger("Artem", "Karpenko", "Ukraine", "AS2519698", new DateTime(1991, 02, 25), Sex.Male, new Ticket(SeatClass.Economy, 130M)),
            //        new Passenger("Yurii", "Vashuk", "Ukraine", "RT1234567", new DateTime(1966, 08, 23), Sex.Male, new Ticket(SeatClass.Business, 300M)),
            //        new Passenger("Ivan", "Klimov", "USA", "AD1586947", new DateTime(1987, 06, 15), Sex.Male, new Ticket(SeatClass.Economy, 130M))
            //    }));
            #endregion

            do
            {
                Console.Clear();
                InputOutputHelper.PrintColorText("********** AIRLINE **********\n", ConsoleColor.DarkGreen);

                Console.WriteLine(@"Please make the choise (enter the number):

                1. View all flights (without passengers);
                2. Search a flight;
                3. View all flight’s passengers;
                4. Search passengers;
                5. Search flights with lower price;
                6. Add, delete, edit flights;
                7. Add, delete, edit passengers.");

                Console.Write("Your choise: ");

                _menuManager.MenuHandler = ManageMainMenu;
                _menuManager.HandleExceptions();

                InputOutputHelper.PrintColorText("\nPress \"Space\" to exit; press any key to return to the main menu\n", ConsoleColor.DarkGreen);
            }
            while (Console.ReadKey().Key != ConsoleKey.Spacebar);
        }

        static void ManageMainMenu()
        {

            int index = (int)uint.Parse(Console.ReadLine());
            IDictionary<int, MenuItemHandler> menuItems = new Dictionary<int, MenuItemHandler>
                    {
                        { 1, airlineManager.ViewAllFlights },
                        { 2, airlineManager.SearchFlight },
                        { 3, airlineManager.ViewPassengers },
                        { 4, airlineManager.SearchPassengers },
                        { 5, airlineManager.SearchLowerPrice },
                        { 6, airlineManager.EditFlights },
                        //{ 7, airlineManager }
                    };
            _menuManager.MenuItemHandler = menuItems[index];
            _menuManager.CallMenuItem();
        }
    }
}

