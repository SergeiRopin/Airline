using Airport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.TemplateMethod
{
    class CreateEntityTemplate
    {
        public IEntity CreateEntity(Type type)
        {
            IEntity entity = null;
            CreateEntityManager<IEntity> createEntityManager = null;  

            if (type == typeof(Flight))
            {
                createEntityManager = new FlightCreateEntityManager(); 
            }
            else if (type == typeof(Passenger))
            {
                createEntityManager = new PassengerCreateEntityManager(); 
            }

            do
            {
                try
                {
                    entity = createEntityManager.Create();
                }
                catch (EntityCreationCanceledException e)
                {
                    Console.WriteLine($"\n{e.Message}");
                    break;
                }
                catch (InvalidEntityInputException e)
                {
                    Console.WriteLine($"\n{e.Message}");
                    continue;
                }
            } while (entity == null);

            return entity;
        }

        public IEntity EditEntity(Type type, IEntity actualEntity)
        {
            IEntity entity = null;
            CreateEntityManager<IEntity> createEntityManager = null;

            if (type == typeof(Flight))
            {
                createEntityManager = new FlightCreateEntityManager();
            }
            else if (type == typeof(Passenger))
            {
                createEntityManager = new PassengerCreateEntityManager();
            }

            do
            {
                try
                {
                    entity = createEntityManager.Edit(actualEntity);
                }
                catch (EntityCreationCanceledException e)
                {
                    Console.WriteLine($"\n{e.Message}");
                    break;
                }
                catch (InvalidEntityInputException e)
                {
                    Console.WriteLine($"\n{e.Message}");
                    continue;
                }
            } while (entity == null);

            return entity;
        }
    }
}
