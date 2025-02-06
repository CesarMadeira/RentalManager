using RentalManager.Application.Queries.Request;
using RentalManager.Application.Queries.Response;

namespace RentalManager.Application.Interfaces.Queries;

public interface ISearchMotorcycleByLicensePlateQueryHandler
{
    Task<SearchMotorcycleByLicensePlateQueryResponse> Handle(SearchMotorcycleByLicensePlateQueryRequest request);
}
