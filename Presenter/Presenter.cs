using Airline;
using Airport.Exceptions;
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
            _airport = AirportFactory.Create();
            Initialize();
        }

        private void Initialize()
        {
            _view.AddingFlightEventRaised += AddingFlightEventHandler;
            _view.DeletingFlightEventRaised += DeletingFlightEventHandler;
            _view.EditingFlightEventRaised += EditingFlightEventHandler;
            _view.FilteringFlightsEventRaised += FilteringFlightsEventHandler;
        }

        private void AddingFlightEventHandler(object sender, FlightEventArgs e) => _airport.AddFlight(e.Flight);

        private void DeletingFlightEventHandler(object sender, FlightEventArgs e) => _airport.DeleteFlight(e.Flight);

        private void EditingFlightEventHandler(object sender, EditingFlightEventArgs e) => _airport.EditFlight(e.ActualFlight, e.UpdatedFlight);

        private IEnumerable<Flight> FilteringFlightsEventHandler(FilteringFlightsEventArgs e) => _airport.FilterFlights(e.Predicate);
    }
}
