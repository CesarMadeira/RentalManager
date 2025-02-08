using RentalManager.Application.Interfaces.Queries;
using RentalManager.Application.Queries.Request;
using RentalManager.Application.Queries.Response;
using RentalManager.Domain.Interfaces.Respositories;

namespace RentalManager.Application.Queries.Handlers
{
    public class GetMotorcycleByIdQueryHandler : IGetMotorcycleByIdQueryHandler
    {
        private readonly IMotorcycleRepository _motorcycleRepository;

        public GetMotorcycleByIdQueryHandler(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task<GetMotorcycleByIdQueryResponse> Handle(GetMotorcycleByIdQueryRequest request)
        {
            var motorcycle = await _motorcycleRepository.Get(request.MotorcycleId);
            if (motorcycle == null)
                throw new ArgumentException("Moto não existe!");
            return new GetMotorcycleByIdQueryResponse
            {
                Id = motorcycle.Id,
                LicencePlate = motorcycle.LicencePlate,
                Model = motorcycle.Model,
                Year = motorcycle.Year
            };
        }
    }
}
