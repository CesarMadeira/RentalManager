using RentalManager.Domain.Entities;

namespace RentalManager.Domain.Interfaces.Respositories;

public interface IRentRepository
{
    Task Create(Rent rent);
    Task<Rent> Get(string id);
    Task Delete(string id);
}
