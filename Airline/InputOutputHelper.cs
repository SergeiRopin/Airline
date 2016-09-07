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

        public static void StringInput(out string input, string inputMessage)
        {
            do
            {
                Console.Write(inputMessage);
                input = Console.ReadLine();
            }
            while (String.IsNullOrWhiteSpace(input));
        }

        public static void EnumInput<T>(out T input, string inputMessage)
        {
            bool failure = false;
            //T temp = default(T);
            input = default(T);
            do
            {
                try
                {
                    Console.WriteLine(inputMessage);
                    Console.Write("Please enter a number: ");
                    input = (T)Enum.Parse(typeof(T), Console.ReadLine());
                    failure = false;
                }
                catch (Exception)
                {
                    PrintColorText("\nWrong value has been entered! Please choose a number from the list", ConsoleColor.Red);
                    failure = true;
                }
                //input = input;
            }
            while (failure);
        }

        public static void ValueInput(out int input, string inputMessage)
        {
            input = default(int);
            bool failure = false;
            do
            {
                try
                {
                    Console.Write(inputMessage);
                    input = (int)uint.Parse(Console.ReadLine());
                    failure = false;
                }
                catch (Exception)
                {
                    PrintColorText("\nWrong value has been entered!", ConsoleColor.Red);
                    failure = true;
                }
            }
            while (failure);
        }
    }
}
