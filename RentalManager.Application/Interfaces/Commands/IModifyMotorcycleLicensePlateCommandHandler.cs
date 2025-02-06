using RentalManager.Application.Commands.Requests;

namespace RentalManager.Application.Interfaces.Commands;

public interface IModifyMotorcycleLicensePlateCommandHandler
{
    Task Handle(ModifyMotorcycleLicensePlateCommandRequest request);
}
