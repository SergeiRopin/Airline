using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Airline.SetDateStrategy
{
    class FlightDate : IDate
    {
        public DateTime CreateDate()
        {
            DateTime flightTime = default(DateTime);
            do
            {
                Console.Write("\nEnter a flight time in the following format - (Year-Month-Day-Hours-Minutes): ");
                string dateTime = Console.ReadLine();
                try
                {
                    var parameters = dateTime.Split('-').ToList().ConvertAll(x => int.Parse(x));
                    flightTime = new DateTime(parameters[0], parameters[1], parameters[2], parameters[3], parameters[4], 0);
                }
                catch (Exception)
                {
                    InputOutputHelper.PrintColorText("\nWrong format of flight date or time. Please re-enter data!", ConsoleColor.Red);
                }
            } while (flightTime == default(DateTime));
            return flightTime;
        }
    }
}
