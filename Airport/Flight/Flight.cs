using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportManager
{
    public class Flight : IEntity
    {
        public ArrivalDeparture ArrivalDeparture { get; set; }
        public string Number { get; private set; }
        public string CityFrom { get; private set; }
        public string CityTo { get; private set; }
        public string Airline { get; private set; }
        public Terminal Terminal { get; private set; }
        public Gate Gate { get; private set; }
        public FlightStatus Status { get; private set; }
        public DateTime DateTime { get; private set; }
        public List<Passenger> Passengers { get; private set; }

        public Flight(ArrivalDeparture arrivalDeparture, string number, string cityFrom, string cityTo, string airline,
            Terminal terminal, Gate gate, FlightStatus status, DateTime dateTime, List<Passenger> passengers)
        {
            ArrivalDeparture = arrivalDeparture;
            Number = number.ToUpper();
            CityFrom = cityFrom.ToUpper();
            CityTo = cityTo.ToUpper();
            Airline = airline.ToUpper();
            Terminal = terminal;
            Gate = gate;
            Status = status;
            DateTime = dateTime;
            Passengers = passengers;
        }

        public override string ToString() =>
            $"Flight number: {Number}, From: {CityFrom}, To: {CityTo}, Airline: {Airline}, Terminal: {Terminal}, Gate: {Gate}, Status: {Status}, Time: {DateTime}";
    }
}
