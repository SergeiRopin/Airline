using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    class MenuCaller
    {
        public MenuHandler MenuHandler {get;set;}

        //public void MenuLogic()
        //{
        //    InvokeMainMenu();
        //}

        public void CallMenuItem()
        {
            if (MenuHandler != null)
                MenuHandler();
        }
    }
}
