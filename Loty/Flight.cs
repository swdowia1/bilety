using System;
using System.Collections.Generic;
using System.Linq;

namespace Loty
{
    public class Flight
    {
        public string FlightId { get; private set; }

        public decimal MinPrice { get; set; }
        public string From { get; private set; }
        public string To { get; private set; }
        public DateTime DepartureTime { get; private set; }
        public List<DayOfWeek> DaysOfOperation { get; private set; }
        public List<FlightPrice> Prices { get; private set; }
        public decimal GetPrice(DateTime date)
        {
            var price = Prices.LastOrDefault(p => p.Date.Date <= date.Date)?.Price;
            return price ?? throw new InvalidOperationException("Price not set for the given date.");
        }
        public Flight(string flightId, string from, string to, DateTime departureTime, List<DayOfWeek> daysOfOperation, decimal minPrice)
        {
            if (!IsValidFlightId(flightId))
                throw new ArgumentException("Invalid flight ID format.");

            FlightId = flightId;
            From = from;
            To = to;
            DepartureTime = departureTime;
            DaysOfOperation = daysOfOperation;
            Prices = new List<FlightPrice>();
            MinPrice = minPrice;
        }

        private bool IsValidFlightId(string flightId)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(flightId, @"^[A-Z]{3} \d{5} [A-Z]{3}$");
        }

        public void AddPrice(DateTime date, decimal price)
        {
            Prices.Add(new FlightPrice(date, price));
        }


    }
}
