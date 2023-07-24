using MediatR;
using Persistence;
using Domain.Customers;

namespace Application.Customers.RegisterNewCustomer;

public class RegisterNewCustomerCommandHandler : IRequestHandler<RegisterNewCustomerCommand>
{
	public RegisterNewCustomerCommandHandler(ApplicationDbContext dbContext)
	{
        _dbContext = dbContext;
	}

    private readonly ApplicationDbContext _dbContext;

    public async Task Handle(RegisterNewCustomerCommand request, CancellationToken cancellationToken)
    {
        var newCustomer = Customer.New(request.FirstName, request.LastName, request.Email);

        _dbContext.Set<Customer>()
            .Add(newCustomer);

        await _dbContext.SaveChangesAsync();
    }
}

