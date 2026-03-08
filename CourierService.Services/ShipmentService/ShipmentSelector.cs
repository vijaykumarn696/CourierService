using CourierService.Domain.Models;

namespace CourierService.Services.ShipmentService
{
    public class ShipmentSelector
    {
        public List<Package> Select(List<Package> packages, int maxWeight)
        {
            List<Package> best = new();
            int bestWeight = 0;

            int n = packages.Count;

            for (int i = 0; i < (1 << n); i++)
            {
                List<Package> current = new();
                int weight = 0;

                for (int j = 0; j < n; j++)
                {
                    if ((i & (1 << j)) > 0)
                    {
                        weight += packages[j].Weight;
                        current.Add(packages[j]);
                    }
                }

                if (weight <= maxWeight)
                {
                    if (current.Count > best.Count || (current.Count == best.Count && weight > bestWeight))
                    {
                        best = current;
                        bestWeight = weight;
                    }
                }
            }

            return best;
        }
    }
}
