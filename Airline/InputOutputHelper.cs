using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    class InputOutputHelper
    {
        public static void PrintColorText(string text, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static string CheckStringInput(string infoMessage)
        {
            string input = null;
            do
            {
                Console.Write(infoMessage);
                input = Console.ReadLine();
            }
            while (String.IsNullOrWhiteSpace(input));
            return input;
        }

        public static T CheckEnumInput<T>(string infoMessage)
        {
            bool failure;
            T input = default(T);
            do
            {
                try
                {
                    Console.WriteLine(infoMessage);
                    Console.Write("Please enter a number: ");
                    input = (T)Enum.Parse(typeof(T), Console.ReadLine());
                    if (Enum.IsDefined(typeof(T), input))
                        failure = false;
                    else
                        throw new Exception();
                }
                catch (Exception)
                {
                    PrintColorText("\nWrong value has been entered! Please choose a number from the list\n", ConsoleColor.Red);
                    failure = true;
                }
            }
            while (failure);
            return input;
        }

        public static int CheckInt32Input(string infoMessage)
        {
            int input = default(int);
            bool failure = false;
            do
            {
                try
                {
                    Console.Write(infoMessage);
                    input = (int)uint.Parse(Console.ReadLine());
                    failure = false;
                }
                catch (Exception)
                {
                    PrintColorText("\nWrong value has been entered. Please check the input!\n", ConsoleColor.Red);
                    failure = true;
                }
            }
            while (failure);
            return input;
        }

        public static decimal CheckDecimalInput(string infoMessage)
        {
            decimal input = default(decimal);
            bool failure = false;
            do
            {
                try
                {
                    Console.Write(infoMessage);
                    input = decimal.Parse(Console.ReadLine());
                    failure = false;
                }
                catch (Exception)
                {
                    PrintColorText("\nWrong value has been entered. Please check the input!\n", ConsoleColor.Red);
                    failure = true;
                }
            }
            while (failure);
            return input;
        }

        public static DateTime CheckDateTimeInput(string infoMessage)
        {
            DateTime flightTime = default(DateTime);
            bool failure = false;
            do
            {
                try
                {
                    Console.Write(infoMessage);
                    int year = CheckInt32Input("\nYear: ");
                    int month = CheckInt32Input("Month (from 01 to 12): ");
                    int day = CheckInt32Input("Day (from 01 to 31): ");
                    int hours = CheckInt32Input("Hours (from 0 to 23): ");
                    int minutes = CheckInt32Input("Minutes (from 0 to 59): ");

                    flightTime = new DateTime(year, month, day, hours, minutes, 00);
                    failure = false;
                }
                catch (Exception)
                {
                    PrintColorText("\nWrong value was encountered during input."
                        + "\nPlease re-enter data!", ConsoleColor.Red);
                    failure = true;
                }
            }
            while (failure);
            return flightTime;
        }
    }
}
