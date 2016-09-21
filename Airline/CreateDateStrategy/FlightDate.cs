using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Airline.CreateDateStrategy
{
    class FlightDate : IDate
    {
        public DateTime CreateDate()
        {
            DateTime flightTime = default(DateTime);
            do
            {
                Console.Write("\nEnter a flight time in the following format - (Year|Month|Day|Hours|Minutes): ");
                string dateTime = Console.ReadLine();
                string pattern = @"\d{1,4}(\|\d{1,2}){4}";
                var matches = Regex.Matches(dateTime, pattern);
                if (matches.Count.Equals(1))
                {
                    string date = null;
                    foreach (Match match in matches)
                    {
                        date = match.Value;
                    }
                    var parameters = date.Split('|').ToList().ConvertAll(x=> int.Parse(x));
                    flightTime = new DateTime(parameters[0], parameters[1], parameters[2], parameters[3], parameters[4], 0);
                }
            } while (flightTime == default(DateTime));
            return flightTime;
        }
    }
}
