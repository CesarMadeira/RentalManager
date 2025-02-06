using RentalManager.Application.Interfaces.Queries;
using RentalManager.Application.Queries.Request;
using RentalManager.Application.Queries.Response;
using RentalManager.Domain.Interfaces.Respositories;

namespace RentalManager.Application.Queries.Handlers
{
    public class SearchMotorcycleByLicensePlateQueryHandler : ISearchMotorcycleByLicensePlateQueryHandler
    {
        private readonly IMotorcycleRepository _motorcycleRepository;

        public SearchMotorcycleByLicensePlateQueryHandler(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task<SearchMotorcycleByLicensePlateQueryResponse> Handle(SearchMotorcycleByLicensePlateQueryRequest request)
        {
            //var query = @"select
            //                identifier,
            //                licence_plate as licenceplate,
            //                model,
            //                year
            //            from motorcycle
            //        where licence_plate like @licencePlate";

            //var queryResult = await _db.QueryAsync<Motorcycle>(query, new { licencePlate = $"%{licencePlate}%" });


            var listMotorcycle = await _motorcycleRepository.GetByLicencePlate(request.LicencePlate);

            if (listMotorcycle == null)
                throw new Exception("Moto não encontrada!");

            var response = new SearchMotorcycleByLicensePlateQueryResponse();

            //listMotorcycle.Select(s => response.Item.Add(new SearchMotorcycleByLicensePlateQueryResponseItem
            //{
            //    Identifier = s.Identifier,
            //    LicencePlate = s.LicencePlate,
            //    Model = s.Model,
            //    Year = s.Year
            //}));

            return response;
        }
    }
}
