using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            { 
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("**********AIRLINE**********\n");
                Console.ResetColor();
                do
                {
                    Console.WriteLine(@"Please make your choise:

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
                        switch (index)
                        {
                            case 1:
                                //EditFlight();
                                break;

                            case 2:
                                //DeleteFlight();
                                break;

                            case 3:
                                //DisplayFlight();
                                break;

                            case 4:
                                //SearchFlight();
                                break;

                            case 5:

                                break;

                            case 6:

                                break;

                            default:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nPlease choose a number from the menu list!");
                                Console.ResetColor();
                                break;
                        }
                    }
                    catch (OverflowException ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nUnexpected value has been entered!" + ex.Message);
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nWrong input! " + ex.Message);
                        Console.ResetColor();
                    }

                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\nPress \"Space\" to exit; press any key to return to the main menu\n");
                    Console.ResetColor();
                }
                while (Console.ReadKey().Key != ConsoleKey.Spacebar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
