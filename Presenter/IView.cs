using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airline;
using AirportManager;

namespace PresenterStorage
{
    public interface IView
    {
        event EventHandler<FlightEventArgs> AddingFlightEventRaised;
        event EventHandler<FlightEventArgs> DeletingFlightEventRaised;
        event EventHandler<EditingFlightEventArgs> EditingFlightEventRaised;        
        event Func<FilteringFlightsEventArgs, IEnumerable<Flight>> FilteringFlightsEventRaised;
    }
}
