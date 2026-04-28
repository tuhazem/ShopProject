using AutoMapper;
using MediatR;
using ShopProject.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Features.Customers.Commands.Handlers
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, bool>
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public UpdateCustomerHandler(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await uow.Customers.GetByIdAsync(request.Id);
            if (customer == null) return false;

            mapper.Map(request, customer);
            uow.Customers.Update(customer);
            await uow.CompleteAsync();

            return true;
        }
    }
}
