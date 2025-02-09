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
        private DatabaseConnection _conn;
        private IMotorcycleRepository _motorcycleRepository;

        public SearchMotorcycleByLicensePlateQueryHandlerTest()
        {
            _conn = new DatabaseConnection("Host=localhost;Port=5432;Database=rentalmanager_db;Username=postgres;Password=postgres");
            _motorcycleRepository = new MotorcycleRepository(_conn);
        }

        [TestMethod("Deve buscar uma moto pela placa")]
        public async Task ShouldSearchMotorcycleByLicensePlateUse()
        {
            await _motorcycleRepository.Create(new Motorcycle("moto123", "CDA-0101", "Sport", 2020));
            await _motorcycleRepository.Create(new Motorcycle("moto123", "CDA-0102", "Sport", 2020));
            await _motorcycleRepository.Create(new Motorcycle("moto123", "AAA-0103", "Sport", 2020));

            var searchMotorcycleByLicensePlateQueryHandler = new SearchMotorcycleByLicensePlateQueryHandler(_conn);
            var response = await searchMotorcycleByLicensePlateQueryHandler.Handle(new SearchMotorcycleByLicensePlateQueryRequest
            {
                LicencePlate = "CDA"
            });

            await _motorcycleRepository.Delete("moto123");

            Assert.AreEqual(2, response.Item.Count);
        }
    }
}
