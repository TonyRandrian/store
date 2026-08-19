using MediatR;
using Store.Application.DTOs.Customers;

namespace Store.Application.Features.Customers.Queries.GetCustomer
{
    public record GetCustomerQuery(Guid Id) : IRequest<CustomerResponse>;
}
