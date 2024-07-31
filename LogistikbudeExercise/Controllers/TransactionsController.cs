using LogistikbudeExercise.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogistikbudeExercise.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionsController(
    ITransactionService transactionService
) : ControllerBase
{
    private readonly ITransactionService _transactionService = transactionService;

    [HttpGet("top-locations")]
    public IActionResult GetTopLocations()
    {
        var topLocations = _transactionService.GetTopLocations();
        return Ok(topLocations);
    }

    [HttpGet("unconfirmed-destinations")]
    public IActionResult GetUnconfirmedDestinations()
    {
        var unconfirmed = _transactionService.GetUnconfirmedDestinations();
        return Ok(unconfirmed);
    }

    [HttpGet("balance")]
    public IActionResult GetBalance()
    {
        DateTime referenceDate = new(2024, 05, 01);
        string carrierType = "EPAL";
        var balance = _transactionService.GetBalance(referenceDate, carrierType);
        return Ok(balance);
    }
}
