using RentalManager.Application.Queries.Request;
using RentalManager.Application.Queries.Response;

namespace RentalManager.Application.Interfaces.Queries;

public interface ICalculateRentValueByReturnDateQueryHandler
{
    Task<CalculateRentValueByReturnDateQueryResponse> Handle(CalculateRentValueByDateQueryRequest request);
}
