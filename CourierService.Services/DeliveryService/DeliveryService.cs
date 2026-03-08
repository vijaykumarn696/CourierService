using CourierService.Domain.Models;
using CourierService.Services.ShipmentService;

namespace CourierService.Services.DeliveryService
{
    public class DeliveryService
    {
        public void Calculate(
        List<Package> packages,
        int vehicleCount,
        int speed,
        int maxWeight)
        {
            List<Vechicle> vehicles = new();

            for (int i = 0; i < vehicleCount; i++)
            {
                vehicles.Add(new Vechicle
                {
                    Id = i+1,
                    MaxSpeed = speed,
                    MaxWeight = maxWeight,
                    AvailableAt = 0
                });
            }

            ShipmentSelector selector = new();

            List<Package> remaining = new(packages);

            while (remaining.Any())
            {
                var vehicle = vehicles.OrderBy(v => v.AvailableAt).ThenBy(v => v.Id).First();

                var shipment = selector.Select(remaining, maxWeight);

                double maxTime = 0;

                foreach (var pkg in shipment)
                {
                    double time = (double)pkg.Distance / speed;

                    pkg.EstimatedDeliveryTime =
                        Math.Round(vehicle.AvailableAt + time, 2);

                    maxTime = Math.Max(maxTime, time);
                }

                vehicle.AvailableAt += maxTime * 2;

                foreach (var pkg in shipment)
                    remaining.Remove(pkg);
            }
        }
    }
}
