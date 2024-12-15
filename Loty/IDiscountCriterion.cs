using System;

namespace Loty
{
    // Discount criteria interface
    public interface IDiscountCriterion
    {
        bool IsApplicable(Flight flight, DateTime purchaseDate, Person p);
    }
    public class Konczy_na_a_Criterion : IDiscountCriterion
    {
        public bool IsApplicable(Flight flight, DateTime purchaseDate, Person p)
        {
            return flight.To.EndsWith("a");
        }
    }
    public class bilet_na_dziecko_Criterion : IDiscountCriterion
    {
        public bool IsApplicable(Flight flight, DateTime purchaseDate, Person p)
        {
            int age = purchaseDate.Year - p.BirthDate.Year;
            return age < 18;
        }
    }
    public class bilet_na_sobota : IDiscountCriterion
    {
        public bool IsApplicable(Flight flight, DateTime purchaseDate, Person p)
        {
            return purchaseDate.DayOfWeek == DayOfWeek.Saturday;
        }
    }
    public class BirthdayDiscountCriterion : IDiscountCriterion
    {
        public bool IsApplicable(Flight flight, DateTime purchaseDate, Person p)
        {
            return purchaseDate.Date == p.BirthDate;
        }
    }

    public class GenderDiscountCriterion : IDiscountCriterion
    {
        public GenderDiscountCriterion(Gender gender)
        {
            this.gender = gender;
        }

        public Gender gender { get; set; }

        public bool IsApplicable(Flight flight, DateTime purchaseDate, Person p)
        {
            return p.Gender == gender;
        }
    }
}
