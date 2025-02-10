using RentalManager.Application.Commands.Requests;

namespace RentalManager.Application.Interfaces.Commands;

public interface ISendPhotoOfDocumentCommandHandler
{
    Task Handle(SendPhotoOfDocumentCommandRequest request);
}
