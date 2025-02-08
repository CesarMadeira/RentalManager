using Dapper;
using RentalManager.Domain.Entities;
using RentalManager.Domain.Interfaces.Respositories;
using RentalManager.Infra.Dapper;
using System.Data;

namespace RentalManager.Infra.Repositories;

public class RentRepository : IRentRepository
{
    private readonly IDbConnection _db;

    public RentRepository(DatabaseConnection databaseConnection)
    {
        _db = databaseConnection.CreateConnection();
    }

    public async Task Create(Rent rent)
    {
        var query = @"insert into rent values (@Id, @DeliveryPersonId, @MotorcycleId, @Start, @Finish, @EndForecast, @Plan)";
        await _db.ExecuteAsync(query, new
        {
            Id = rent.Id,
            DeliveryPersonId = rent.DeliveryPersonId,
            MotorcycleId = rent.MotorcycleId,
            Start = rent.Start,
            Finish = rent.Finish,
            EndForecast = rent.EndForecast,
            Plan = rent.Plan
        });
    }

    public async Task Delete(string id)
    {
        var query = @"delete from rent where id = @id";
        await _db.ExecuteAsync(query, new
        {
            id = id
        });
    }

    public async Task<Rent> Get(string id)
    {
        try
        {
            var query = @"select
	                            id,
	                            delivery_person_id as DeliveryPersonId,
	                            motorcycle_id as motorcycleId,
	                            start,
	                            finish,
	                            end_forecast as endForecast,
	                            Plan
                            from rent
                            where id = @id";
            var queryResult = await _db.QueryAsync<Rent>(query, new { id = id });
            return queryResult.FirstOrDefault();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<bool> HasRentals(string motocycleId)
    {
        var query = @"select 
		                    case when count(*) > 0 then 1 else 0 end as response
	                    from rent 
                    where motorcycle_id = @motocycleId";
        var queryResult = await _db.QueryAsync<bool>(query, new { motocycleId = motocycleId });
        return queryResult.FirstOrDefault();
    }
}
