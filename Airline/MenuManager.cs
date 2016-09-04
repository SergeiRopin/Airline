using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    class MenuManager
    {
        public MenuItemHandler MenuItemHandler { get; set; }
        public MenuHandler MenuHandler { get; set; }
        
        public void CallMenuItem()
        {
            if (MenuItemHandler != null)
                MenuItemHandler();
        }

        public void HandleExceptions()
        {
            try
            {
                if (MenuHandler != null)
                    MenuHandler();
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
