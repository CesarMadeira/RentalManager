using Dapper;
using RentalManager.Domain.Entities;
using RentalManager.Domain.Interfaces.Respositories;
using RentalManager.Infra.Dapper;
using System.Data;

namespace RentalManager.Infra.Repositories;

public class DeliveryPersonRepository : IDeliveryPersonRepository
{
    private readonly IDbConnection _db;

    public DeliveryPersonRepository(DatabaseConnection databaseConnection)
    {
        _db = databaseConnection.CreateConnection();
    }

    public async Task Create(DeliveryPerson deliveryPerson)
    {
        var query = @"insert into delivery_person values (@id, @name, @cnpj, @dateOfBirth, @documentNumber, @documentType)";
        await _db.ExecuteAsync(query, new
        {
            id = deliveryPerson.Id,
            name = deliveryPerson.Name,
            cnpj = deliveryPerson.CNPJ.Value,
            dateOfBirth = deliveryPerson.DateOfBirth,
            documentNumber = deliveryPerson.DocumentNumber.Value,
            documentType = deliveryPerson.DocumentType
        });
    }

    public async Task<DeliveryPerson> Get(string id)
    {
        var query = @"
                    select
	                    id,
	                    name,
	                    cnpj,
	                    date_of_birth as dateOfBirth,
	                    document_number as documentNumber,
	                    document_type as documentType,
	                    document_image as documentImage
                    from delivery_person
                    where id = @id";
        var queryResult = await _db.QueryAsync<DeliveryPerson>(query, new { id = id });
        return queryResult.FirstOrDefault();
    }

    public async Task Delete(string id)
    {
        var query = @"delete from delivery_person where id = @id";
        await _db.ExecuteAsync(query, new
        {
            id = id
        });
    }

}
