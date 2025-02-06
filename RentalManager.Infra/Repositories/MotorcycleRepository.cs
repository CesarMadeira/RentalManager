using Dapper;
using RentalManager.Domain.Entities;
using RentalManager.Domain.Interfaces.Respositories;
using RentalManager.Infra.Dapper;
using System.Data;

namespace RentalManager.Infra.Repositories
{
    public class MotorcycleRepository : IMotorcycleRepository
    {
        private readonly IDbConnection _db;

        public MotorcycleRepository(DatabaseConnection databaseConnection)
        {
            _db = databaseConnection.CreateConnection();
        }

        public async Task Save(Motorcycle motorcycle)
        {
            var query = @"insert into motorcycle values (@identifier, @licence_plate, @model, @year)";
            await _db.ExecuteAsync(query, new
            {
                identifier = motorcycle.Identifier,
                licence_plate = motorcycle.LicencePlate,
                model = motorcycle.Model,
                year = motorcycle.Year
            });
        }

        public async Task<Motorcycle> Get(string identifier)
        {
            var query = @"select
                            identifier,
                            licence_plate as licenceplate,
                            model,
                            year
                        from motorcycle
                    where identifier = @identifier";
            var queryResult = await _db.QueryAsync<Motorcycle>(query, new { identifier = identifier });
            var response = queryResult.FirstOrDefault();
            if (response == null)
                throw new Exception("Moto não encontrada!");
            return response;
        }

        public async Task<List<Motorcycle>> GetByLicencePlate(string licencePlate)
        {
            var query = @"select
                            identifier,
                            licence_plate as licenceplate,
                            model,
                            year
                        from motorcycle
                    where licence_plate like @licencePlate";

            var queryResult = await _db.QueryAsync<Motorcycle>(query, new { licencePlate = $"%{licencePlate}%" });

            //var queryResult = await _db.QueryAsync<Motorcycle>(query, new { licencePlate = licencePlate });
            return queryResult.ToList();
        }

        public async Task Delete(string motorcycleId)
        {
            var query = @"delete from motorcycle where identifier = @identifier";
            await _db.ExecuteAsync(query, new
            {
                identifier = motorcycleId
            });
        }
    }
}
