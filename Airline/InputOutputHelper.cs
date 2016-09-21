using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public static string SetString(string infoMessage)
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

        public static T SetEnum<T>(string infoMessage)
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

        public static T SetValueType<T>(string infoMessage) where T : struct
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
    }
}
