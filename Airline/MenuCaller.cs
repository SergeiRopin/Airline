using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    class MenuCaller
    {
        public MenuHandler MenuHandler { get; set; }
        public ExceptionsHandler ExceptionsHandler { get; set; }
        
        public void CallMenuItem()
        {
            if (MenuHandler != null)
                MenuHandler();
        }

        public void HandleExceptions()
        {
            try
            {
                if (ExceptionsHandler != null)
                    ExceptionsHandler();
            }
            catch (KeyNotFoundException)
            {
                AuxiliaryMethods.PrintColorText("\nUnexpected value has been entered, please select the value from menu list!", ConsoleColor.Red);
            }
            catch (Exception ex)
            {
                AuxiliaryMethods.PrintColorText("\nWrong value has been entered! " + ex.Message, ConsoleColor.Red);
            }
        }
    }
}
