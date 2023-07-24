using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Customers.GetAllCustomers;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IReadOnlyCollection<CustomerDto>>
{
	public GetAllCustomersQueryHandler(ApplicationDbContext dbContext)
	{
        _dbContext = dbContext;
	}

    private readonly ApplicationDbContext _dbContext;

    public async Task<IReadOnlyCollection<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await _dbContext.Customers.ToListAsync();

        var customerDtos = customers
            .Select(c => new CustomerDto(c.Id.Value, c.FirstName, c.LastName, c.Email))
            .ToList()
            .AsReadOnly();

        return customerDtos;
    }
}

