using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    class Flight
    {
        public string Number { get; private set; }
        public string CityFrom { get; private set; }
        public string CityTo { get; private set; }
        public Terminal Terminal { get; private set; }
        public Gate Gate { get; private set; }
        public FlightStatus Status { get; private set; }
        public DateTime DateTime { get; private set; }
        public List<Passenger> PassengersList { get; private set; }

        public Flight(string number, string cityFrom, string cityTo, Terminal terminal, Gate gate, FlightStatus status, DateTime dateTime,  List<Passenger> passengersList)
        {
            Number = number;
            CityFrom = cityFrom;
            CityTo = cityTo;
            Terminal = terminal;
            Gate = gate;
            Status = status;
            DateTime = dateTime;
            PassengersList = passengersList;
        }

        public override string ToString()
        {
            return $"Flight information";
        }
    }
}
