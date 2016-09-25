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
        void AddFlight(Flight flight);
        void DeleteFlight(Flight flight);
        void EditFlight(Flight actualFlight, Flight updatedFlight);
        IEnumerable<Flight> FilterFlights(Func<Flight, bool> predicate);
    }
}
