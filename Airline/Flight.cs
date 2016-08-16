using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    class Flight
    {
        public int Number { get; private set; }
        public string City { get; private set; }
        public string Terminal { get; private set; }
        public string Gate { get; private set; }
        public FlightStatus Status { get; private set; }
        public List<Passenger> PassengersList { get; private set; }

        public Flight(int number, string city, string terminal, string gate, FlightStatus status, List<Passenger> passengerslist)
        {
            Number = number;
            City = city;
            Terminal = terminal;
            Gate = gate;
            Status = status;
            PassengersList = passengerslist;
        }

        public override string ToString()
        {
            return $"Flight information";
        }
    }
}
