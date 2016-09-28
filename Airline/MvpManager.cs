using PresenterStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportManager;
using Airport.Exceptions;

namespace Airline
{
    /// <summary>
    /// Contains events for MVP and methods to raise them
    /// </summary>
    public class MvpManager : IView
    {
        #region Singleton
        readonly static MvpManager s_instance = new MvpManager();
        public static MvpManager Instance => s_instance;
        private MvpManager() { }
        #endregion

        public event EventHandler<FlightEventArgs> AddingFlightEventRaised;
        public event EventHandler<FlightEventArgs> DeletingFlightEventRaised;
        public event EventHandler<EditingFlightEventArgs> EditingFlightEventRaised;
        public event Func<FilteringFlightsEventArgs, IEnumerable<Flight>> FilteringFlightsEventRaised;

        public void OnAddingFlightEventRaised(object sender, FlightEventArgs e) =>
            AddingFlightEventRaised?.Invoke(sender, e);

        public void OnDeletingFlightEventRaised(object sender, FlightEventArgs e) =>
            DeletingFlightEventRaised?.Invoke(sender, e);

        public void OnEditingFlightEventRaised(object sender, EditingFlightEventArgs e) =>
            EditingFlightEventRaised?.Invoke(sender, e);

        public IEnumerable<Flight> OnFilteringFlightsEventRaised(FilteringFlightsEventArgs e) =>
            FilteringFlightsEventRaised?.Invoke(e);
    }
}
