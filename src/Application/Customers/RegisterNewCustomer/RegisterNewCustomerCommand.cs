using MediatR;

namespace Application.Customers.RegisterNewCustomer;

public sealed record RegisterNewCustomerCommand(
    string FirstName,
    string LastName,
    string Email) : IRequest;

