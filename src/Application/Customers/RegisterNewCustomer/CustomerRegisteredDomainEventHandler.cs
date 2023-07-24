using Domain.Customers;
using MediatR;

namespace Application.Customers.RegisterNewCustomer;

public class CustomerRegisteredDomainEventHandler : INotificationHandler<CustomerRegisteredDomainEvent>
{
	public CustomerRegisteredDomainEventHandler()
	{
	}

    public Task Handle(CustomerRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

