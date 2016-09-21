using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Airline.SetDateStrategy
{
    class BirthdayDate : IDate
    {
        public DateTime CreateDate()
        {
            DateTime birthdayDate = default(DateTime);
            do
            {
                Console.Write("\nEnter a passenger birthday date in the following format - (Year-Month-Day): ");
                string dateTime = Console.ReadLine();
                string pattern = @"";
                var matches = Regex.Matches(dateTime, pattern);
                if (matches.Count.Equals(1))
                {
                    string date = null;
                    foreach (Match match in matches)
                    {
                        date = match.Value;
                    }
                    var parameters = date.Split('-').ToList().ConvertAll(x => int.Parse(x));
                    birthdayDate = new DateTime(parameters[0], parameters[1], parameters[2]);
                }
                else InputOutputHelper.PrintColorText("\nWrong format of passenger birthday date. Please re-enter data!", ConsoleColor.Red);
            } while (birthdayDate == default(DateTime));
            return birthdayDate;
        }
    }
}
