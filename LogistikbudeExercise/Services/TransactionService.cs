using LogistikbudeExercise.DTOs;
using LogistikbudeExercise.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LogistikbudeExercise.Services;

public class TransactionService : ITransactionService
{
    private readonly List<Location> _locations = LoadLocations("E:\\logistikbude_exercise.json");

    public List<LocationSummaryDto> GetTopLocations()
    {
        return _locations
            .SelectMany(x => x.TransactionDtos.Select(t => new { x.FromLocationId, x.FromLocationName }))
            .GroupBy(x => new { x.FromLocationId, x.FromLocationName })
            .OrderByDescending(x => x.Count())
            .Take(3)
            .Select(x => new LocationSummaryDto { LocationId = x.Key.FromLocationId, LocationName = x.Key.FromLocationName, Count = x.Count() })
            .ToList();
    }

    public List<LocationSummaryDto> GetUnconfirmedDestinations()
    {
        return _locations
            .SelectMany(x => x.TransactionDtos)
            .Where(x => x.AcceptedDate == null)
            .GroupBy(x => new { x.DestinationLocationId, x.DestinationLocationName })
            .Select(x => new LocationSummaryDto { LocationId = x.Key.DestinationLocationId, LocationName = x.Key.DestinationLocationName, Count = x.Count() })
            .ToList();
    }

    public List<LocationBalanceDto> GetBalance(DateTime referenceDate, string carrierType)
    {
        return _locations
            .SelectMany(x => x.TransactionDtos.Select(t => new { x.FromLocationId, x.FromLocationName, t.DestinationLocationId, t.DestinationLocationName, t.Date, t.LoadCarriers }))
            .Where(x => x.Date <= referenceDate && x.LoadCarriers.Contains(carrierType))
            .GroupBy(x => new { x.DestinationLocationId, x.DestinationLocationName })
            .Select(x => new LocationBalanceDto
            {
                LocationId = x.Key.DestinationLocationId,
                LocationName = x.Key.DestinationLocationName,
                Balance = x.Sum(t => GetCarrierQuantity(t.LoadCarriers, carrierType))
            })
            .ToList();
    }

    private static int GetCarrierQuantity(string loadCarriers, string carrierType)
    {
        var carriers = loadCarriers.Split(',').Select(x => x.Trim());
        foreach (var carrier in carriers)
        {
            var parts = carrier.Split('x');
            if (parts[1] == carrierType)
            {
                return int.Parse(parts[0]);
            }
        }
        return 0;
    }

    public static List<Location> LoadLocations(string path)
    {
        var data = File.ReadAllText(path);

        var format = "dd/MM/yyyy";
        var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format };

        var result = JsonConvert.DeserializeObject<List<Location>>(data, dateTimeConverter);

        return result ?? [];
    }
}