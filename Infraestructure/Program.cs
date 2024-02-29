using Applications;
using Applications.Interfaces;
using Domain.Interfaces;
using Infraestructure.Context;
using Infraestructure.Infraestructure;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks();

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});


// Add services to the container.
builder.Services.AddScoped<IDockingService, DockingService>();
builder.Services.AddScoped<IDockingRepository, DockingRepository>();

builder.Services.AddSingleton(provider =>
{
    var factory = new ConnectionFactory()
    {
        HostName = "localhost",
        Port = 5675,
        UserName = "guest",
        Password = "guest"
    };
    return factory.CreateConnection();
});

builder.Services.AddSingleton(provider =>
{
    var connection = provider.GetRequiredService<IConnection>();
    var model = connection.CreateModel();
    return model;
});
builder.Services.AddSingleton<IRabbitMQSetupService, RabbitMQSetupService>();

builder.Services.AddScoped<IMessagePublisher, MessagePublisher>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DBConnectionString");
builder.Services.AddDbContext<ApgManagementDbContext>(options => options.UseSqlServer(connectionString));


var app = builder.Build();

// Configurar RabbitMQ
var rabbitMQSetupService = app.Services.GetRequiredService<IRabbitMQSetupService>();
rabbitMQSetupService.Setup();


// Initialize the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApgManagementDbContext>();
    context.Database.EnsureCreated();
}


app.MapHealthChecks("/health");

app.UseSwagger();
app.UseSwaggerUI();
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

