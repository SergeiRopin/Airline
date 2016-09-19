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

            string firstName = CreateFirstName();
            answer.Append(AddSeparator(firstName));

            string lastName = CreateLastName();
            answer.Append(AddSeparator(lastName));

            string nationality = CreateNationality();
            answer.Append(AddSeparator(nationality));

            string passport = CreatePassport();
            answer.Append(AddSeparator(passport));

            DateTime birthday = CreateBirthday();
            answer.Append(AddSeparator(birthday.ToString()));

            var sex = CreateSex();
            answer.Append(AddSeparator(sex.ToString()));

            var seatClass = CreateSeatClass();
            answer.Append(AddSeparator(seatClass.ToString()));

            decimal price = CreatePrice();
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
            Console.Write("\nDo you want to edit the passenger information? (Y/N): ");
            var answer = Console.ReadLine();
            return string.Equals(answer, "y", StringComparison.OrdinalIgnoreCase);
        }

        protected override string ReadEditAnswer(IEntity actualPassenger)
        {
            var editEntityHelper = new EditEntityHelper();
            var answer = new StringBuilder();

            //Edit first name.
            var actualFirstName = actualPassenger.GetType().GetProperty("FirstName").GetValue(actualPassenger);
            InputOutputHelper.PrintColorText($"\nActual passenger first name: {actualFirstName}", ConsoleColor.DarkCyan);
            Func<string> firstNameHandler = CreateFirstName;
            var firstName = editEntityHelper.EditEntity(firstNameHandler);
            firstName = editEntityHelper.UpdatePropertyOrKeepDefault((string)actualFirstName, firstName);
            answer.Append(AddSeparator(firstName.ToString()));

            //Edit last name.
            var actualLastName = actualPassenger.GetType().GetProperty("LastName").GetValue(actualPassenger);
            InputOutputHelper.PrintColorText($"\nActual passenger last name: {actualLastName}", ConsoleColor.DarkCyan);
            Func<string> lastNameHandler = CreateLastName;
            var lastName = editEntityHelper.EditEntity(lastNameHandler);
            lastName = editEntityHelper.UpdatePropertyOrKeepDefault((string)actualLastName, lastName);
            answer.Append(AddSeparator(lastName.ToString()));

            //Edit nationality.
            var actualNationality = actualPassenger.GetType().GetProperty("Nationality").GetValue(actualPassenger);
            InputOutputHelper.PrintColorText($"\nActual passenger nationality: {actualNationality}", ConsoleColor.DarkCyan);
            Func<string> nationalityHandler = CreateNationality;
            string nationality = editEntityHelper.EditEntity(nationalityHandler);
            nationality = editEntityHelper.UpdatePropertyOrKeepDefault((string)actualNationality, nationality);
            answer.Append(AddSeparator(nationality));

            //Edit passport.
            var actualPassport = actualPassenger.GetType().GetProperty("Passport").GetValue(actualPassenger);
            InputOutputHelper.PrintColorText($"\nActual passenger passport: {actualPassport}", ConsoleColor.DarkCyan);
            Func<string> passportHandler = CreatePassport;
            string passport = editEntityHelper.EditEntity(passportHandler);
            passport = editEntityHelper.UpdatePropertyOrKeepDefault((string)actualPassport, passport);
            answer.Append(AddSeparator(passport));

            //Edit birthday.
            var actualBirthday = actualPassenger.GetType().GetProperty("Birthday").GetValue(actualPassenger);
            InputOutputHelper.PrintColorText($"\nActual passenger birthday: {actualBirthday}", ConsoleColor.DarkCyan);
            Func<DateTime> birthdayHandler = CreateBirthday;
            var birthday = editEntityHelper.EditEntity(birthdayHandler);
            birthday = editEntityHelper.UpdatePropertyOrKeepDefault((DateTime)actualBirthday, birthday);
            answer.Append(AddSeparator(birthday.ToString()));

            //Edit sex.
            var actualSex = actualPassenger.GetType().GetProperty("Sex").GetValue(actualPassenger);
            InputOutputHelper.PrintColorText($"\nActual passenger sex: {actualSex}", ConsoleColor.DarkCyan);
            Func<Sex> sexHandler = CreateSex;
            var sex = editEntityHelper.EditEntity(sexHandler);
            sex = editEntityHelper.UpdatePropertyOrKeepDefault((Sex)actualSex, sex);
            answer.Append(AddSeparator(sex.ToString()));

            //Edit ticket seat class.
            var actualTicket = actualPassenger.GetType().GetProperty("Ticket").GetValue(actualPassenger);
            var actualSeatClass = actualTicket.GetType().GetProperty("SeatClass").GetValue(actualTicket);
            InputOutputHelper.PrintColorText($"\nActual ticket seat class: {actualSeatClass}", ConsoleColor.DarkCyan);
            Func<SeatClass> seatClassHandler = CreateSeatClass;
            var seatClass = editEntityHelper.EditEntity(seatClassHandler);
            seatClass = editEntityHelper.UpdatePropertyOrKeepDefault((SeatClass)actualSeatClass, seatClass);
            answer.Append(AddSeparator(seatClass.ToString()));

            //Edit ticket price.
            var actualPrice = actualTicket.GetType().GetProperty("Price").GetValue(actualTicket);
            InputOutputHelper.PrintColorText($"\nActual ticket price: {actualPrice}", ConsoleColor.DarkCyan);
            Func<decimal> priceHandler = CreatePrice;
            var price = editEntityHelper.EditEntity(priceHandler);
            price = editEntityHelper.UpdatePropertyOrKeepDefault((decimal)actualPrice, price);
            answer.Append(price.ToString());

            return answer.ToString();
        }

        #region CreatePassengerMethods

        private string CreateFirstName()
        {
            string firstName = InputOutputHelper.CheckStringInput("\nEnter a first name: ");
            return firstName;
        }

        private string CreateLastName()
        {
            string lastName = InputOutputHelper.CheckStringInput("\nEnter a last name: ");
            return lastName;
        }

        private string CreateNationality()
        {
            string nationality = InputOutputHelper.CheckStringInput("\nEnter a nationalty: ");
            return nationality;
        }

        private string CreatePassport()
        {
            string passport = InputOutputHelper.CheckStringInput("\nEnter a passport: ");
            return passport;
        }

        private DateTime CreateBirthday()
        {
            DateTime birthday = InputOutputHelper.CheckDateTimeInput("\nEnter a passenger birthday: ");
            return birthday;
        }

        private Sex CreateSex()
        {
            var sex = InputOutputHelper.CheckEnumInput<Sex>("\n" + @"Enter a sex of the passenger. Choose a number from the following list:
                1. Male
                2. Female");
            return sex;
        }

        private SeatClass CreateSeatClass()
        {
            var seatClass = InputOutputHelper.CheckEnumInput<SeatClass>(@"Enter a seat class. Choose a number from the following list:
                1. Economy
                2. Business");
            return seatClass;
        }

        private decimal CreatePrice()
        {
            decimal price = InputOutputHelper.CheckValueTypeInput<decimal>("\nEnter a ticket price (dollars): ");
            return price;
        }
        #endregion
    }
}
