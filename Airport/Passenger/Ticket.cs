using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportManager
{
    public class Ticket
    {
        public SeatClass SeatClass { get; private set; }
        public decimal Price { get; private set; }

        public Ticket(SeatClass seatClass, decimal price)
        {
            SeatClass = seatClass;
            Price = price;
        }

        public override string ToString()
        {
            return $"Ticket price: {Price.ToString("C", CultureInfo.GetCultureInfo("en-US"))}, Seat: {SeatClass}";
        }
    }
}
