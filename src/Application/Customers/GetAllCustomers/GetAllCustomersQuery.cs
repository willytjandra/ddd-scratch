using MediatR;

namespace Application.Customers.GetAllCustomers;

public sealed record GetAllCustomersQuery() : IRequest<IReadOnlyCollection<CustomerDto>>;

