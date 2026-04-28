using AutoMapper;
using MediatR;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Features.Customers.Queries
{
    public class GetCustomerByIdOrNameQuery : IRequest<CustomerDTO>
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
    }


    public class GetCustomerByIdOrNameHandler : IRequestHandler<GetCustomerByIdOrNameQuery, CustomerDTO>
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public GetCustomerByIdOrNameHandler(IUnitOfWork uow , IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        public async Task<CustomerDTO> Handle(GetCustomerByIdOrNameQuery request, CancellationToken cancellationToken)
        {

            Customer customer = null;

            if (request.Id.HasValue)
            {

                customer = await uow.Customers.GetByIdAsync(request.Id.Value);
            }
            else if (!string.IsNullOrEmpty(request.Name)) {

                customer = await uow.Customers.FindAsync(c => c.Name.Contains(request.Name));
            }

            if (customer == null)
                return null!;

            return mapper.Map<CustomerDTO>(customer);

        }
    }

}
