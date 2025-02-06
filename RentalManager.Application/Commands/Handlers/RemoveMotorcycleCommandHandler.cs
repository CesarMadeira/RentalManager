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
            var motorcycle = await _motorcycleRepository.Get(request.MotorcycleId);
            if (motorcycle == null)
                throw new Exception("Moto não existe!");
            await _motorcycleRepository.Delete(request.MotorcycleId);
        }
    }
}
