using AirportManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    public class EditingFlightEventArgs : EventArgs
    {
        Flight _actualFlight;
        Flight _updatedFlight;

        public EditingFlightEventArgs(Flight actualFlight, Flight updatedFlight)
        {
            _actualFlight = actualFlight;
            _updatedFlight = updatedFlight;
        }

        public Flight ActualFlight => _actualFlight;
        public Flight UpdatedFlight => _updatedFlight;
    }
}
