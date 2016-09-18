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
                    PrintColorText("\nWrong value has been entered! Please choose a number from the list!", ConsoleColor.Red);
                    failure = true;
                }
            }
            while (failure);
            return input;
        }

        public static T CheckValueTypeInput<T>(string infoMessage) where T : struct
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

        public static DateTime CheckDateTimeInput(string infoMessage)
        {
            DateTime flightTime = default(DateTime);
            bool failure = false;
            do
            {
                try
                {
                    Console.Write(infoMessage);
                    int year = CheckValueTypeInput<int>("\nYear: ");
                    int month = CheckValueTypeInput<int>("Month (from 01 to 12): ");
                    int day = CheckValueTypeInput<int>("Day (from 01 to 31): ");
                    int hours = CheckValueTypeInput<int>("Hours (from 0 to 23): ");
                    int minutes = CheckValueTypeInput<int>("Minutes (from 0 to 59): ");

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



        public static T CheckEditedEntityToDefaultProperties<T>(T actual, T updated) where T : class
        {
            T updatedEntity = updated;
            if (actual != null && updated != null)
            {
                Type type = typeof(T);
                var properties = type.GetProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    var valueType = properties[i].PropertyType.IsValueType;
                    object actualValue = type.GetProperty(properties[i].Name).GetValue(actual);
                    object updatedValue = type.GetProperty(properties[i].Name).GetValue(updated);
                    object o = updatedEntity.GetType().GetProperty(properties[i].Name).GetValue(updated);

                    if (valueType)
                    {
                        o = actualValue;
                        updatedValue = actualValue;
                    }
                }
            }
            return updated;
        }
    }
}
