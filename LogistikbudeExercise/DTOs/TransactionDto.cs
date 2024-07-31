namespace LogistikbudeExercise.DTOs;

public class TransactionDto
{
    public int DestinationLocationId { get; set; }
    public string DestinationLocationName { get; set; }
    public DateTime Date { get; set; }
    public DateTime? AcceptedDate { get; set; }
    public string LoadCarriers { get; set; }
}
