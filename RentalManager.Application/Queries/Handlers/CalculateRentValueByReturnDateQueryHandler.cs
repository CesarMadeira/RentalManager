using RentalManager.Application.Interfaces.Queries;
using RentalManager.Application.Queries.Request;
using RentalManager.Application.Queries.Response;
using RentalManager.Domain.Interfaces.Respositories;

namespace RentalManager.Application.Queries.Handlers;

public class CalculateRentValueByReturnDateQueryHandler : ICalculateRentValueByReturnDateQueryHandler
{
    private readonly IRentRepository _rentRepository;

    public CalculateRentValueByReturnDateQueryHandler(IRentRepository rentRepository)
    {
        _rentRepository = rentRepository;
    }

    public async Task<CalculateRentValueByReturnDateQueryResponse> Handle(CalculateRentValueByDateQueryRequest request)
    {
        // TODO refatorar calculo do valor total

        var rent = await _rentRepository.Get(request.RentId);

        if (request.EndDate == rent.EndForecast)
            return new CalculateRentValueByReturnDateQueryResponse { RentalValue = rent.CalculateTotalValue() };

        decimal total = 0;
        if (request.EndDate > rent.EndForecast)
        {
            total = ((request.EndDate - rent.EndForecast).Days * 50) + rent.CalculateTotalValue();
        }
        else
        {
            if (rent.Plan == 7)
                total = ((decimal)((rent.EndForecast - request.EndDate).Days * 30 * 0.2)) + rent.CalculateTotalValue(request.EndDate);
            if (rent.Plan == 15)
                total = ((decimal)((rent.EndForecast - request.EndDate).Days * 28 * 0.4)) + rent.CalculateTotalValue(request.EndDate);
        }
        return new CalculateRentValueByReturnDateQueryResponse { RentalValue = total };
    }

    //private decimal CalculateRentValue()
}
