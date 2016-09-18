using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.TemplateMethod
{
    class EditEntityHelper
    {
        public T EditEntity<T>(Func<T> entityHandler)
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
                        InputOutputHelper.PrintColorText("\nPlease make a choise. \"C\" / \"Enter\": ", ConsoleColor.Red);
                        break;
                }
            } while (key != "ENTER" & key != "ESCAPE");

            return output;
        }
    }
}
