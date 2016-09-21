using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.SetDateStrategy
{
    class SetDateHelper
    {
        IDate _date;

        public SetDateHelper(IDate date)
        {
            _date = date;
        }

        public DateTime CreateDate()
        {
            return _date.CreateDate();
        }
    }
}
