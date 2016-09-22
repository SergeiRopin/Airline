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
                try
                {
                    var parameters = dateTime.Split('-').ToList().ConvertAll(x => int.Parse(x));
                    birthdayDate = new DateTime(parameters[0], parameters[1], parameters[2]);
                }
                catch (Exception)
                {
                    InputOutputHelper.PrintColorText("\nWrong format of passenger birthday date. Please re-enter data!", ConsoleColor.Red);
                }
            } while (birthdayDate == default(DateTime));
            return birthdayDate;
        }
    }
}
