using Dapper;
using RentalManager.Domain.Entities;
using RentalManager.Domain.Interfaces.Respositories;
using RentalManager.Domain.ValueObject;
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

    public async Task Save(DeliveryPerson deliveryPerson)
    {
        var query = @"update delivery_person set
                        name = @Name,
                        cnpj = @CNPJ,
                        date_of_birth = @DateOfBirth,
                        document_number = @documentNumber,
                        document_type = @documentType,
                        document_image = @documentImage
                      where id = @Id";
        await _db.ExecuteAsync(query, new
        {
            Id = deliveryPerson.Id,
            name = deliveryPerson.Name,
            CNPJ = deliveryPerson.CNPJ.Value,
            DateOfBirth = deliveryPerson.DateOfBirth,
            DocumentNumber = deliveryPerson.DocumentNumber.Value,
            DocumentType = deliveryPerson.DocumentType,
            DocumentImage = deliveryPerson.DocumentImage
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
        await _db.ExecuteAsync(query, new { id = id });
    }

    public async Task<DeliveryPerson> GetByCNPJ(CNPJ cnpj)
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
                    where cnpj = @cnpj";
        var queryResult = await _db.QueryAsync<DeliveryPerson>(query, new { cnpj = cnpj.Value });
        return queryResult.FirstOrDefault();
    }

    public async Task<DeliveryPerson> GetByCNH(CNH cnh)
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
                    where document_number = @cnh";
        var queryResult = await _db.QueryAsync<DeliveryPerson>(query, new { cnh = cnh.Value });
        return queryResult.FirstOrDefault();
    }
}
