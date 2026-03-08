using CourierService.Domain.Models;
using CourierService.Services.CostService;

namespace CourierService.Tests.TestCases
{
    public class CostServiceTests
    {
        [Fact]
        public void ShouldApplyOffer003()
        {
            var service = new CostService();

            var pkg = new Package
            {
                Weight = 10,
                Distance = 100,
                OfferCode = "OFR003"
            };

            service.Calculate(pkg, 100);

            Assert.Equal(35, pkg.Discount);
            Assert.Equal(665, pkg.TotalCost);
        }
    }
}
