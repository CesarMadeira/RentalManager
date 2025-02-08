using RentalManager.Application.Commands.Requests;
using RentalManager.Application.Commands.Responses;

namespace RentalManager.Application.Interfaces.Commands;

public interface IRentMotorcycleCommandHandler
{
    Task<RentMotorcycleCommandResponse> Handle(RentMotorcycleCommandRequest request);
}
