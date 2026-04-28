using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Features.Customers.Commands
{
    public class RestoreCustomerCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public RestoreCustomerCommand(int id)
        {
            Id = id;
        }
    }
}
