using CourierService.Domain.Models;
using CourierService.Offers.Factories;

namespace CourierService.Services.CostService
{
    public class CostService
    {
        private readonly OfferFactory _offerFactory;

        public CostService()
        {
            _offerFactory = new OfferFactory();
        }
        public void Calculate(Package package, int baseCost)
        {
            decimal deliveryCost =
                baseCost +
                (package.Weight * 10) +
                (package.Distance * 5);

            package.DeliveryCost = deliveryCost;

            var offer = _offerFactory.GetOffer(package.OfferCode);

            if (offer != null && offer.IsApplicable(package))
            {
                package.Discount = offer.CalculateDiscount(deliveryCost);
            }
            else
            {
                package.Discount = 0;
            }

            package.TotalCost = deliveryCost - package.Discount;
        }
    }
}
