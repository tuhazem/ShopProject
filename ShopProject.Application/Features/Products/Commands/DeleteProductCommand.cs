using MediatR;
using ShopProject.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Features.Products.Commands
{
    public record DeleteProductCommand(int id) : IRequest<bool>;

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IApplicationDbContext context;

        public DeleteProductCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var result = await context.Products.FindAsync(request.id);
            if(result == null)
            {
                return false;
            }

            context.Products.Remove(result);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}

