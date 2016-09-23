using Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresenterStorage
{
    class Presenter
    {
        IView _view;

        public Presenter(IView view)
        {
            _view = view;
        }

        private void Initialize()
        {
            _view.ViewAllFlightsEventRaised += ViewAllFlightsEventHandler;
        }

        private void ViewAllFlightsEventHandler(object sender, EventArgs e)
        {
            ;
        }
    }
}
