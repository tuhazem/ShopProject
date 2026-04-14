using AutoMapper;
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

        public string Description { get; init; } = string.Empty;
        public decimal Price { get; init; }

        public int CategoryId { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool> {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public UpdateProductCommandHandler(IApplicationDbContext context , IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Products.FindAsync(request.Id);

            if(entity == null)
            {
                return false;
            }

            mapper.Map(request, entity);

            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }


}
