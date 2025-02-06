using RentalManager.Application.Commands.Requests;

namespace RentalManager.Application.Interfaces.Commands;

public interface IRegisterNewMotorcycleCommandHandler
{
    Task Handle(RegisterNewMotorcycleCommandRequest request);
}
