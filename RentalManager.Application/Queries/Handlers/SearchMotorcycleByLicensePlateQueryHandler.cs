using Dapper;
using RentalManager.Application.Interfaces.Queries;
using RentalManager.Application.Queries.Request;
using RentalManager.Application.Queries.Response;
using RentalManager.Infra.Dapper;
using System.Data;

namespace RentalManager.Application.Queries.Handlers
{
    public class SearchMotorcycleByLicensePlateQueryHandler : ISearchMotorcycleByLicensePlateQueryHandler
    {

        private readonly IDbConnection _db;

        public SearchMotorcycleByLicensePlateQueryHandler(DatabaseConnection databaseConnection)
        {
            _db = databaseConnection.CreateConnection();
        }

        public async Task<SearchMotorcycleByLicensePlateQueryResponse> Handle(SearchMotorcycleByLicensePlateQueryRequest request)
        {
            var query = @"select
                            identifier as Id,
                            licence_plate as LicencePlate,
                            model,
                            year
                        from motorcycle
                    where licence_plate like @licencePlate";

            var queryResult = await _db.QueryAsync<SearchMotorcycleByLicensePlateQueryResponseItem>(query, new { licencePlate = $"%{request.LicencePlate}%" });

            var response = new SearchMotorcycleByLicensePlateQueryResponse();
            response.Item = queryResult.ToList();

            return response;
        }
    }
}
