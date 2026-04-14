using AutoMapper;
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
    public record UpdateCategoryCommand : IRequest<bool>
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand , bool>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public UpdateCategoryCommandHandler(IApplicationDbContext context , IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Categories.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (entity == null)
            {
               throw new Exception("Category not found");
            }

            mapper.Map(request, entity);
            await context.SaveChangesAsync(cancellationToken);
            return true;

        }
    }
}
