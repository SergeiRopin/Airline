using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.CreateDateStrategy
{
    class CreateDateHelper
    {
        IDate _date;

        public CreateDateHelper(IDate date)
        {
            _date = date;
        }

        public DateTime CreateDate()
        {
            return _date.CreateDate();
        }
    }
}
