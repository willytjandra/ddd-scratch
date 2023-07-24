using MediatR;
using Persistence;

namespace Application.Customers.GetCustomerById;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto?>
{
    public GetCustomerByIdQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private readonly ApplicationDbContext _dbContext;

    public async Task<CustomerDto?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _dbContext.Customers.FindAsync(request.Id);

        if (customer is null)
        {
            return null;
        }

        var customerDto = new CustomerDto(customer.Id.Value, customer.FirstName, customer.LastName, customer.Email);

        return customerDto;
    }
}

