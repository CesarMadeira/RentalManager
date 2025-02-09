using RentalManager.Domain.Entities;
using RentalManager.Domain.ValueObject;

namespace RentalManager.Domain.Interfaces.Respositories;

public interface IDeliveryPersonRepository
{
    Task Create(DeliveryPerson deliveryPerson);
    Task<DeliveryPerson> Get(string id);
    Task<DeliveryPerson> GetByCNPJ(CNPJ cnpj);
    Task<DeliveryPerson> GetByCNH(CNH cnh);
    Task Delete(string id);
}
