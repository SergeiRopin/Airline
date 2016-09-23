using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportManager
{
    public class Passenger : IEntity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Nationality { get; private set; }
        public string Passport { get; private set; }
        public DateTime Birthday { get; private set; }
        public Sex Sex { get; private set; }
        public Ticket Ticket { get; private set; }

        public Passenger(string firstName, string lastName, string nationality, string passport, DateTime birthday,
            Sex sex, Ticket ticket)
        {
            FirstName = firstName.ToUpper();
            LastName = lastName.ToUpper();
            Nationality = nationality.ToUpper();
            Passport = passport.ToUpper();
            Birthday = birthday;
            Sex = sex;
            Ticket = ticket;
        }

        public override string ToString() =>
            $@"Name: {FirstName} {LastName}, Nationality: {Nationality}, Passport: {Passport}, " +
                $"Birthday: {Birthday.ToShortDateString()}, Sex: {Sex}, " + Ticket;
    }
}
