using AirportManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.TemplateMethod
{
    class CreateEditEntityHelper
    {
        /// <summary>
        /// Asks to press "Enter" if a property need to be changed or "Esc" if not
        /// </summary>
        /// <typeparam name="T">type of property</typeparam>
        /// <param name="entityHandler">method that allows to input property</param>
        /// <returns>updated property or default value of property</returns>
        public T AskEditEntity<T>(Func<T> entityHandler)
        {
            T output = default(T);
            string key;
            do
            {
                Console.Write("To change actual value press \"Enter\" button, to keep the default value press \"Esc\": ");
                key = Console.ReadKey().Key.ToString().ToUpper();
                switch (key)
                {
                    case "ENTER":
                        output = entityHandler.Invoke();
                        InputOutputHelper.PrintColorText($"Information has been updated!", ConsoleColor.DarkCyan);
                        break;
                    case "ESCAPE":
                        InputOutputHelper.PrintColorText("\nDefault information has been kept!", ConsoleColor.DarkCyan);
                        break;
                    default:
                        InputOutputHelper.PrintColorText("\nPlease make a choise. \"Enter\" / \"Esc\": ", ConsoleColor.Red);
                        break;
                }
            } while (key != "ENTER" & key != "ESCAPE");

            return output;
        }

        /// <summary>
        /// Checks if the updated property was changed. If no - keeps actual value of property
        /// </summary>
        /// <typeparam name="T">type of property</typeparam>
        /// <param name="actual">actual (before updating) property</param>
        /// <param name="updated">updated property</param>
        /// <returns>updated property or actual property</returns>
        public T UpdateEntityOrKeepDefault<T>(T actual, T updated)
        {
            T value = (T)Convert.ChangeType(updated, typeof(T));

            if (updated == null)
                updated = actual;
            else if (updated.GetType().IsValueType && value.ToString().Equals("0"))
                updated = actual;
            else if (updated.GetType().IsValueType && value.Equals(default(DateTime)))
                updated = actual;
            return updated;
        }

        /// <summary>
        /// Creates entities using template method
        /// </summary>
        /// <returns>new entity (Flight / Passenger)</returns>
        public T CreateEntity<T>()
        {
            CreateEntityTemplate createEntityTemplate = new CreateEntityTemplate();
            IEntity entity = createEntityTemplate.CreateEntity(typeof(T));
            return (T)entity;
        }

        /// <summary>
        /// Edits entities using template method
        /// </summary>
        /// <param name="actualEntity">entity to edit</param>
        /// <returns>updated entity (Flight / Passenger)</returns>
        public T EditEntity<T>(IEntity actualEntity)
        {
            CreateEntityTemplate createEntityTemplate = new CreateEntityTemplate();
            IEntity entity = createEntityTemplate.EditEntity(typeof(T), actualEntity);
            return (T)entity;
        }
    }
}
