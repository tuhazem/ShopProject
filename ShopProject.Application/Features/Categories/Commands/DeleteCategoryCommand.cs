using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopProject.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Features.Categories.Commands
{
    public record DeleteCategoryCommand(int id) : IRequest;


    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly IApplicationDbContext context;

        public DeleteCategoryCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await context.Categories.Include(p => p.Products)
                .FirstOrDefaultAsync(c => c.Id == request.id, cancellationToken);

            if(category == null)
            {
                throw new Exception("Category not found");
            }
             
            if(category.Products.Any())
            {
                throw new Exception("Category has products, cannot be deleted");
            }

            context.Categories.Remove(category);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
    


}
