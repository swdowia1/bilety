using System;

namespace Loty
{
    public class FlightPrice
    {
        public DateTime Date { get; private set; }
        public decimal Price { get; private set; }

        public FlightPrice(DateTime date, decimal price)
        {
            Date = date;
            Price = price;
        }
    }
}
