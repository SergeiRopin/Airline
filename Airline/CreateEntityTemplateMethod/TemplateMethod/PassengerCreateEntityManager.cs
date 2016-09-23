using Airline.SetDateStrategy;
using Airline.TemplateMethod;
using AirportManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.TemplateMethod
{
    class PassengerCreateEntityManager : CreateEntityManager<IEntity>
    {
        protected override bool AskQuestionCreateEntity()
        {
            Console.Write("\nDo you want to create a new passenger? (Y/N): ");
            var answer = Console.ReadLine();
            return string.Equals(answer, "y", StringComparison.OrdinalIgnoreCase);
        }

        protected override string ReadAnswerCreateEntity()
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
            return new Passenger(
                parameters[0],
                parameters[1],
                parameters[2],
                parameters[3],
                Convert.ToDateTime(parameters[4]),
                (Sex)Enum.Parse(typeof(Sex),
                parameters[5]),
                new Ticket((SeatClass)Enum.Parse(typeof(SeatClass), parameters[6]), Convert.ToDecimal(parameters[7])));
        }

        protected override bool AskQuestionEditEntity()
        {
            Console.Write("\nDo you want to edit the passenger information? (Y/N): ");
            var answer = Console.ReadLine();
            return string.Equals(answer, "y", StringComparison.OrdinalIgnoreCase);
        }

        protected override string ReadAnswerEditEntity(IEntity actualPassenger)
        {
            var entityHelper = new CreateEditEntityHelper();
            var answer = new StringBuilder();

            //Edit first name.
            var actualFirstName = actualPassenger.GetType().GetProperty("FirstName").GetValue(actualPassenger);
            InputOutputHelper.PrintColorText($"\nActual passenger first name: {actualFirstName}", ConsoleColor.DarkCyan);
            Func<string> firstNameHandler = CreateFirstName;
            var firstName = entityHelper.AskEditEntity(firstNameHandler);
            firstName = entityHelper.UpdateEntityOrKeepDefault((string)actualFirstName, firstName);
            answer.Append(AddSeparator(firstName.ToString()));

            //Edit last name.
            var actualLastName = actualPassenger.GetType().GetProperty("LastName").GetValue(actualPassenger);
            InputOutputHelper.PrintColorText($"\nActual passenger last name: {actualLastName}", ConsoleColor.DarkCyan);
            Func<string> lastNameHandler = CreateLastName;
            var lastName = entityHelper.AskEditEntity(lastNameHandler);
            lastName = entityHelper.UpdateEntityOrKeepDefault((string)actualLastName, lastName);
            answer.Append(AddSeparator(lastName.ToString()));

            //Edit nationality.
            var actualNationality = actualPassenger.GetType().GetProperty("Nationality").GetValue(actualPassenger);
            InputOutputHelper.PrintColorText($"\nActual passenger nationality: {actualNationality}", ConsoleColor.DarkCyan);
            Func<string> nationalityHandler = CreateNationality;
            string nationality = entityHelper.AskEditEntity(nationalityHandler);
            nationality = entityHelper.UpdateEntityOrKeepDefault((string)actualNationality, nationality);
            answer.Append(AddSeparator(nationality));

            //Edit passport.
            var actualPassport = actualPassenger.GetType().GetProperty("Passport").GetValue(actualPassenger);
            InputOutputHelper.PrintColorText($"\nActual passenger passport: {actualPassport}", ConsoleColor.DarkCyan);
            Func<string> passportHandler = CreatePassport;
            string passport = entityHelper.AskEditEntity(passportHandler);
            passport = entityHelper.UpdateEntityOrKeepDefault((string)actualPassport, passport);
            answer.Append(AddSeparator(passport));

            //Edit birthday.
            var actualBirthday = actualPassenger.GetType().GetProperty("Birthday").GetValue(actualPassenger);
            InputOutputHelper.PrintColorText($"\nActual passenger birthday: {actualBirthday}", ConsoleColor.DarkCyan);
            Func<DateTime> birthdayHandler = CreateBirthday;
            var birthday = entityHelper.AskEditEntity(birthdayHandler);
            birthday = entityHelper.UpdateEntityOrKeepDefault((DateTime)actualBirthday, birthday);
            answer.Append(AddSeparator(birthday.ToString()));

            //Edit sex.
            var actualSex = actualPassenger.GetType().GetProperty("Sex").GetValue(actualPassenger);
            InputOutputHelper.PrintColorText($"\nActual passenger sex: {actualSex}", ConsoleColor.DarkCyan);
            Func<Sex> sexHandler = CreateSex;
            var sex = entityHelper.AskEditEntity(sexHandler);
            sex = entityHelper.UpdateEntityOrKeepDefault((Sex)actualSex, sex);
            answer.Append(AddSeparator(sex.ToString()));

            //Edit ticket seat class.
            var actualTicket = actualPassenger.GetType().GetProperty("Ticket").GetValue(actualPassenger);
            var actualSeatClass = actualTicket.GetType().GetProperty("SeatClass").GetValue(actualTicket);
            InputOutputHelper.PrintColorText($"\nActual ticket seat class: {actualSeatClass}", ConsoleColor.DarkCyan);
            Func<SeatClass> seatClassHandler = CreateSeatClass;
            var seatClass = entityHelper.AskEditEntity(seatClassHandler);
            seatClass = entityHelper.UpdateEntityOrKeepDefault((SeatClass)actualSeatClass, seatClass);
            answer.Append(AddSeparator(seatClass.ToString()));

            //Edit ticket price.
            var actualPrice = actualTicket.GetType().GetProperty("Price").GetValue(actualTicket);
            InputOutputHelper.PrintColorText($"\nActual ticket price: {actualPrice}", ConsoleColor.DarkCyan);
            Func<decimal> priceHandler = CreatePrice;
            var price = entityHelper.AskEditEntity(priceHandler);
            price = entityHelper.UpdateEntityOrKeepDefault((decimal)actualPrice, price);
            answer.Append(price.ToString());

            return answer.ToString();
        }

        protected override IEntity EditEntity(string value, IEntity actualPassenger)
        {
            var entity = CreateEntity(value);
            return entity;
        }

        #region CreatePassengerMethods

        private string CreateFirstName()
        {
            string firstName = InputOutputHelper.SetString("\nEnter a first name: ");
            return firstName;
        }

        private string CreateLastName()
        {
            string lastName = InputOutputHelper.SetString("\nEnter a last name: ");
            return lastName;
        }

        private string CreateNationality()
        {
            string nationality = InputOutputHelper.SetString("\nEnter a nationalty: ");
            return nationality;
        }

        private string CreatePassport()
        {
            string passport = InputOutputHelper.SetString("\nEnter a passport: ");
            return passport;
        }

        private DateTime CreateBirthday()
        {
            SetDateHelper dateHelper = new SetDateHelper(new BirthdayDate());
            DateTime birthdayDate = dateHelper.CreateDate();
            return birthdayDate;
        }

        private Sex CreateSex()
        {
            var sex = InputOutputHelper.SetEnum<Sex>("\n" + @"Enter a sex of the passenger. Choose a number from the following list:
                1. Male
                2. Female");
            return sex;
        }

        private SeatClass CreateSeatClass()
        {
            var seatClass = InputOutputHelper.SetEnum<SeatClass>(@"Enter a seat class. Choose a number from the following list:
                1. Economy
                2. Business");
            return seatClass;
        }

        private decimal CreatePrice()
        {
            decimal price = InputOutputHelper.SetValueType<decimal>("\nEnter a ticket price (dollars): ");
            return price;
        }
        #endregion
    }
}
