using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    class Ticket
    {
        public SeatClass SeatClass { get; private set; }
        public decimal Price { get; private set; }

        public Ticket(SeatClass seatClass, decimal price)
        {
            SeatClass = seatClass;
            Price = price;
        }
    }
}
