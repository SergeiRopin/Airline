using Airline;
using AirportManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresenterStorage
{
    public class Presenter
    {
        IView _view;
        IAirportModel _airport;

        public Presenter(IView view)
        {
            _view = view;
            _airport = Airport.Instance;
            //_airport = AirportFactory.Create();
            Initialize();
        }

        private void Initialize()
        {
            _view.AddFlightEventRaised += AddFlightEventHandler;
        }

        private void AddFlightEventHandler(object sender, FlightEventArgs e)
        {
            _airport.AddFlight(e.Flight);
        }
    }
}
