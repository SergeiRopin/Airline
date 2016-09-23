using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airline;

namespace PresenterStorage
{
    public interface IView
    {
        event EventHandler<FlightEventArgs> AddFlightEventRaised;

        
    }
}
