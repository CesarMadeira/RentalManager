using RentalManager.Domain.Entities;

namespace RentalManager.Domain.Interfaces.Respositories;

public interface IDeliveryPersonRepository
{
    Task Create(DeliveryPerson deliveryPerson);
    Task<DeliveryPerson> Get(string id);
    Task Delete(string deliveryPersonId);
}
