using Domain.Customers;
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

app.MapGet("/customers", async (ApplicationDbContext dbContext) => {
    var customers = await dbContext.Customers.ToListAsync();
    return customers;
})
.WithName("GetAllCustomers")
.WithOpenApi();


app.MapGet("/customers/{id:Guid}", async Task<Results<Ok<Customer>, NotFound>> (
    Guid id,
    ApplicationDbContext dbContext) => {

        var customerId = new CustomerId(id);
        var customer = await dbContext.Customers.FindAsync(customerId);
        if (customer is null)
        {
            return TypedResults.NotFound();
        }
        else
        {
            return TypedResults.Ok(customer);
        }
    })
.WithName("GetCustomer")
.WithOpenApi();

app.MapPost("/customers", async (RegisterNewCustomerRequest request, ApplicationDbContext dbContext) =>
{
    dbContext.Set<Customer>().Add(Customer.New(request.FirstName, request.LastName, request.Email));
    await dbContext.SaveChangesAsync();
})
.WithName("RegisterNewCustomer")
.WithOpenApi();

app.Run();

public sealed record RegisterNewCustomerRequest(string FirstName, string LastName, string Email);
