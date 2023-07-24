using Domain.Primitives;

namespace Domain.Customers;

public class Customer : Entity
{
	private Customer()
	{
	}

	public CustomerId Id { get; private set; }

	public string FirstName { get; private set; }

	public string LastName { get; private set; }

	public string Email { get; private set; }

	public UserStatus Status { get; private set; }

	public static Customer New(string firstName, string lastName, string email)
	{
		var customer = new Customer
		{
			Id = new CustomerId(Guid.NewGuid()),
			FirstName = firstName,
			LastName = lastName,
			Email = email,
		};

		customer.Raise(new CustomerRegisteredDomainEvent(Guid.NewGuid(), customer.Id));

		return customer;
	}
}
