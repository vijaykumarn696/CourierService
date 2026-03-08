using CourierService.Domain.Models;
using CourierService.Offers.Interfaces;

namespace CourierService.Offers.Offers
{
    public class OFR003 : IOfferStrategy
    {
        public bool IsApplicable(Package p)
        {
            return p.Distance >= 50 && p.Distance <= 250
                && p.Weight >= 10 && p.Weight <= 150;
        }

        public decimal CalculateDiscount(decimal cost)
        {
            return (cost * 5) / 100;
        }
    }
}
