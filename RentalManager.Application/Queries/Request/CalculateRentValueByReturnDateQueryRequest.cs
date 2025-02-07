namespace RentalManager.Application.Queries.Request;

public class CalculateRentValueByDateQueryRequest
{
    public string RentId { get; set; }
    public DateTime EndDate { get; set; }
}
