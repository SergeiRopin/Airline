using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportManager
{
    public enum FlightStatus
    {
        CheckIn = 1,
        GateClosed,
        Arrived,
        DeparturedAt,
        Unknown,
        Canceled,
        ExpectedAt,
        Delayed,
        InFlight,
        Boarding
    }
}
