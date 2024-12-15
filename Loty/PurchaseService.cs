using System;
using System.Collections.Generic;

namespace Loty
{

    // Purchase service
    public class PurchaseService
    {
        private readonly List<IDiscountCriterion> _discountCriteria;
        public int Discount { get; set; }

        public PurchaseService(List<IDiscountCriterion> discountCriteria, int discount)
        {
            _discountCriteria = discountCriteria;
            Discount = discount;
        }

        public decimal PurchaseFlight(Tenant tenant, Flight flight, DateTime purchaseDate, Person p, out List<string> appliedCriteria)
        {
            appliedCriteria = new List<string>();

            decimal initialPrice = flight.GetPrice(purchaseDate);
            decimal finalPrice = initialPrice;

            foreach (var criterion in _discountCriteria)
            {
                if (criterion.IsApplicable(flight, purchaseDate, p))
                {
                    if (finalPrice - Discount >= flight.MinPrice)
                    {
                        finalPrice -= Discount;
                        if (tenant.CanLogDiscountCriteria())
                        {
                            appliedCriteria.Add(criterion.GetType().Name);
                        }
                    }
                }
            }

            return finalPrice;
        }
    }

}
