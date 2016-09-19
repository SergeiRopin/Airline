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

        public static string CreateString(string infoMessage)
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

        public static T CreateEnum<T>(string infoMessage)
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
                    PrintColorText("\nWrong value has been entered! Please choose a number from the list!", ConsoleColor.Red);
                    failure = true;
                }
            }
            while (failure);
            return input;
        }

        public static T CreateValueType<T>(string infoMessage) where T : struct
        {
            T input = default(T);
            bool failure = false;
            do
            {
                try
                {
                    Console.Write(infoMessage);
                    input = (T)Convert.ChangeType(Console.ReadLine(), typeof(T));
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

        public static DateTime CreateDateTime(string infoMessage)
        {
            DateTime flightTime = default(DateTime);
            bool failure = false;
            do
            {
                try
                {
                    Console.Write(infoMessage);
                    int year = CreateValueType<int>("\nYear: ");
                    int month = CreateValueType<int>("Month (from 01 to 12): ");
                    int day = CreateValueType<int>("Day (from 01 to 31): ");
                    int hours = CreateValueType<int>("Hours (from 0 to 23): ");
                    int minutes = CreateValueType<int>("Minutes (from 0 to 59): ");

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
