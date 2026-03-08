namespace CourierService.Domain.Models
{
    public class Package
    {
        public string Id { get; set; }
        public int Weight { get; set; }
        public int Distance { get; set; }
        public string OfferCode { get; set; }
        public decimal DeliveryCost { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalCost { get; set; }
        public double EstimatedDeliveryTime { get; set; }
    }
}
