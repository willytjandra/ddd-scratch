using Domain.Customers;
using MediatR;

namespace Application.Customers.GetCustomerById;

public sealed record GetCustomerByIdQuery(CustomerId Id) : IRequest<CustomerDto?>;
