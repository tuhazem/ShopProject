using MediatR;
using ShopProject.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Features.Products.Commands
{
    public record UpdateProductCommand : IRequest<bool>
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public decimal Price { get; init; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool> {
        private readonly IApplicationDbContext context;

        public UpdateProductCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Products.FindAsync(request.Id);

            if(entity == null)
            {
                return false;
            }

            entity.Name = request.Name;
            entity.Price = request.Price;

            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }


}
