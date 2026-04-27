using MediatR;
using ShopProject.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Features.Customers.Commands.Handlers
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly IUnitOfWork uow;

        public CreateCustomerHandler(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var Customer = new Domain.Entities.Customer
            {
                Name = request.Name,
                Phone = request.Phone,
                Email = request.Email
            };
            await uow.Customers.AddAsync(Customer);
            await uow.CompleteAsync();

            return Customer.Id;
        }
    }
}
