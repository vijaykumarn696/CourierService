using CourierService.Domain;
using CourierService.Domain.Models;
using CourierService.Services;
using CourierService.Services.CostService;
using CourierService.Services.DeliveryService;

var firstLine = Console.ReadLine().Split();

int baseCost = int.Parse(firstLine[0]);
int count = int.Parse(firstLine[1]);

List<Package> packages = new();

for (int i = 0; i < count; i++)
{
    var line = Console.ReadLine().Split();

    packages.Add(new Package
    {
        Id = line[0],
        Weight = int.Parse(line[1]),
        Distance = int.Parse(line[2]),
        OfferCode = line[3]
    });
}

CostService costService = new();

foreach (var p in packages)
{
    costService.Calculate(p, baseCost);
}

string vehicleLine = Console.ReadLine();

if (string.IsNullOrEmpty(vehicleLine))
{
    foreach (var p in packages)
        Console.WriteLine($"{p.Id} {p.Discount} {p.TotalCost}");
}
else
{
    var vehicleInput = vehicleLine.Split();

    int vehicles = int.Parse(vehicleInput[0]);
    int speed = int.Parse(vehicleInput[1]);
    int maxWeight = int.Parse(vehicleInput[2]);

    DeliveryService deliveryService = new();

    deliveryService.Calculate(packages, vehicles, speed, maxWeight);

    foreach (var p in packages)
    {
        Console.WriteLine($"{p.Id} {(int)p.Discount} {(int)p.TotalCost} {p.EstimatedDeliveryTime:F2}");
    }
}