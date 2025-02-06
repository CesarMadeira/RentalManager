using RentalManager.Application.Commands.Requests;

namespace RentalManager.Application.Interfaces.Commands;

public interface IRegisterDeliveryPersonCommandHandler
{
    Task Handle(RegisterDeliveryPersonCommandRequest request);
}
