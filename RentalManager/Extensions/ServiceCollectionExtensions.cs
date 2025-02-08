using RentalManager.Application.Commands.Handlers;
using RentalManager.Application.Interfaces.Commands;
using RentalManager.Application.Interfaces.Queries;
using RentalManager.Application.Queries.Handlers;
using RentalManager.Domain.Interfaces.Respositories;
using RentalManager.Infra.Repositories;
using RentalManager.Infra.Worker;

namespace RentalManager.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterApiServices(this IServiceCollection services)
    {
        //commands
        services.AddScoped<IRegisterNewMotorcycleCommandHandler, RegisterNewMotorcycleCommandHandler>();
        services.AddScoped<IRemoveMotorcycleCommandHandler, RemoveMotorcycleCommandHandler>();
        services.AddScoped<IModifyMotorcycleLicensePlateCommandHandler, ModifyMotorcycleLicensePlateCommandHandler>();
        services.AddScoped<IRegisterDeliveryPersonCommandHandler, RegisterDeliveryPersonCommandHandler>();
        services.AddScoped<IRentMotorcycleCommandHandler, RentMotorcycleCommandHandler>();

        //queries
        services.AddScoped<ISearchMotorcycleByLicensePlateQueryHandler, SearchMotorcycleByLicensePlateQueryHandler>();
        services.AddScoped<IGetMotorcycleByIdQueryHandler, GetMotorcycleByIdQueryHandler>();
        services.AddScoped<ICalculateRentValueByReturnDateQueryHandler, CalculateRentValueByReturnDateQueryHandler>();

        //repositorios
        services.AddScoped<IDeliveryPersonRepository, DeliveryPersonRepository>();
        services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
        services.AddScoped<IRentRepository, RentRepository>();

        //worker
        services.AddHostedService<ConsumeQueueMessagesBackgroundService>();

        ////services
        //services.AddScoped<IEventoProdutoService, EventoProdutoService>();
        //services.AddScoped<IEventoCarteiraService, EventoCarteiraService>();
        //services.AddScoped<IEmailService, EmailService>();
    }
}
