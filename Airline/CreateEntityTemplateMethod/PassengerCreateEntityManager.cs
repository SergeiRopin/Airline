using Airline.TemplateMethod;
using Airport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.TemplateMethod
{
    class PassengerCreateEntityManager : CreateEntityManager<IEntity>
    {
        protected override bool AskQuestion()
        {
            Console.Write("\nDo you want to create a new passenger? (Y/N): ");
            var answer = Console.ReadLine();
            return string.Equals(answer, "y", StringComparison.OrdinalIgnoreCase);
        }

        protected override string ReadAnswer()
        {
            var answer = new StringBuilder();

            string firstName = InputOutputHelper.CheckStringInput("\nEnter a first name: ");
            answer.Append(AddSeparator(firstName));

            string lastName = InputOutputHelper.CheckStringInput("\nEnter a last name: ");
            answer.Append(AddSeparator(lastName));

            string nationality = InputOutputHelper.CheckStringInput("\nEnter a nationalty: ");
            answer.Append(AddSeparator(nationality));

            string passport = InputOutputHelper.CheckStringInput("\nEnter a passport: ");
            answer.Append(AddSeparator(passport));

            DateTime birthday = InputOutputHelper.CheckDateTimeInput("\nEnter a passenger birthday: ");
            answer.Append(AddSeparator(birthday.ToString()));

            var sex = InputOutputHelper.CheckEnumInput<Sex>("\n" + @"Enter a sex of the passenger. Choose a number from the following list:
                1. Male
                2. Female");
            answer.Append(AddSeparator(sex.ToString()));

            var seatClass = InputOutputHelper.CheckEnumInput<SeatClass>(@"Enter a seat class. Choose a number from the following list:
                1. Economy
                2. Business");
            answer.Append(AddSeparator(seatClass.ToString()));

            decimal price = InputOutputHelper.CheckValueTypeInput<decimal>("\nEnter a ticket price (dollars): ");
            answer.Append(price.ToString());

            return answer.ToString();
        }

        protected override bool IsValid(string value)
        {
            var parameters = value.Split('|');
            return parameters.Count() == 8;
        }

        protected override IEntity CreateEntity(string value)
        {
            var parameters = value.Split('|');
            return new Passenger(parameters[0], parameters[1], parameters[2], parameters[3],
               Convert.ToDateTime(parameters[4]), (Sex)Enum.Parse(typeof(Sex), parameters[5]),
               new Ticket((SeatClass)Enum.Parse(typeof(SeatClass), parameters[6]), Convert.ToDecimal(parameters[7])));
        }

        protected override bool AskEditQuestion()
        {
            throw new NotImplementedException();
        }

        protected override string ReadEditAnswer(IEntity actualPassenger)
        {
            throw new NotImplementedException();
        }

        protected override IEntity EditEntity(string value, IEntity actualPassenger)
        {
            throw new NotImplementedException();
        }
    }
}
