using LogistikbudeExercise.DTOs;

namespace LogistikbudeExercise.Services;

public interface ITransactionService
{
    List<LocationSummaryDto> GetTopLocations();
    List<LocationSummaryDto> GetUnconfirmedDestinations();
    List<LocationBalanceDto> GetBalance(DateTime referenceDate, string carrierType);
}
