using RentalManager.Application.Interfaces.Queries;
using RentalManager.Application.Queries.Request;
using RentalManager.Application.Queries.Response;
using RentalManager.Domain.Exceptions;
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
        var rent = await _rentRepository.Get(request.RentId);
        if (rent == null)
            throw new BusinessException("Locação não encontrada!");

        return new CalculateRentValueByReturnDateQueryResponse { RentalValue = rent.CalculateRentalValueForecast(request.EndDate) };

    }
}
