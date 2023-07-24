using Domain.Primitives;

namespace Domain.Customers;

public record CustomerRegisteredDomainEvent(Guid Id, CustomerId customerId)
    : DomainEvent(Id);

