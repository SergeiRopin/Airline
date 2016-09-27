using AirportManager;
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
            EntityFactory entityFactory = new EntityFactory();
            IEntity entity = null;
            var createEntityManager = entityFactory.Create(type);
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
            EntityFactory entityFactory = new EntityFactory();
            IEntity entity = null;
            var createEntityManager = entityFactory.Create(type);
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
