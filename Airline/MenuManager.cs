using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    class MenuManager
    {
        public void CallMenuItem(Action action)
        {
            if (action != null)
                action();
        }

        public void CatchMenuExceptions(Action action)
        {
            try
            {
                if (action != null)
                    action();
            }
            catch (KeyNotFoundException)
            {
                InputOutputHelper.PrintColorText("\nUnexpected value has been entered, please select the value from menu list!", ConsoleColor.Red);
            }
            catch (Exception ex)
            {
                InputOutputHelper.PrintColorText("\nWrong value has been entered! " + ex.Message, ConsoleColor.Red);
            }
        }
    }
}
