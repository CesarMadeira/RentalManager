using RentalManager.Application.Commands.Requests;
using RentalManager.Application.Interfaces.Commands;
using RentalManager.Domain.Exceptions;
using RentalManager.Domain.Interfaces.Respositories;

namespace RentalManager.Application.Commands.Handlers
{
    public class RemoveMotorcycleCommandHandler : IRemoveMotorcycleCommandHandler
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IRentRepository _rentRepository;

        public RemoveMotorcycleCommandHandler(
            IMotorcycleRepository motorcycleRepository,
            IRentRepository rentRepository
        ) {
            _motorcycleRepository = motorcycleRepository;
            _rentRepository = rentRepository;
        }

        public async Task Handle(RemoveMotorcycleCommandRequest request)
        {
            var motorcycle = await _motorcycleRepository.Get(request.MotorcycleId);
            if (motorcycle == null)
                throw new BusinessException("Moto não existe!");

            if (await _rentRepository.HasRentals(request.MotorcycleId))
                throw new BusinessException("Não foi possivel remover, moto ja foi alugada!");

            await _motorcycleRepository.Delete(request.MotorcycleId);
        }
    }
}
