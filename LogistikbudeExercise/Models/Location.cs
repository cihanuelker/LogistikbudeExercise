using LogistikbudeExercise.DTOs;

namespace LogistikbudeExercise.Models;

public class Location
{
    public int FromLocationId { get; set; }
    public string FromLocationName { get; set; }
    public List<TransactionDto> TransactionDtos { get; set; } = []; 
}
