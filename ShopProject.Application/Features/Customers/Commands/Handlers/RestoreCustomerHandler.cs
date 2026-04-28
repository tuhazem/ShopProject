using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopProject.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Features.Customers.Commands.Handlers
{
    public class RestoreCustomerHandler : IRequestHandler<RestoreCustomerCommand, bool>
    {
        private readonly IApplicationDbContext context;

        public RestoreCustomerHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Handle(RestoreCustomerCommand request, CancellationToken cancellationToken)
        {
            var cusomter = await context.Customers.IgnoreQueryFilters()
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (cusomter == null || cusomter.IsDeleted == false)
            {
                return false;
            }
            cusomter.IsDeleted = false;
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
