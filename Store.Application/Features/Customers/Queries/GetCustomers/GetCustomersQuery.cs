using MediatR;
using Store.Application.Commons;
using Store.Application.DTOs.Customers;

namespace Store.Application.Features.Customers.Queries.GetCustomers
{
    public record GetCustomersQuery(int PageNumber, int PageSize)
        : IRequest<PagedResult<CustomerResponse>>;
}
