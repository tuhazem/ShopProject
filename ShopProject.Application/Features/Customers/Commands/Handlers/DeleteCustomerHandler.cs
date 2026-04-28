using MediatR;
using ShopProject.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Features.Customers.Commands.Handlers
{
    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly IUnitOfWork uow;

        public DeleteCustomerHandler(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            
            var customer = await uow.Customers.GetByIdAsync(request.Id);
            if (customer == null || customer.IsDeleted) return false;

            customer.IsDeleted = true;
            uow.Customers.Delete(customer);
            await uow.CompleteAsync();

            return true;
        }
    }
}
