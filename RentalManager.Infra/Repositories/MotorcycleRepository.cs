using Dapper;
using RentalManager.Domain.Entities;
using RentalManager.Domain.Interfaces.Respositories;
using RentalManager.Infra.Dapper;
using System.Data;

namespace RentalManager.Infra.Repositories;

public class MotorcycleRepository : IMotorcycleRepository
{
    private readonly IDbConnection _db;

    public MotorcycleRepository(DatabaseConnection databaseConnection)
    {
        _db = databaseConnection.CreateConnection();
    }

    public async Task Create(Motorcycle motorcycle)
    {
        var query = @"insert into motorcycle values (@identifier, @licence_plate, @model, @year)";
        await _db.ExecuteAsync(query, new
        {
            identifier = motorcycle.Id,
            licence_plate = motorcycle.LicencePlate,
            model = motorcycle.Model,
            year = motorcycle.Year
        });
    }

    public async Task Save(Motorcycle motorcycle)
    {
        var query = @"update motorcycle set licence_plate = @licence_plate, model = @model, year = @year where identifier = @id";
        await _db.ExecuteAsync(query, new
        {
            id = motorcycle.Id,
            licence_plate = motorcycle.LicencePlate,
            model = motorcycle.Model,
            year = motorcycle.Year
        });
    }

    public async Task<Motorcycle> Get(string id)
    {
        var query = @"select
                            identifier as Id,
                            licence_plate as licenceplate,
                            model,
                            year
                        from motorcycle
                    where identifier = @id";
        var queryResult = await _db.QueryAsync<Motorcycle>(query, new { id = id });
        return queryResult.FirstOrDefault();
    }

    public async Task<List<Motorcycle>> GetByLicencePlate(string licencePlate)
    {
        var query = @"select
                            identifier as Id,
                            licence_plate as licenceplate,
                            model,
                            year
                        from motorcycle
                    where licence_plate like @licencePlate";

        var queryResult = await _db.QueryAsync<Motorcycle>(query, new { licencePlate = $"%{licencePlate}%" });
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
