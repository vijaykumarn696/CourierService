using CourierService.Offers.Interfaces;
using CourierService.Offers.Offers;

namespace CourierService.Offers.Factories
{
    public class OfferFactory
    {
        private readonly Dictionary<string, IOfferStrategy> _offers;

        public OfferFactory()
        {
            _offers = new Dictionary<string, IOfferStrategy>()
                        {
                            { "OFR001", new OFR001() },
                            { "OFR002", new OFR002() },
                            { "OFR003", new OFR003() }
                        };
        }

        public IOfferStrategy? GetOffer(string code)
        {
            return _offers.TryGetValue(code, out var offer) ? offer : null;
        }
    }
}
