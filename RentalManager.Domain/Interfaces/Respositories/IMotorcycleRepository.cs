using RentalManager.Domain.Entities;

namespace RentalManager.Domain.Interfaces.Respositories
{
    public interface IMotorcycleRepository
    {
        Task Create(Motorcycle motorcycle);
        Task Save(Motorcycle motorcycle);
        Task<Motorcycle> Get(string id);
        Task<List<Motorcycle>> GetByLicencePlate(string licencePlate);
        Task Delete(string motorcycleId);
    }
}
