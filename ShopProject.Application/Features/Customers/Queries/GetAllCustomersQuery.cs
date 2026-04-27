using MediatR;
using ShopProject.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Features.Customers.Queries
{
    public record GetAllCustomersQuery : IRequest<IEnumerable<CustomerDTO>>;

    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerDTO>>
    {
        private readonly IUnitOfWork uow;

        public GetAllCustomersQueryHandler(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public async Task<IEnumerable<CustomerDTO>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await uow.Customers.GetAllAsync();

            return customers.Select(c => new CustomerDTO
            {
                Name = c.Name,
                Phone = c.Phone,
                Email = c.Email,
                Balance = c.Balance
            });
        }
    }
}
