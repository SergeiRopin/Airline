using AirportManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.TemplateMethod
{
    class EntityFactory
    {
        public CreateEntityManager<IEntity> Create(Type type)
        {
            CreateEntityManager<IEntity> createEntityManager = null;

            if (type == typeof(Flight))
            {
                createEntityManager = new FlightCreateEntityManager();
            }
            else if (type == typeof(Passenger))
            {
                createEntityManager = new PassengerCreateEntityManager();
            }
            return createEntityManager;
        }
    }
}
