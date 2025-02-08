using RentalManager.Application.Interfaces.Queries;
using RentalManager.Application.Queries.Request;
using RentalManager.Application.Queries.Response;
using RentalManager.Domain.Exceptions;
using RentalManager.Domain.Interfaces.Respositories;

namespace RentalManager.Application.Queries.Handlers;

public class GetRentByIdQueryHandler : IGetRentByIdQueryHandler
{
    private readonly IRentRepository _rentRepository;

    public GetRentByIdQueryHandler(IRentRepository rentRepository)
    {
        _rentRepository = rentRepository;
    }

    public async Task<GetRentByIdQueryResponse> Handle(GetRentByIdQueryRequest request)
    {
        var rent = await _rentRepository.Get(request.RentId);
        if (rent == null)
            throw new BusinessException("Aluguel não existe!");

        return new GetRentByIdQueryResponse
        {
            Id = rent.Id,
            DeliveryPersonId = rent.DeliveryPersonId,
            MotorcycleId = rent.MotorcycleId,
            Start = rent.Start,
            Finish = rent.Finish,
            EndForecast = rent.EndForecast,
            DailyValue = rent.PlanValue()
        };
    }
}
