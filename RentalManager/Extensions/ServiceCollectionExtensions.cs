using RentalManager.Application.Commands.Handlers;
using RentalManager.Application.Interfaces.Commands;
using RentalManager.Application.Interfaces.Queries;
using RentalManager.Application.Queries.Handlers;
using RentalManager.Domain.Interfaces.Messages.Consumers;
using RentalManager.Domain.Interfaces.Messages.Producers;
using RentalManager.Domain.Interfaces.Respositories;
using RentalManager.Infra.Messages.Consumers;
using RentalManager.Infra.Messages.Producers;
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
        services.AddScoped<ISendPhotoOfDocumentCommandHandler, SendPhotoOfDocumentCommandHandler>();

        //queries
        services.AddScoped<ISearchMotorcycleByLicensePlateQueryHandler, SearchMotorcycleByLicensePlateQueryHandler>();
        services.AddScoped<IGetMotorcycleByIdQueryHandler, GetMotorcycleByIdQueryHandler>();
        services.AddScoped<ICalculateRentValueByReturnDateQueryHandler, CalculateRentValueByReturnDateQueryHandler>();
        services.AddScoped<IGetRentByIdQueryHandler, GetRentByIdQueryHandler>();

        //repositorios
        services.AddScoped<IDeliveryPersonRepository, DeliveryPersonRepository>();
        services.AddScoped<IRentRepository, RentRepository>();
        services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
        services.AddSingleton<IMotorcycleEventRepository, MotorcycleEventRepository>();

        //worker
        services.AddHostedService<ConsumeQueueMessagesBackgroundService>();

        // consumer
        services.AddSingleton<IMotorcycleCreatedEventConsumer, MotorcycleCreatedEventConsumer>();

        // producers
        services.AddSingleton<IMotorcycleCreatedEventProducer, MotorcycleCreatedEventProducer>();
    }
}
