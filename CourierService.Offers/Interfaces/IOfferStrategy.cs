using CourierService.Domain.Models;

namespace CourierService.Offers.Interfaces
{
    public interface IOfferStrategy
    {
        bool IsApplicable(Package package);
        decimal CalculateDiscount(decimal deliveryCost);
    }
}
