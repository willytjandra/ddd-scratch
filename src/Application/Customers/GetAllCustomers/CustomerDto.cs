using System;
namespace Application.Customers.GetAllCustomers;

public sealed record CustomerDto(Guid Id, string FirstName, string LastName, string Email);

