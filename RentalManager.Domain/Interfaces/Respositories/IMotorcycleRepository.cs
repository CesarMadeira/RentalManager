using RentalManager.Domain.Entities;

namespace RentalManager.Domain.Interfaces.Respositories
{
    public interface IMotorcycleRepository
    {
        Task Save(Motorcycle motorcycle);
        Task<Motorcycle> Get(string identifier);
        Task<List<Motorcycle>> GetByLicencePlate(string licencePlate);
        Task Delete(string motorcycleId);
    }
}
