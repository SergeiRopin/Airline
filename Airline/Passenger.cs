using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    class Passenger
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Nationality { get; private set; }
        public string Passport { get; private set; }
        public string Birthday { get; private set; }
        public Sex Sex { get; private set; }
        public Ticket Ticket { get; private set; }

        //Ticket ticket = new Ticket();
        public Passenger(string firstName, string lastName, string nationality, string passport, string birthday,
            Sex sex, Ticket ticket)
        {
            FirstName = firstName;
            LastName = lastName;
            Nationality = nationality;
            Passport = passport;
            Birthday = birthday;
            Sex = sex;
            Ticket = ticket;
        }

        public override string ToString()
        {
            return $"Passenger info";
        }
    }
}
