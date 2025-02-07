﻿using RentalManager.Application.Commands.Handlers;
using RentalManager.Application.Interfaces.Commands;
using RentalManager.Application.Interfaces.Queries;
using RentalManager.Application.Queries.Handlers;
using RentalManager.Domain.Interfaces.Respositories;
using RentalManager.Infra.Repositories;

namespace RentalManager.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterApiServices(this IServiceCollection services)
    {
        //commands
        services.AddScoped<IRegisterNewMotorcycleCommandHandler, RegisterNewMotorcycleCommandHandler>();
        services.AddScoped<IRemoveMotorcycleCommandHandler, RemoveMotorcycleCommandHandler>();
        services.AddScoped<IModifyMotorcycleLicensePlateCommandHandler, ModifyMotorcycleLicensePlateCommandHandler>();

        //queries
        services.AddScoped<ISearchMotorcycleByLicensePlateQueryHandler, SearchMotorcycleByLicensePlateQueryHandler>();
        services.AddScoped<IGetMotorcycleByIdQueryHandler, GetMotorcycleByIdQueryHandler>();

        //repositorios
        services.AddScoped<IDeliveryPersonRepository, DeliveryPersonRepository>();
        services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
        services.AddScoped<IRentRepository, RentRepository>();

        ////services
        //services.AddScoped<IEventoProdutoService, EventoProdutoService>();
        //services.AddScoped<IEventoCarteiraService, EventoCarteiraService>();
        //services.AddScoped<IEmailService, EmailService>();
    }
}
