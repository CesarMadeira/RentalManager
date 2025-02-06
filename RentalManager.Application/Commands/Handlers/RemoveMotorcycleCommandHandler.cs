using RentalManager.Application.Commands.Requests;
using RentalManager.Application.Interfaces.Commands;
using RentalManager.Domain.Interfaces.Respositories;

namespace RentalManager.Application.Commands.Handlers
{
    public class RemoveMotorcycleCommandHandler : IRemoveMotorcycleCommandHandler
    {
        private readonly IMotorcycleRepository _motorcycleRepository;

        public RemoveMotorcycleCommandHandler(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task Handle(RemoveMotorcycleCommandRequest request)
        {
            await _motorcycleRepository.Delete(request.MotorcycleId);
        }
    }
}
