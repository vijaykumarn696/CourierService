using CourierService.Domain.Models;
using CourierService.Offers.Interfaces;

namespace CourierService.Offers.Offers
{
    public class OFR002 : IOfferStrategy
    {
        public bool IsApplicable(Package p)
        {
            return p.Distance >= 50 && p.Distance <= 150
                && p.Weight >= 100 && p.Weight <= 250;
        }

        public decimal CalculateDiscount(decimal cost)
        {
            return (cost * 7) / 100; ;
        }
    }
}
