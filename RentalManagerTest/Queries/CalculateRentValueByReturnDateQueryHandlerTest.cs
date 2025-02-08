using RentalManager.Application.Queries.Handlers;
using RentalManager.Application.Queries.Request;
using RentalManager.Application.Queries.Response;
using RentalManager.Domain.Entities;
using RentalManager.Domain.Interfaces.Respositories;
using RentalManager.Infra.Dapper;
using RentalManager.Infra.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RentalManagerTest.Queries
{
    [TestClass]
    public class CalculateRentValueByReturnDateQueryHandlerTest
    {
        private IRentRepository _rentRepository;
        private IDeliveryPersonRepository _deliveryPersonRepository;
        private IMotorcycleRepository _motorcycleRepository;

        public CalculateRentValueByReturnDateQueryHandlerTest()
        {
            var conn = new DatabaseConnection("Host=localhost;Port=5432;Database=rentalmanager_db;Username=postgres;Password=postgres");

            _rentRepository = new RentRepository(conn);
            _deliveryPersonRepository = new DeliveryPersonRepository(conn);
            _motorcycleRepository = new MotorcycleRepository(conn);
        }

        [TestMethod("Deve calcular valor a ser pago")]
        public async Task ShouldCalculateRentValueByReturnDate()
        {
            var deliveryPerson = new DeliveryPerson(Guid.NewGuid().ToString(), "teste", "81388311000171", DateTime.Now, "34569709670", "A", null);
            await _deliveryPersonRepository.Create(deliveryPerson);

            var motorcycle = new Motorcycle(Guid.NewGuid().ToString(), "CDX-9999", "Sport", 2025);
            await _motorcycleRepository.Create(motorcycle);

            var rent = new Rent(Guid.NewGuid().ToString(), deliveryPerson.Id, motorcycle.Id, new DateTime(2025, 02, 7), null, new DateTime(2025, 02, 14), 7);
            await _rentRepository.Create(rent);

            var queryHandler = new CalculateRentValueByReturnDateQueryHandler(_rentRepository);
            var request = new CalculateRentValueByDateQueryRequest
            {
                RentId = rent.Id,
                EndDate = new DateTime(2025, 02, 14)
            };
            var response = await queryHandler.Handle(request);

            await _deliveryPersonRepository.Delete(deliveryPerson.Id);
            await _motorcycleRepository.Delete(motorcycle.Id);
            await _rentRepository.Delete(rent.Id);

            Assert.AreEqual(210, response.RentalValue);
        }

        [TestMethod("Deve calcular valor a ser pago com data anterior a previsão")]
        public async Task ShouldCalculateRentValueByReturnDateBeforeForecast()
        {
            var deliveryPerson = new DeliveryPerson(Guid.NewGuid().ToString(), "teste", "56262112000175", DateTime.Now, "96328255924", "A", null);
            await _deliveryPersonRepository.Create(deliveryPerson);

            var motorcycle = new Motorcycle(Guid.NewGuid().ToString(), "CDX-9999", "Sport", 2025);
            await _motorcycleRepository.Create(motorcycle);

            var rent = new Rent(Guid.NewGuid().ToString(), deliveryPerson.Id, motorcycle.Id, new DateTime(2025, 02, 7), null, new DateTime(2025, 02, 14), 7);
            await _rentRepository.Create(rent);

            var queryHandler = new CalculateRentValueByReturnDateQueryHandler(_rentRepository);
            var request = new CalculateRentValueByDateQueryRequest
            {
                RentId = rent.Id,
                EndDate = new DateTime(2025, 02, 12)
            };
            var response = await queryHandler.Handle(request);

            await _deliveryPersonRepository.Delete(deliveryPerson.Id);
            await _motorcycleRepository.Delete(motorcycle.Id);
            await _rentRepository.Delete(rent.Id);

            Assert.AreEqual(162, response.RentalValue);
        }

        [TestMethod("Deve calcular valor a ser pago com data posterior a previsão")]
        public async Task ShouldCalculateRentValueByReturnDateAfterForecast()
        {
            var deliveryPerson = new DeliveryPerson(Guid.NewGuid().ToString(), "teste", "33815534000125", DateTime.Now, "32654675141", "A", null);
            await _deliveryPersonRepository.Create(deliveryPerson);

            var motorcycle = new Motorcycle(Guid.NewGuid().ToString(), "CDX-9999", "Sport", 2025);
            await _motorcycleRepository.Create(motorcycle);

            var rent = new Rent(Guid.NewGuid().ToString(), deliveryPerson.Id, motorcycle.Id, new DateTime(2025, 02, 7), null, new DateTime(2025, 02, 14), 7);
            await _rentRepository.Create(rent);

            var queryHandler = new CalculateRentValueByReturnDateQueryHandler(_rentRepository);
            var request = new CalculateRentValueByDateQueryRequest
            {
                RentId = rent.Id,
                EndDate = new DateTime(2025, 02, 16)
            };
            var response = await queryHandler.Handle(request);

            await _deliveryPersonRepository.Delete(deliveryPerson.Id);
            await _motorcycleRepository.Delete(motorcycle.Id);
            await _rentRepository.Delete(rent.Id);

            Assert.AreEqual(310, response.RentalValue);
        }

        [TestMethod("Deve calcular valor a ser pago com quantidade minima de dias")]
        public async Task ShouldCalculateRentValueByReturnDateAfterForecastWithMinimumDays()
        {
            var deliveryPerson = new DeliveryPerson(Guid.NewGuid().ToString(), "teste", "29842136000120", DateTime.Now, "77541118740", "A", null);
            await _deliveryPersonRepository.Create(deliveryPerson);

            var motorcycle = new Motorcycle(Guid.NewGuid().ToString(), "CDX-9999", "Sport", 2025);
            await _motorcycleRepository.Create(motorcycle);

            var rent = new Rent(Guid.NewGuid().ToString(), deliveryPerson.Id, motorcycle.Id, new DateTime(2025, 02, 7), null, new DateTime(2025, 02, 14), 7);
            await _rentRepository.Create(rent);

            var queryHandler = new CalculateRentValueByReturnDateQueryHandler(_rentRepository);
            var request = new CalculateRentValueByDateQueryRequest
            {
                RentId = rent.Id,
                EndDate = new DateTime(2025, 02, 07)
            };
            var response = await queryHandler.Handle(request);

            await _deliveryPersonRepository.Delete(deliveryPerson.Id);
            await _motorcycleRepository.Delete(motorcycle.Id);
            await _rentRepository.Delete(rent.Id);

            Assert.AreEqual(42, response.RentalValue);
        }
    }
}
