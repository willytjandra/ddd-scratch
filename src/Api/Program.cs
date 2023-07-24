using Application.Customers.GetAllCustomers;
using Application.Customers.GetCustomerById;
using Application.Customers.RegisterNewCustomer;
using Domain.Customers;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
{
    var connectionString = builder.Configuration.GetConnectionString("Database");
    optionsBuilder.UseNpgsql(connectionString);
});

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.MapGet("/customers", async Task<Results<Ok<IReadOnlyCollection<Application.Customers.GetAllCustomers.CustomerDto>>, NotFound>>  (
    ISender sender,
    CancellationToken cancellationToken) => {

        var query = new GetAllCustomersQuery();

        var customers = await sender.Send(query, cancellationToken);
        if (!customers.Any())
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(customers);
    })
.WithName("GetAllCustomers")
.WithOpenApi();

app.MapGet("/customers/{id:Guid}", async Task<Results<Ok<Application.Customers.GetCustomerById.CustomerDto>, NotFound>> (
    Guid id,
    ISender sender,
    CancellationToken cancellationToken) => {

        var query = new GetCustomerByIdQuery(new CustomerId(id));

        var customerDto = await sender.Send(query, cancellationToken);

        if (customerDto is null)
        {
            return TypedResults.NotFound();
        }
        else
        {
            return TypedResults.Ok(customerDto);
        }
    })
.WithName("GetCustomer")
.WithOpenApi();

app.MapPost("/customers", async (
    RegisterNewCustomerRequest request,
    ISender sender,
    CancellationToken cancellationToken) =>
{
    var command = new RegisterNewCustomerCommand(request.FirstName, request.LastName, request.Email);
    await sender.Send(command, cancellationToken);
})
.WithName("RegisterNewCustomer")
.WithOpenApi();

app.Run();

public sealed record RegisterNewCustomerRequest(string FirstName, string LastName, string Email);
