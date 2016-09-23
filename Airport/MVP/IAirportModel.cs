using AirportManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportManager
{
    public interface IAirportModel
    {
        IEnumerable<Flight> Flights { get; }

        void AddFlight(Flight flight);
    }
}
