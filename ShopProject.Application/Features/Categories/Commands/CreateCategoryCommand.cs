using MediatR;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Features.Categories.Commands
{
    public record CreateCategoryCommand : IRequest<int>
    { 
        public string Name { get; set; } = string.Empty;
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly IApplicationDbContext context;

        public CreateCategoryCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = new Category
            {
                Name = request.Name,
            };

            context.Categories.Add(entity);
            await context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
    
}
