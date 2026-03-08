# Courier Service

A delivery management system that calculates shipping costs, applies discounts based on specific offers, and optimizes delivery routes for multiple vehicles. The system processes packages, computes delivery costs, schedules deliveries, and provides estimated delivery times.

## Key Features

- **Cost Calculation**: Compute delivery costs based on base cost, weight, and distance
- **Dynamic Offers System**: Apply promotional discounts with configurable eligibility criteria
- **Vehicle Routing**: Optimize package assignments across multiple vehicles
- **Delivery Scheduling**: Calculate estimated delivery times based on vehicle availability and speed
- **Shipment Optimization**: Select optimal package combinations for each shipment using bin packing algorithm

## Technology Stack

- **Language**: C# 14.0
- **.NET Version**: .NET 10
- **Architecture**: Service-oriented with Factory pattern for offers
- **Testing**: xUnit framework

## Project Structure

```
CourierService/
├── CourierService.Domain/
│   └── Models/
│       ├── Package.cs              # Package entity with delivery info
│       │   - Id: Package identifier
│       │   - Weight: Package weight in kg
│       │   - Distance: Delivery distance in km
│       │   - OfferCode: Promotional offer code
│       │   - DeliveryCost: Calculated delivery cost
│       │   - Discount: Applied discount amount
│       │   - TotalCost: Final cost after discount
│       │   - EstimatedDeliveryTime: Scheduled delivery time
│       │
│       └── Vehicle.cs              # Vehicle entity with capacity constraints
│           - Id: Vehicle identifier
│           - MaxSpeed: Maximum speed in km/h
│           - MaxWeight: Maximum carrying capacity in kg
│           - AvailableAt: Time when vehicle becomes available
│
├── CourierService.Services/
│   ├── CostService/
│   │   └── CostService.cs         # Calculates delivery costs & applies discounts
│   │
│   ├── DeliveryService/
│   │   └── DeliveryService.cs     # Manages delivery scheduling & optimization
│   │       - Calculate(): Main orchestration method
│   │       - Assigns packages to vehicles
│   │       - Computes estimated delivery times
│   │
│   └── ShipmentService/
│       └── ShipmentSelector.cs    # Selects optimal package combinations
│           - Select(): Uses bit manipulation for subset enumeration
│           - Maximizes package count while respecting weight limits
│
├── CourierService.Offers/
│   ├── Offers/
│   │   ├── OFR001.cs              # 10% discount offer
│   │   ├── OFR002.cs              # 7% discount offer
│   │   └── OFR003.cs              # 5% discount offer
│   │
│   ├── Interfaces/
│   │   └── IOfferStrategy.cs       # Offer strategy interface
│   │
│   └── Factories/
│       └── OfferFactory.cs         # Factory pattern for offer creation
│
├── CourierService.Console/
│   └── Program.cs                  # CLI interface for user input/output
│
└── CourierService.Tests/
    └── TestCases/
        └── CostServiceTests.cs     # Unit tests for cost calculation
```
### Available Offers

| Offer Code | Discount | Weight Range | Distance Range | Conditions |
|-----------|----------|--------------|-----------------|-----------|
| OFR001    | 10%      | 70-200 kg    | < 200 km        | Best discount for heavy short-distance packages |
| OFR002    | 7%       | 100-250 kg   | 50-150 km       | For heavy packages in mid-range distances |
| OFR003    | 5%       | 10-150 kg    | 50-250 km       | Widest distance range, lighter packages |

**Offer Selection:**
- Offers are applied if both weight AND distance conditions are met
- If no offer is applicable, discount = 0

### Delivery Scheduling Algorithm

1. **Vehicle Selection**: Choose the vehicle with the earliest availability time (ties broken by vehicle ID)
2. **Shipment Selection**: Use bin packing algorithm to select optimal package subset
   - Maximize number of packages
   - Respects maximum vehicle weight capacity
3. **Time Calculation**:
   - Delivery Time per Package = Distance / Speed
   - Estimated Delivery = Vehicle Available Time + Delivery Time
4. **Vehicle Update**:
   - Vehicle AvailableAt = Previous AvailableAt + (Max Delivery Time × 2)
   - Factor of 2 accounts for return journey

### Shipment Optimization

The `ShipmentSelector` uses a brute-force approach with bit manipulation:
- Generates all possible subsets (2^n combinations)
- Filters subsets that fit within weight limit
- Selects the subset with:
  - Maximum package count (priority)
  - Maximum weight utilization (tie-breaker)

## Running the Application

### Prerequisites
- .NET 10 SDK or later
- Visual Studio, Visual Studio Code, or any .NET-compatible IDE

### Build and Run

```bash
# Navigate to project directory
cd CourierService

# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run the console application
dotnet run --project CourierService.Console

# Or run with input file
dotnet run --project CourierService.Console < input.txt
```


### Test Coverage

- **CostServiceTests**: Validates offer calculation and cost computation
- Cost calculations with various weight/distance combinations
- Discount application logic
- Edge cases for offer eligibility

## Algorithm Complexity

| Operation | Time Complexity | Space Complexity |
|-----------|-----------------|------------------|
| Cost Calculation | O(1) per package | O(1) |
| Offer Application | O(1) per package | O(1) |
| Shipment Selection | O(2^n) where n = remaining packages | O(2^n) |
| Delivery Scheduling | O(m × 2^n) where m = vehicles | O(m + 2^n) |

## Design Patterns Used

1. **Factory Pattern**: `OfferFactory` for creating offer instances
2. **Strategy Pattern**: `IOfferStrategy` for different discount calculations
3. **Service Layer Pattern**: Separation of concerns (Cost, Delivery, Shipment services)
4. **Dependency Injection**: Manual DI in service constructors

## Future Enhancements

- [ ] Database integration for package and vehicle persistence
- [ ] RESTful API for remote access
- [ ] Advanced optimization algorithms (Genetic Algorithm, Simulated Annealing)
- [ ] Real-time tracking and status updates
- [ ] Multiple offer combinations
- [ ] Dynamic pricing based on demand
- [ ] Async/await for concurrent operations
