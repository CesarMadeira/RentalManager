using RentalManager.Application.Queries.Handlers;
using RentalManager.Application.Queries.Request;
using RentalManager.Domain.Entities;
using RentalManager.Domain.Interfaces.Respositories;
using RentalManager.Infra.Dapper;
using RentalManager.Infra.Repositories;

namespace RentalManagerTest.Queries
{
    [TestClass]
    public class SearchMotorcycleByLicensePlateQueryHandlerTest
    {
        private IMotorcycleRepository _motorcycleRepository;

        public SearchMotorcycleByLicensePlateQueryHandlerTest()
        {
            var conn = new DatabaseConnection("Host=localhost;Port=5432;Database=rentalmanager_db;Username=postgres;Password=postgres");

            _motorcycleRepository = new MotorcycleRepository(conn);
        }

        [TestMethod("Deve buscar uma moto pela placa")]
        public async Task ShouldSearchMotorcycleByLicensePlateUse()
        {
            await _motorcycleRepository.Create(new Motorcycle("moto123", "CDX-0101", "Sport", 2020));
            await _motorcycleRepository.Create(new Motorcycle("moto123", "CDX-0102", "Sport", 2020));
            await _motorcycleRepository.Create(new Motorcycle("moto123", "AAA-0103", "Sport", 2020));

            var searchMotorcycleByLicensePlateQueryHandler = new SearchMotorcycleByLicensePlateQueryHandler(_motorcycleRepository);
            var response = await searchMotorcycleByLicensePlateQueryHandler.Handle(new SearchMotorcycleByLicensePlateQueryRequest
            {
                LicencePlate = "CDX"
            });

            await _motorcycleRepository.Delete("moto123");

            //Assert.AreEqual(2, response.Count);
        }
    }
}
