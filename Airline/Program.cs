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
        static void Main(string[] args)
        {
            AirlineLogic airlineLogic = new AirlineLogic();
            AuxiliaryClass.PrintColorText("**********AIRLINE**********\n", ConsoleColor.DarkGreen);
            Console.WindowHeight = 50;
            Console.WindowWidth = 150;
            do
            {
                Console.WriteLine(@"Please make the choise (enter the number):

                1. View all flights (without passengers);
                2. View all flight’s passengers;
                3. Search passengers;
                4. Search a flight with lower price;
                5. Edit flights information;
                6. Edit passengers information.");

                try
                {
                    Console.Write("Your choise: ");
                    int index = (int)uint.Parse(Console.ReadLine());
                    IDictionary<int, MenuHandler> menuItems = new Dictionary<int, MenuHandler>
                        {
                            { 1, airlineLogic.ViewAllFlights },
                            { 2, airlineLogic.ViewPassengers },
                            //{ 3, airlineLogic. },
                        };
                    MenuCaller menuCaller = new MenuCaller();
                    menuCaller.MenuHandler = menuItems[index];
                    menuCaller.CallMenuItem();

                    #region MyRegion
                    //switch (index)
                    //{
                    //    case 1:
                    //        // DisplayFlights();
                    //        //EditFlight();
                    //        break;

                    //    case 2:
                    //        //DeleteFlight();
                    //        break;

                    //    case 3:
                    //        //DisplayFlight();
                    //        break;

                    //    case 4:
                    //        //SearchFlight();
                    //        break;

                    //    case 5:

                    //        break;

                    //    case 6:

                    //        break;

                    //default:
                    //Console.ForegroundColor = ConsoleColor.Red;
                    //Console.WriteLine("\nPlease choose a number from the menu list!");
                    //Console.ResetColor();
                    //break;
                    //}

                    #endregion
                }

                catch (OverflowException ex)
                {
                    AuxiliaryClass.PrintColorText("\nUnexpected value has been entered! " + ex.Message, ConsoleColor.Red);
                }
                catch (Exception ex)
                {
                    AuxiliaryClass.PrintColorText("\nWrong input! " + ex.Message, ConsoleColor.Red);
                }
                AuxiliaryClass.PrintColorText("\nPress \"Space\" to exit; press any key to return to the main menu\n", ConsoleColor.DarkGreen);
            }
            while (Console.ReadKey().Key != ConsoleKey.Spacebar);
        }
    }
}

