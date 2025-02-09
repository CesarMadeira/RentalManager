using Microsoft.Extensions.Options;
using RentalManager.Extensions;
using RentalManager.Infra.Dapper;
using RentalManager.Infra.RabbitMQ;
using RentalManager.Infra.RabbitMQ.Model;
using RentalManager.Middleware;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton<DatabaseConnection>(provider => new DatabaseConnection(connectionString));

builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMqSettings"));
builder.Services.AddSingleton<RabbitMqConnection>(provider => 
{ 
    var options = provider.GetRequiredService<IOptions<RabbitMqSettings>>();
    return new RabbitMqConnection(options);
});

// Add services to the container.
builder.Services.RegisterApiServices();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
