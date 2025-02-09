using Dapper;
using RentalManager.Domain.Entities.Events;
using RentalManager.Domain.Interfaces.Respositories;
using RentalManager.Infra.Dapper;
using System.Data;

namespace RentalManager.Infra.Repositories;

public class MotorcycleEventRepository : IMotorcycleEventRepository
{
    private readonly IDbConnection _db;

    public MotorcycleEventRepository(DatabaseConnection databaseConnection)
    {
        _db = databaseConnection.CreateConnection();
    }

    public async Task Create(MotorcycleEvent motorcycleEvent)
    {
        var query = @"insert into motorcycle_event values (@Id, @MortorcycleId, @LicencePlate, @Model, @Year, @Type, @CreatedAt)";
        await _db.ExecuteAsync(query, new
        {
            Id = motorcycleEvent.Id,
            MortorcycleId = motorcycleEvent.MortorcycleId,
            LicencePlate = motorcycleEvent.LicencePlate,
            Model = motorcycleEvent.Model,
            Year = motorcycleEvent.Year,
            Type = motorcycleEvent.Type,
            CreatedAt = motorcycleEvent.CreatedAt
        });
    }
}
