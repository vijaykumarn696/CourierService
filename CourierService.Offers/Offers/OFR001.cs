using CourierService.Domain.Models;
using CourierService.Offers.Interfaces;

namespace CourierService.Offers.Offers
{
    public class OFR001 : IOfferStrategy
    {
        public bool IsApplicable(Package p)
        {
            return p.Distance < 200
                && p.Weight >= 70 && p.Weight <= 200;
        }

        public decimal CalculateDiscount(decimal cost)
        {
            return (cost * 10) / 100;
        }
    }
}
